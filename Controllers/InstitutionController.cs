using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Services;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserService _userService;
        private readonly InstitutionService _institutionService;

        public InstitutionController(
            DataContext context,
            UserService userService,
            InstitutionService institutionService
        )
        {
            _context = context;
            _userService = userService;
            _institutionService = institutionService;
        }

        /*
        * GET BY ID
        */
        [HttpGet("get-current")]
        public async Task<IActionResult> GetSingleAsync()
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                Institution? institution = await _context.ST_INSTITUTIONS
                // .Include(i => i.Users)
                // .ThenInclude(ui => ui.User)
                .FirstOrDefaultAsync(i =>
                    i.Id == institutionId
                );

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

        [HttpGet("get-all-by-user")]
        public async Task<IActionResult> GetAllByUserAsync()
        {
            try
            {
                User user = _userService.GetUser();

                List<int> institutionIds = await _context
                    .ST_USER_INSTITUTIONS.Where(ui => ui.UserId == user.Id)
                    .Select(ui => ui.InstitutionId)
                    .ToListAsync();

                if (institutionIds.Count == 0)
                {
                    return NotFound();
                }

                List<Institution> list = await _context
                    .ST_INSTITUTIONS.Where(i => institutionIds.Contains(i.Id))
                    .ToListAsync();

                if (list.Count == 0)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
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

                return Ok(EnvelopeFactory.factoryEnvelope(newInstitution));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
