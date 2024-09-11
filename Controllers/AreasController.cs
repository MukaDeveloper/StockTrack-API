using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.Area;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class AreasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AreasController(
            DataContext context,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                Area? area = await _context.ST_AREAS
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
                string? contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (contextAcessor == null) {
                    throw new Exception("Requisição inválida.");
                }

                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                List<Area> list = await _context.ST_AREAS
                .Where(area => area.InstitutionId == institutionId)
                .ToListAsync();
                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> AddAsync(AddNewReq data)
        {
            try
            {
                string? context1 = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");
                string? context2 = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (context1 == null || context2 == null) {
                    throw new Exception("Requisição inválida.");
                }

                int userId = int.Parse(context1);
                int institutionId = int.Parse(context2);

                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Id == userId);
                UserInstitution? userInstitution = await _context.ST_USER_INSTITUTIONS
                                .FirstOrDefaultAsync(ui => ui.UserId == userId && ui.InstitutionId == institutionId);

                if (user == null || userInstitution == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                if (userInstitution.UserType == UserType.USER)
                {
                    throw new Exception("Sem autorização.");
                }

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(a => a.Name.ToLower() == data.Name.ToLower());
                if (area != null) {
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
                    CreatedBy = user.Name
                };

                await _context.ST_AREAS.AddAsync(newArea);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(newArea));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(Area area) {
            try
            {
                string? context1 = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");
                string? context2 = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (context1 == null || context2 == null)
                {
                    throw new Exception("Requisição inválida.");
                }

                int userId = int.Parse(context1);
                int institutionId = int.Parse(context2);

                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Id == userId);
                UserInstitution? userInstitution = await _context.ST_USER_INSTITUTIONS
                                .FirstOrDefaultAsync(ui => ui.UserId == userId && ui.InstitutionId == institutionId);

                if (user == null || userInstitution == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                if (userInstitution.UserType == UserType.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                if (area.Name != null) {
                    Area? areaCheck = await _context.ST_AREAS.FirstOrDefaultAsync(a => a.Name.Equals(area.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (areaCheck != null) {
                        throw new Exception("Já existe uma área com esse nome!");
                    }
                }

                Area? areaToUpdate = await _context.ST_AREAS.FirstOrDefaultAsync(a => a.Id == area.Id);

                if (areaToUpdate == null) {
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