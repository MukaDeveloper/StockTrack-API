using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockTrack_API.Data;
using StockTrack_API.Models;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ItensController : ControllerBase
    {
        private readonly DataContext _context;

        public ItensController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Item? i = await _context.ST_ITENS
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

    }
}