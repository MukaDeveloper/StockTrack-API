using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InstitutionController(
            DataContext context,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /*
        * GET BY ID
        */
        [HttpGet("get-current")]
        public async Task<IActionResult> GetSingleAsync()
        {
            try
            {
                string? contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (contextAcessor == null) {
                    throw new Exception("Requisição inválida.");
                }

                int institutionId = int.Parse(contextAcessor);

                Institution? institution = await _context.ST_INSTITUTIONS
                    .FirstOrDefaultAsync(a => a.Id == institutionId);

                if (institution == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(institution));
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
                List<Institution> list = await _context.ST_INSTITUTIONS.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(Institution newInstitution)
        {
            try
            {
                await _context.ST_INSTITUTIONS.AddAsync(newInstitution);
                await _context.SaveChangesAsync();

                return Ok(newInstitution);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}