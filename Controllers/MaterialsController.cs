using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly DataContext _context;

        public MaterialsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                Material? i = await _context.ST_MATERIALS
                    .FirstOrDefaultAsync(iBusca => iBusca.Id == id);

                if (i == null)
                {
                    return NotFound();
                }

                return Ok(i);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<Material> list = await _context.ST_MATERIALS.ToListAsync();
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Material newMaterial)
        {
            try
            {
                await _context.ST_MATERIALS.AddAsync(newMaterial);
                await _context.SaveChangesAsync();

                return Ok(newMaterial);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}