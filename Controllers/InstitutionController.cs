using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly DataContext _context;

        public InstitutionController(DataContext context)
        {
            _context = context;
        }

        /*
        * GET BY ID
        */
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                Institution? institution = await _context.ST_INSTITUTIONS
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (institution == null)
                {
                    return NotFound();
                }

                return Ok(institution);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
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
                return BadRequest(new { message = ex.Message});
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
                return BadRequest(new { message = ex.Message});
            }
        }
    }
}