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
    public class AreasController : ControllerBase
    {
        private readonly DataContext _context;

        public AreasController(DataContext context)
        {
            _context = context;
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
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<Area> list = await _context.ST_AREAS.ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(Area newArea)
        {
            try
            {
                await _context.ST_AREAS.AddAsync(newArea);
                await _context.SaveChangesAsync();

                return Ok(newArea);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

    }
}