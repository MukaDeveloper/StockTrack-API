using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;

        public WarehousesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<Warehouse> list = await _context.ST_WAREHOUSES.ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(Warehouse warehouse)
        {
            try
            {
                if (warehouse.Name.IsNullOrEmpty())
                {
                    throw new Exception("Nome do armazém é obrigatório.");
                }

                if (warehouse.AreaId == 0) {
                    throw new Exception("Área do armazém é obrigatória.");
                }

                await _context.ST_WAREHOUSES.AddAsync(warehouse);
                await _context.SaveChangesAsync();

                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(Warehouse warehouse)
        {
            try
            {
                Warehouse? warehouseToUpdate = await _context.ST_WAREHOUSES.FirstOrDefaultAsync(x => x.Id == warehouse.Id);

                if (warehouseToUpdate == null)
                {
                    throw new Exception("Armazém não encontrado.");
                }

                warehouseToUpdate.Name = warehouse.Name;
                warehouseToUpdate.AreaId = warehouse.AreaId;

                _context.ST_WAREHOUSES.Update(warehouseToUpdate);
                await _context.SaveChangesAsync();

                return Ok(warehouseToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }
    }
}
