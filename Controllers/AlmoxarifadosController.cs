using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTrack_API.Data;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AlmoxarifadosController : ControllerBase
    {
        private readonly DataContext _context;


        public AlmoxarifadosController(DataContext context)
        {
            _context = context;
        }


    }
}