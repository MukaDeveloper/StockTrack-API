using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Services;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class SolicitationsController : ControllerBase
    {

        private readonly DataContext _context;
        // private readonly MovimentationService _movimentationService;
        private readonly InstitutionService _institutionService;
        private readonly UserService _userService;

        public SolicitationsController(
            DataContext context,
            // MovimentationService movimentationService,
            InstitutionService instituionService,
            UserService userService
        )
        {
            _context = context;
            // _movimentationService = movimentationService;
            _institutionService = instituionService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                List<Solicitation> list = await _context
                    .ST_SOLICITATIONS.Include(s => s.Items)
                    .Where(s => s.InstitutionId == institutionId)
                    .ToListAsync();

                switch (userInstitution.UserRole)
                {
                    case EUserRole.USER:
                        list = list.Where(w => w.UserId == user.Id).ToList();
                        break;
                    case EUserRole.WAREHOUSEMAN:
                        // Se o usuário for um almoxarife, pego quais almoxarifados o usuário é responsável
                        var managedWarehouseIds = await _context.ST_WAREHOUSE_USERS
                            .Where(wu => wu.UserId == user.Id)
                            .Select(wu => wu.WarehouseId)
                            .ToListAsync();

                        // Modifico minha lista para exibir somente as solicitações onde algum item
                        // Faça parte de algum almoxarifado que o usuário é responsável
                        list = await _context.ST_SOLICITATIONS
                            .Where(s => s.Items.Any(item => _context.ST_MATERIAL_WAREHOUSES
                                .Where(mw => managedWarehouseIds.Contains(mw.WarehouseId))
                                .Select(mw => mw.MaterialId)
                                .Contains(item.MaterialId)))
                            .ToListAsync();

                        // Para cada solicitação, eu filtro para exibir apenas os materiais no qual
                        // o usuário é responsável
                        foreach (var solicitation in list)
                        {
                            solicitation.Items = solicitation.Items
                                .Where(item => _context.ST_MATERIAL_WAREHOUSES
                                .Any(mw => managedWarehouseIds.Contains(mw.WarehouseId)
                                    && mw.MaterialId == item.MaterialId))
                                .ToList();
                        }
                        break;
                    default:
                        break;
                }

                List<GetSolicitationRes> res = new();
                for (int i = 0; i < list.Count; i++)
                {
                    GetSolicitationRes solicitation = new()
                    {
                        Id = list[i].Id,
                        Description = list[i].Description,
                        Items = list[i].Items,
                        UserId = list[i].UserId,
                        InstitutionId = list[i].InstitutionId,
                        SolicitedAt = list[i].SolicitedAt,
                        ExpectReturnAt = list[i].ExpectReturnAt,
                        Status = list[i].Status.ToString(),
                    };

                    res.Add(solicitation);
                }

                return Ok(EnvelopeFactory.factoryEnvelopeArray(res));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Liberado Anonymous apenas para testes no postman
        // [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateSolicitationAsync(CreateSolicitationReq solicitation)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(solicitation);
                if (solicitation == null || solicitation.Items == null || !solicitation.Items.Any())
                {
                    return BadRequest("A solicitação deve conter materiais.");
                }

                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (user.Id != userInstitution.UserId)
                {
                    return BadRequest("Informações divergentes");
                }

                var validatedItems = new List<SolicitationMaterials>();

                foreach (var itemReq in solicitation.Items)
                {
                    Material? material = await _context.ST_MATERIALS
                        .Include(m => m.Status)
                        .FirstOrDefaultAsync(m => m.Id == itemReq.MaterialId);

                    if (material == null)
                    {
                        return NotFound($"Material com ID {itemReq.MaterialId} não encontrado.");
                    }

                    var availableStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.AVAILABLE);
                    if (availableStatus == null || availableStatus.Quantity < itemReq.Quantity)
                    {
                        return BadRequest($"Quantidade divergente para o material: {material.Name}");
                    }

                    validatedItems.Add(new SolicitationMaterials
                    {
                        MaterialId = itemReq.MaterialId,
                        Quantity = itemReq.Quantity,
                        Status = ESolicitationStatus.WAITING
                    });
                }

                var newSolicitation = new Solicitation
                {
                    Description = solicitation.Description,
                    Items = validatedItems,
                    UserId = userInstitution.UserId,
                    InstitutionId = userInstitution.InstitutionId,
                    UserInstitution = userInstitution,
                    SolicitedAt = DateTime.UtcNow,
                    ExpectReturnAt = solicitation.ExpectReturnAt,
                    Status = ESolicitationStatus.WAITING
                };

                _context.ST_SOLICITATIONS.Add(newSolicitation);
                _context.SaveChanges();

                GetSolicitationRes res = new()
                {
                    Id = newSolicitation.Id,
                    Description = newSolicitation.Description,
                    Items = newSolicitation.Items,
                    UserId = newSolicitation.UserId,
                    InstitutionId = newSolicitation.InstitutionId,
                    SolicitedAt = newSolicitation.SolicitedAt,
                    ExpectReturnAt = newSolicitation.ExpectReturnAt,
                    Status = newSolicitation.Status.ToString(),
                };

                if (res == null)
                {
                    return BadRequest("Houve um problema ao registrar sua solicitação. Consulte o responsável do(s) almoxarifado(s) relacionados.");
                }

                return Ok(EnvelopeFactory.factoryEnvelope(res));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}