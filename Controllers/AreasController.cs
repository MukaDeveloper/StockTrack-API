using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.Area;
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
                    .ST_AREAS.Where(a => a.InstitutionId == institutionId)
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
                    .ST_AREAS.Where(area => area.InstitutionId == institutionId)
                    .ToListAsync();
                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> AddAsync(AddNewAreaReq data)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    userInstitution.UserType == UserType.USER
                    || userInstitution.UserType == UserType.WAREHOUSEMAN
                    || user.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(a =>
                    a.Name.ToLower() == data.Name.ToLower()
                );
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
                    InstitutionName = userInstitution.InstitutionName,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name,
                };

                await _context.ST_AREAS.AddAsync(newArea);
                await _context.SaveChangesAsync();

                await _movimentationService.AddArea(
                    institutionId,
                    newArea.Id,
                    user.Id,
                    newArea.Name
                );
                return Ok(EnvelopeFactory.factoryEnvelope(newArea));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(Area area)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    userInstitution.UserType == UserType.USER
                    || userInstitution.UserType == UserType.WAREHOUSEMAN
                    || user.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                if (area.Name != null)
                {
                    Area? areaCheck = await _context.ST_AREAS.FirstOrDefaultAsync(a =>
                        a.Name.Equals(area.Name, StringComparison.CurrentCultureIgnoreCase)
                    );
                    if (areaCheck != null)
                    {
                        throw new Exception("Já existe uma área com esse nome!");
                    }
                }

                Area? areaToUpdate = await _context.ST_AREAS.FirstOrDefaultAsync(a =>
                    a.Id == area.Id
                );

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

                return Ok(EnvelopeFactory.factoryEnvelope(areaToUpdate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
