using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTrack_API.Data;

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


    }
}