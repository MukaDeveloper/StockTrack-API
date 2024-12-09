using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AreasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly MovimentationService _movimentationService;
        private readonly InstitutionService _instituionService;
        private readonly UserService _userService;

        public AreasController(
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

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();

                Area? area = await _context
                    .ST_AREAS.Where(a => a.InstitutionId == institutionId && a.Active == true)
                    // .Include(a => a.Institution)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (area == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(area));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();

                List<Area> list = await _context
                    .ST_AREAS.Where(area =>
                        area.InstitutionId == institutionId && area.Active == true
                    )
                    // .Include(a => a.Institution)
                    .ToListAsync();
                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> AddAsync(CreateAreaReq data)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    userInstitution.UserRole == EUserRole.USER
                    || userInstitution.UserRole == EUserRole.WAREHOUSEMAN
                    || userInstitution.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                Area? area = await _context
                    .ST_AREAS.Where((a) => a.InstitutionId == institutionId && a.Active == true)
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == data.Name.ToLower());
                if (area != null)
                {
                    throw new Exception("Já existe uma área com esse nome");
                }

                Area newArea = new Area
                {
                    Active = true,
                    Name = data.Name,
                    Description = data.Description,
                    InstitutionId = userInstitution.InstitutionId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name,
                };

                await _context.ST_AREAS.AddAsync(newArea);
                await _context.SaveChangesAsync();

                await _movimentationService.AddArea(
                    institutionId,
                    newArea.Id,
                    user.Name,
                    newArea.Name
                );

                Area? areaAdded = await _context
                    .ST_AREAS
                    // .Include(a => a.Institution)
                    .Where((a) => a.InstitutionId == institutionId && a.Active == true)
                    .FirstOrDefaultAsync(a => a.Id == newArea.Id);

                if (areaAdded == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(areaAdded));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateAreaReq area)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    userInstitution.UserRole == EUserRole.USER
                    || userInstitution.UserRole == EUserRole.WAREHOUSEMAN
                    || userInstitution.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                Area? areaToUpdate = await _context
                    .ST_AREAS
                    // .Include(a => a.Institution)
                    .Where((a) => a.InstitutionId == institutionId && a.Active == true)
                    .FirstOrDefaultAsync(a => a.Id == area.Id);

                if (area.Name != null && area.Name != areaToUpdate?.Name)
                {
                    Area? areaCheck = await _context
                        .ST_AREAS.Where((a) => a.InstitutionId == institutionId && a.Active == true)
                        .FirstOrDefaultAsync(a => a.Name.ToLower() == area.Name.ToLower());
                    if (areaCheck != null)
                    {
                        throw new Exception("Já existe uma área com esse nome!");
                    }
                }

                if (areaToUpdate == null)
                {
                    throw new Exception("Área não encontrada");
                }

                if (area.Name != null)
                {
                    areaToUpdate.Name = area.Name;
                }
                if (area.Description != null)
                {
                    areaToUpdate.Description = area.Description;
                }
                areaToUpdate.UpdatedAt = DateTime.Now;
                areaToUpdate.UpdatedBy = user.Name;

                _context.ST_AREAS.Update(areaToUpdate);
                await _context.SaveChangesAsync();

                await _movimentationService.UpdateArea(
                    institutionId,
                    areaToUpdate.Id,
                    user.Name,
                    areaToUpdate.Name
                );
                return Ok(EnvelopeFactory.factoryEnvelope(areaToUpdate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{areaId}")]
        public async Task<IActionResult> DeleteAsync(int areaId)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Area? areaToDelete =
                    await _context
                        .ST_AREAS.Where((a) => a.InstitutionId == institutionId && a.Active == true)
                        .FirstOrDefaultAsync(x => x.Id == areaId)
                    ?? throw new Exception("Área não encontrada");

                List<Warehouse>? listWarehouses = await _context
                    .ST_WAREHOUSES.Where(wh => wh.AreaId == areaId && wh.Active == true)
                    .ToListAsync();
                if (listWarehouses.Count > 0)
                {
                    throw new Exception(
                        "Essa área não pode ser excluída pois contém almoxarifados vinculados"
                    );
                }

                await _movimentationService.DeleteArea(
                    institutionId,
                    areaToDelete.Id,
                    user.Name,
                    areaToDelete.Name
                );

                areaToDelete.Active = false;
                areaToDelete.UpdatedAt = DateTime.Now;
                areaToDelete.UpdatedBy = user.Name;

                _context.ST_AREAS.Update(areaToDelete);
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
