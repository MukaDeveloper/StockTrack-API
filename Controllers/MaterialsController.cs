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
    public class MaterialsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MaterialsController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("get-by-id/{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                string? contextAcessor =
                    (_httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId"))
                    ?? throw new Exception("Requisição inválida.");
                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                Material? material = await _context.ST_MATERIALS.FirstOrDefaultAsync(iBusca =>
                    iBusca.Id == id
                );

                if (material == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(material));
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
                string? contextAcessor =
                    (_httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId"))
                    ?? throw new Exception("Requisição inválida.");
                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                List<Material> list = await _context
                    .ST_MATERIALS.Where(m => m.InstitutionId == institutionId)
                    .ToListAsync();
                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(Material newMaterial)
        {
            try
            {
                await _context.ST_MATERIALS.AddAsync(newMaterial);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(newMaterial));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
