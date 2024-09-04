using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.Warehouse;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehousesController(
            DataContext context,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                string? contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (contextAcessor == null) {
                    throw new Exception("Requisição inválida.");
                }

                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                List<Warehouse> list = await _context.ST_WAREHOUSES
                .Where(w => w.InstitutionId == institutionId)
                .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-area-id/{areaId}")]
        public async Task<IActionResult> GetByAreaIdAsync(int areaId)
        {
            try
            {
                string? contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (contextAcessor == null) {
                    throw new Exception("Requisição inválida.");
                }

                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                List<Warehouse> list = await _context.ST_WAREHOUSES
                .Where(w => w.InstitutionId == institutionId && w.AreaId == areaId)
                .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-id/{warehouseId}")]
        public async Task<IActionResult> GetByIdAsync(int warehouseId)
        {
            try
            {
                string? contextAcessor = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (contextAcessor == null) {
                    throw new Exception("Requisição inválida.");
                }

                int? institutionId = int.Parse(contextAcessor);

                if (!institutionId.HasValue)
                {
                    throw new Exception("Identificação da instituição não localizada.");
                }

                List<Warehouse> list = await _context.ST_WAREHOUSES
                .Where(w => w.InstitutionId == institutionId && w.Id == warehouseId)
                .ToListAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddAsync(CreateReq data)
        {
            try
            {
                if (data.Name.IsNullOrEmpty())
                {
                    throw new Exception("Nome do armazém é obrigatório.");
                }

                if (data.AreaId == 0)
                {
                    throw new Exception("Área do armazém é obrigatória.");
                }

                string? context1 = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");
                string? context2 = _httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId");

                if (context1 == null || context2 == null) {
                    throw new Exception("Requisição inválida.");
                }

                int userId = int.Parse(context1);
                int institutionId = int.Parse(context2);

                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Id == userId);
                UserInstitution? userInstitution = await _context.ST_USER_INSTITUTIONS
                                .FirstOrDefaultAsync(ui => ui.UserId == userId && ui.InstitutionId == institutionId);

                Area? area = await _context.ST_AREAS.FirstOrDefaultAsync(x => x.Id == data.AreaId);

                if (user == null || userInstitution == null)
                {
                    throw new Exception("Usuário não encontrado");
                }

                if (area?.InstitutionId != institutionId || area.Active == false) {
                    throw new Exception("Área não encontrada ou inválida");
                }

                if (userInstitution.UserType == UserType.USER || user.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Warehouse newWarehouse = new Warehouse
                {
                    Active = true,
                    Name = data.Name,
                    Description = data.Description,
                    AreaId = data.AreaId,
                    AreaName = area.Name,
                    InstitutionId = institutionId,
                    InstitutionName = userInstitution.InstitutionName,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name
                };

                await _context.ST_WAREHOUSES.AddAsync(newWarehouse);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(newWarehouse));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
