using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Services;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly MovimentationService _movimentationService;
        private readonly InstitutionService _institutionService;
        private readonly UserService _userService;

        public WarehousesController(
            DataContext context,
            MovimentationService movimentationService,
            InstitutionService instituionService,
            UserService userService
        )
        {
            _context = context;
            _movimentationService = movimentationService;
            _institutionService = instituionService;
            _userService = userService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .Include(w => w.Warehousemans)
                    .Where(w => w.InstitutionId == institutionId && w.Active == true)
                    .ToListAsync();

                if (userInstitution.UserRole == EUserRole.WAREHOUSEMAN)
                {
                    list = list.Where(w => w.Warehousemans != null
                        && w.Warehousemans.Any(wh => wh.UserId == userInstitution.UserId)).ToList();
                }

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-area-id/{areaId}")]
        public async Task<IActionResult> GetByAreaIdAsync(int areaId)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .Include(w => w.Warehousemans)
                    .Where(w => w.InstitutionId == institutionId && w.AreaId == areaId && w.Active == true)
                    .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-id/{warehouseId}")]
        public async Task<IActionResult> GetByIdAsync(int warehouseId)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .Include(w => w.Warehousemans)
                    .Where(w => w.InstitutionId == institutionId && w.Id == warehouseId && w.Active == true)
                    .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search-by-name/{nameQuery}")]
        public async Task<IActionResult> SearchByName(string nameQuery)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .Include(w => w.Warehousemans)
                    .Where(w =>
                        w.InstitutionId == institutionId
                        && EF.Functions.Like(w.Name, "%" + nameQuery + "%")
                        && w.Active == true
                    )
                    .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(CreateWarehouseReq data)
        {
            try
            {
                if (data.Name.IsNullOrEmpty())
                {
                    throw new Exception("Nome do armazém é obrigatório.");
                }

                if (data.AreaId == 0)
                {
                    throw new Exception("Área do armazém é obrigatória.");
                }

                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(x => x.Id == data.AreaId && x.Active == true);
                Warehouse? warehouse = await _context.ST_WAREHOUSES
                    .Where((a) => a.InstitutionId == institutionId && a.Active == true)
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == data.Name.ToLower());

                if (warehouse != null)
                {
                    throw new Exception("Nome de armazém já cadastrado.");
                }

                if (area?.InstitutionId != institutionId || area.Active == false)
                {
                    throw new Exception("Área não encontrada ou inválida");
                }

                Warehouse newWarehouse =
                    new()
                    {
                        Active = true,
                        Name = data.Name,
                        Description = data.Description,
                        AreaId = data.AreaId,
                        InstitutionId = institutionId,
                        CreatedAt = DateTime.Now,
                        CreatedBy = user.Name,
                    };

                await _context.ST_WAREHOUSES.AddAsync(newWarehouse);
                await _context.SaveChangesAsync();

                if (data.Warehousemans != null && data.Warehousemans.Count > 0)
                {
                    for (int i = 0; i < data.Warehousemans.Count; i++)
                    {
                        await _context.ST_WAREHOUSE_USERS.AddAsync(
                            new WarehouseUsers
                            {
                                UserId = data.Warehousemans[i].Id,
                                WarehouseId = newWarehouse.Id,
                            }
                        );
                    }
                    await _context.SaveChangesAsync();
                }

                await _movimentationService.AddWarehouse(
                    newWarehouse.Id,
                    newWarehouse.Name,
                    user.Name,
                    institutionId
                );

                Warehouse? warehouseAdded = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .FirstOrDefaultAsync(w => w.Id == newWarehouse.Id);

                if (warehouseAdded == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(warehouseAdded));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Mudar a interface da requisição pra aceitar AreaIdBefore e AreaIdAfter
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateWarehouseReq warehouse)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse? warehouseToUpdate = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .FirstOrDefaultAsync(x => x.Id == warehouse.Id && x.Active == true);

                if (warehouse.Name != null && warehouse.Name != warehouseToUpdate?.Name)
                {
                    Warehouse? warehouseCheck = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(
                        x => x.Name.ToLower() == warehouse.Name.ToLower() && x.Active == true
                    );
                    if (warehouseCheck != null)
                    {
                        throw new Exception("Já existe um almoxarifado com esse nome!");
                    }
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(x =>
                    x.Id == warehouse.AreaId && x.Active == true
                );

                if (area == null || area?.InstitutionId != institutionId || area.Active == false)
                {
                    throw new Exception("Área não encontrada ou inválida");
                }

                if (warehouseToUpdate == null)
                {
                    throw new Exception("Armazém não encontrado.");
                }

                if (warehouse.Name != null)
                {
                    warehouseToUpdate.Name = warehouse.Name;
                }
                if (warehouse.Description != null)
                {
                    warehouseToUpdate.Description = warehouse.Description;
                }
                if (warehouse.Warehousemans != null)
                {
                    List<WarehouseUsers>? warehouseUsers = await _context.ST_WAREHOUSE_USERS
                        .Where(wu => wu.WarehouseId == warehouse.Id)
                        .ToListAsync();

                    if (warehouseUsers.Count > 0)
                    {
                        _context.ST_WAREHOUSE_USERS.RemoveRange(warehouseUsers);
                        await _context.SaveChangesAsync();
                    }

                    for (int i = 0; i < warehouse.Warehousemans.Count; i++)
                    {
                        await _context.ST_WAREHOUSE_USERS.AddAsync(
                            new WarehouseUsers
                            {
                                UserId = warehouse.Warehousemans[i].Id,
                                WarehouseId = warehouseToUpdate.Id,
                            }
                        );
                    }
                }
                if (area.Id != warehouseToUpdate.AreaId)
                {
                    warehouseToUpdate.AreaId = warehouse.AreaId;
                    area.Warehouses.Remove(warehouseToUpdate);
                }
                warehouseToUpdate.UpdatedAt = DateTime.Now;
                warehouseToUpdate.UpdatedBy = user.Name;

                _context.ST_WAREHOUSES.Update(warehouseToUpdate);
                await _context.SaveChangesAsync();

                await _movimentationService.UpdateWarehouse(
                    institutionId,
                    warehouseToUpdate.Id,
                    user.Name,
                    warehouseToUpdate.Name
                );

                return Ok(EnvelopeFactory.factoryEnvelope(warehouseToUpdate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{warehouseId}")]
        public async Task<IActionResult> DeleteAsync(int warehouseId)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse? warehouseToDelete =
                    await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x => x.Id == warehouseId && x.Active == true)
                    ?? throw new Exception("Almoxarifado não encontrado");

                List<MaterialWarehouses>? listMaterialWarehouses = await _context
                    .ST_MATERIAL_WAREHOUSES.Where(m => m.WarehouseId == warehouseId)
                    .ToListAsync();

                if (listMaterialWarehouses.Count > 0)
                {
                    throw new Exception(
                        $"Almoxarifado não pode ser excluído pois contém {listMaterialWarehouses.Count} materiais vinculados"
                    );
                }

                await _movimentationService.DeleteWarehouse(
                    institutionId,
                    warehouseToDelete.Id,
                    user.Name,
                    warehouseToDelete.Name
                );

                warehouseToDelete.Active = false;
                warehouseToDelete.UpdatedAt = DateTime.Now;
                warehouseToDelete.UpdatedBy = user.Name;

                _context.ST_WAREHOUSES.Update(warehouseToDelete);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
