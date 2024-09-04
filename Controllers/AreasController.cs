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

                return Ok(area);
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
                string contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");
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
                int userId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("id"));
                int institutionId = int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId"));

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

                Area newArea = new Area
                {
                    Active = true,
                    Name = data.Name,
                    Description = data.Description,
                    InstitutionId = data.InstitutionId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name
                };

                await _context.ST_AREAS.AddAsync(newArea);
                await _context.SaveChangesAsync();

                return Ok(newArea);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}