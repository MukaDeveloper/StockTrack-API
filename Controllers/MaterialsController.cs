using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models;
using StockTrack_API.Services;
using StockTrack_API.Utils;
using StockTrack_API.Models.Interfaces.Response.Material;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Models.Interfaces;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class MaterialsController : ControllerBase
    {
        // Contexto do banco de dados
        private readonly DataContext _context;

        // Serviços
        private readonly InstitutionService _instituionService;
        private readonly UserService _userService;

        // Construtor
        public MaterialsController(
            DataContext context,
            InstitutionService institutionService,
            UserService userService
        )
        {
            _context = context;
            _instituionService = institutionService;
            _userService = userService;
        }

        // Busca pelo ID do Material
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                // Busca o ID da instituição pelo serviço, pelo httpContextAcessor
                int institutionId = _instituionService.GetInstitutionId();

                // Busca o material pelo ID e pelo institutionId no banco de dados através do contexto
                Material? material = await _context.ST_MATERIALS
                    .Include(m => m.Status)
                    .FirstOrDefaultAsync(m =>
                        m.Id == id && m.InstitutionId == institutionId
                    );

                // O resultado pode ser null
                if (material == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(material));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(int lastDocId = 0, int limit = 20)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();

                List<Material> list = await _context
                    .ST_MATERIALS.Where(m => m.InstitutionId == institutionId && m.Id > lastDocId)
                    .OrderBy(m => m.Id)
                    .Take(limit)
                    .Include(m => m.Status)
                    .Include(m => m.MaterialWarehouses)
                    .ToListAsync();

                List<GetAllRes> materials = list.Select(material => new GetAllRes
                {
                    Id = material.Id,
                    Name = material.Name,
                    Description = material.Description,
                    ImageURL = material.ImageURL,
                    Manufacturer = material.Manufacturer,
                    RecordNumber = material.RecordNumber,
                    MaterialType = material.MaterialType.ToString(),
                    Measure = material.Measure,
                    Status = material.Status.Select(status => new MaterialStatusDto
                    {
                        Status = status.Status.ToString(),
                        Quantity = status.Quantity
                    }
                    ).ToList(),
                    MaterialWarehouses = material.MaterialWarehouses.Select(
                        mw => mw.WarehouseId).ToList(),
                }).ToList();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(materials));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> AddAsync(AddNewMaterialReq newMaterial)
        {
            try
            {
                int institutionId = _instituionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                // VERIFICAR SE JÁ TEM UM MATERIAL COM ESSE NOME, E SE TIVER, 
                // DAR UM UPDATE ADICIONANDO A QUANTIDADE QUE SERIA INSERIDA
                Material materialToAdd = new() {
                    Active = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name,
                    Description = newMaterial.Description,
                    ImageURL = newMaterial.ImageURL,
                    InstitutionId = institutionId,
                    Manufacturer = newMaterial.Manufacturer,
                    MaterialType = Enum.Parse<EMaterialType>(newMaterial.MaterialType),
                    Measure = newMaterial.Measure,
                    Name = newMaterial.Name,
                    RecordNumber = newMaterial.RecordNumber,
                    Status = new List<MaterialStatus> {
                        new MaterialStatus {
                            Quantity = newMaterial.quantity,
                            Status = EMaterialStatus.AVAILABLE
                        }
                    },
                };
                await _context.ST_MATERIALS.AddAsync(materialToAdd);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(newMaterial));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
