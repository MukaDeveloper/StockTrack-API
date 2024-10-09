using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.Warehouse;
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

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .Include(w => w.Warehousemans)
                    .Where(w => w.InstitutionId == institutionId)
                    .ToListAsync();

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
                    .Where(w => w.InstitutionId == institutionId && w.AreaId == areaId)
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
                    .Where(w => w.InstitutionId == institutionId && w.Id == warehouseId)
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

                if (userInstitution.UserRole == UserRole.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(x => x.Id == data.AreaId);
                Warehouse? warehouse = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == data.Name.ToLower()
                );

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

                _context.ST_AREAS.Update(area);
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

                if (userInstitution.UserRole == UserRole.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse? warehouseToUpdate = await _context
                    .ST_WAREHOUSES.Include(w => w.Area)
                    .FirstOrDefaultAsync(x => x.Id == warehouse.Id);

                if (warehouse.Name != null && warehouse.Name != warehouseToUpdate?.Name)
                {
                    Warehouse? warehouseCheck = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(
                        x => x.Name.ToLower() == warehouse.Name.ToLower()
                    );
                    if (warehouseCheck != null)
                    {
                        throw new Exception("Já existe um almoxarifado com esse nome!");
                    }
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(x =>
                    x.Id == warehouse.AreaId
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

                if (userInstitution.UserRole == UserRole.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse? warehouseToDelete =
                    await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x => x.Id == warehouseId)
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

                _context.ST_WAREHOUSES.Remove(warehouseToDelete);
                await _context.SaveChangesAsync();

                return Ok("Almoxarifado excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
