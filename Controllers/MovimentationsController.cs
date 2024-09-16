using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class MovimentationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InstitutionValidationService _instituionService;

        public MovimentationsController(
            DataContext context,
            IHttpContextAccessor httpContextAccessor,
            InstitutionValidationService instituionService
        )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _instituionService = instituionService;
        }

        [HttpGet("get-by-id/{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();

                Movimentation? mov = await _context.ST_MOVIMENTATIONS.FirstOrDefaultAsync(iBusca =>
                    iBusca.Id == id
                );

                if (mov == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(mov));
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

                List<Movimentation> list = await _context
                    .ST_MOVIMENTATIONS.Where(m => m.InstitutionId == institutionId)
                    .ToListAsync();
                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
