using System.Security.Claims;
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
        private readonly InstitutionService _instituionService;
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
            _instituionService = instituionService;
            _userService = userService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Where(w => w.InstitutionId == institutionId)
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
                int institutionId = _instituionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Where(w =>
                        w.InstitutionId == institutionId && w.AreaId == areaId
                    )
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
                int institutionId = _instituionService.GetInstitutionId();

                List<Warehouse> list = await _context
                    .ST_WAREHOUSES.Where(w =>
                        w.InstitutionId == institutionId && w.Id == warehouseId
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
        public async Task<IActionResult> AddAsync(CreateReq data)
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

                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserType == UserType.USER || user.Active == false)
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
                        AreaName = area.Name,
                        InstitutionId = institutionId,
                        InstitutionName = userInstitution.InstitutionName,
                        CreatedAt = DateTime.Now,
                        CreatedBy = user.Name,
                    };
                area.Warehouses.Add(newWarehouse);
                _context.ST_AREAS.Update(area);
                await _context.ST_WAREHOUSES.AddAsync(newWarehouse);
                await _context.SaveChangesAsync();

                await _movimentationService.AddWarehouse(
                    institutionId,
                    newWarehouse.Id,
                    user.Id,
                    newWarehouse.Name
                );
                return Ok(EnvelopeFactory.factoryEnvelope(newWarehouse));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Mudar a interface da requisição pra aceitar AreaIdBefore e AreaIdAfter
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateReq warehouse)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserType == UserType.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                if (warehouse.Name != null)
                {
                    Warehouse? warehouseCheck = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(
                        x =>
                            x.Name.Equals(warehouse.Name, StringComparison.CurrentCultureIgnoreCase)
                    );
                    if (warehouseCheck != null)
                    {
                        throw new Exception("Já existe um almoxarifado com esse nome!");
                    }
                }

                Area? areaBefore = await _context.ST_AREAS.FirstOrDefaultAsync(x =>
                    x.Id == warehouse.AreaId
                );
                Area? areaAfter = await _context.ST_AREAS.FirstOrDefaultAsync(x =>
                    x.Id == warehouse.AreaIdAfter
                );

                Warehouse? warehouseToUpdate = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x =>
                    x.Id == warehouse.Id
                );

                if (areaBefore?.InstitutionId != institutionId || areaBefore.Active == false)
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
                if (warehouse.AreaIdAfter != null && areaAfter != null)
                {
                    if (areaAfter?.InstitutionId != institutionId || areaAfter.Active == false)
                    {
                        throw new Exception("Área alvo não encontrada ou inválida");
                    }

                    warehouseToUpdate.AreaId = (int)warehouse.AreaIdAfter;
                    warehouseToUpdate.Area = areaAfter;
                    areaBefore.Warehouses.Remove(warehouseToUpdate);
                }
                warehouseToUpdate.UpdatedAt = DateTime.Now;
                warehouseToUpdate.UpdatedBy = user.Name;

                _context.ST_WAREHOUSES.Update(warehouseToUpdate);
                await _context.SaveChangesAsync();

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
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserType == UserType.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse? warehouseToDelete =
                    await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x => x.Id == warehouseId)
                    ?? throw new Exception("Almoxarifado não encontrado");
                List<Material>? listMaterial = await _context
                    .ST_MATERIALS.Where(m => m.WarehouseId == warehouseId)
                    .ToListAsync();
                if (listMaterial.Count > 0)
                {
                    throw new Exception(
                        "Almoxarifado não pode ser excluído pois contém materiais vinculados"
                    );
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
