using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
                // Efetua a consulta no banco com todos os itens necessários
                List<GetSolicitationRes> list = await this.GetSolicitationsAsync();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSolicitationAsync(CreateSolicitationReq solicitation)
        {
            if (solicitation == null || solicitation.Items == null || !solicitation.Items.Any())
            {
                return BadRequest("A solicitação deve conter materiais.");
            }

            try
            {
                ArgumentNullException.ThrowIfNull(solicitation);

                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (user.Id != userInstitution.UserId)
                {
                    return BadRequest("Informações divergentes");
                }

                // Validação dos itens da solicitação
                var validatedItems = await ValidateSolicitationItemsAsync(solicitation.Items);
                if (validatedItems == null)
                {
                    return BadRequest("Erro na validação dos materiais.");
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
                    Status = ESolicitationStatus.WAITING,
                };

                _context.ST_SOLICITATIONS.Add(newSolicitation);
                await _context.SaveChangesAsync();

                var res = CreateSolicitationResponse(newSolicitation);

                if (res == null)
                {
                    return BadRequest("Houve um problema ao registrar sua solicitação.");
                }

                return Ok(EnvelopeFactory.factoryEnvelope(res));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSolicitationAsync(UpdateSolicitationReq updateSol)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(updateSol);

                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                if (user.Id != userInstitution.UserId)
                {
                    return BadRequest("Informações divergentes");
                }

                var solicitation = await _context
                    .ST_SOLICITATIONS.Include(s => s.Items)
                    .Include(s => s.UserInstitution)
                    .FirstOrDefaultAsync(s => s.Id == updateSol.Id);

                if (solicitation == null)
                {
                    return NotFound("Solicitação não encontrada.");
                }

                switch (solicitation.Status)
                {
                    case ESolicitationStatus.WAITING:
                        switch (Enum.Parse<ESolicitationStatus>(updateSol.Status))
                        {
                            case ESolicitationStatus.WAITING:
                                return Ok(EnvelopeFactory.factoryEnvelope(solicitation));
                            case ESolicitationStatus.ACCEPT:
                                

                                solicitation.Status = ESolicitationStatus.ACCEPT;
                                solicitation.ApprovedAt = updateSol.MovimentedAt;

                                foreach (var item in solicitation.Items)
                                {
                                    var material = await _context.ST_MATERIALS
                                        .Include(m => m.Status)
                                        .FirstOrDefaultAsync(m => m.Id == item.MaterialId);

                                    if (material == null)
                                    {
                                        throw new Exception($"Material com ID {item.MaterialId} não encontrado.");
                                    }

                                    var availableStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.AVAILABLE);
                                    var borrowedStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.BORROWED);
                                    if (availableStatus == null || availableStatus.Quantity < item.Quantity)
                                    {
                                        throw new Exception($"Quantidade insuficiente para o material com ID {item.MaterialId}.");
                                    }
                                    if (borrowedStatus == null)
                                    {
                                        material.Status.Add(new MaterialStatus
                                        {
                                            Status = EMaterialStatus.BORROWED,
                                            Quantity = item.Quantity,
                                        });
                                        borrowedStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.BORROWED);
                                        if (borrowedStatus == null)
                                        {
                                            throw new Exception($"Erro ao adicionar status de empréstimo para o material com ID {item.MaterialId}.");
                                        }
                                    }

                                    availableStatus.Quantity -= item.Quantity;
                                    borrowedStatus.Quantity += item.Quantity;
                                    _context.ST_MATERIALS.Update(material);
                                }
                                break;
                            case ESolicitationStatus.DECLINED:
                                solicitation.Status = ESolicitationStatus.DECLINED;
                                solicitation.DeclinedAt = updateSol.MovimentedAt;
                                break;
                            default:
                                throw new Exception($"Estado {updateSol.Status} inválido");
                        }
                        break;
                    case ESolicitationStatus.ACCEPT:
                        switch (Enum.Parse<ESolicitationStatus>(updateSol.Status))
                        {
                            case ESolicitationStatus.ACCEPT:
                                return Ok(EnvelopeFactory.factoryEnvelope(solicitation));
                            case ESolicitationStatus.WITHDRAWN:
                                solicitation.Status = ESolicitationStatus.WITHDRAWN;
                                solicitation.BorroadAt = updateSol.MovimentedAt;
                                break;
                            default:
                                throw new Exception($"Estado {updateSol.Status} inválido");
                        }
                        break;
                    case ESolicitationStatus.DECLINED:
                        throw new Exception($"Essa solicitação foi recusada e não pode ser alterada");
                    case ESolicitationStatus.WITHDRAWN:
                        switch (Enum.Parse<ESolicitationStatus>(updateSol.Status))
                        {
                            case ESolicitationStatus.WITHDRAWN:
                                return Ok(EnvelopeFactory.factoryEnvelope(solicitation));
                            case ESolicitationStatus.RETURNED:
                                solicitation.Status = ESolicitationStatus.RETURNED;
                                solicitation.ReturnedAt = updateSol.MovimentedAt;

                                foreach (var item in solicitation.Items)
                                {
                                    var material = await _context.ST_MATERIALS
                                        .Include(m => m.Status)
                                        .FirstOrDefaultAsync(m => m.Id == item.MaterialId);

                                    if (material == null)
                                    {
                                        throw new Exception($"Material com ID {item.MaterialId} não encontrado.");
                                    }

                                    var borrowedStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.BORROWED);
                                    var availableStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.AVAILABLE);
                                    if (borrowedStatus == null || borrowedStatus.Quantity < item.Quantity)
                                    {
                                        throw new Exception($"Informações divergentes para o material com ID {item.MaterialId}. Contate o suporte.");
                                    }
                                    if (availableStatus == null)
                                    {
                                        material.Status.Add(new MaterialStatus
                                        {
                                            Status = EMaterialStatus.AVAILABLE,
                                            Quantity = item.Quantity,
                                        });
                                        availableStatus = material.Status.FirstOrDefault(s => s.Status == EMaterialStatus.AVAILABLE);
                                        if (availableStatus == null)
                                        {
                                            throw new Exception($"Erro ao adicionar status de disponível para o material com ID {item.MaterialId}.");
                                        }
                                    }

                                    borrowedStatus.Quantity -= item.Quantity;
                                    availableStatus.Quantity += item.Quantity;
                                    _context.ST_MATERIALS.Update(material);
                                }
                                break;
                            default:
                                throw new Exception($"Estado {updateSol.Status} inválido");
                        }
                        break;
                    case ESolicitationStatus.RETURNED:
                        break;
                }

                _context.ST_SOLICITATIONS.Update(solicitation);
                await _context.SaveChangesAsync();

                var res = new GetSolicitationRes
                {
                    Id = solicitation.Id,
                    Description = solicitation.Description,
                    UserId = solicitation.UserId,
                    InstitutionId = solicitation.InstitutionId,
                    UserInstitution = new UserInstitutionRes()
                    {
                        Active = solicitation.UserInstitution.Active,
                        UserRole = solicitation.UserInstitution.UserRole.ToString(),
                        UserName = solicitation.UserInstitution.User.Name,
                    },
                    SolicitedAt = solicitation.SolicitedAt,
                    ExpectReturnAt = solicitation.ExpectReturnAt,
                    Status = solicitation.Status.ToString(),
                    Items = solicitation
                        .Items.Select(item => new GetSolicitationItemsRes
                        {
                            MaterialId = item.MaterialId,
                            MaterialName = _context
                                .ST_MATERIALS.Where(m => m.Id == item.MaterialId)
                                .Select(m => m.Name)
                                .FirstOrDefault(),
                            Quantity = item.Quantity,
                            Status = item.Status.ToString(),
                        })
                        .ToList(),
                };

                return Ok(EnvelopeFactory.factoryEnvelope(res));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Método auxiliar para validar os itens da solicitação
        private async Task<List<SolicitationMaterials>?> ValidateSolicitationItemsAsync(
            IEnumerable<SolicitationMaterialsReq> items
        )
        {
            var validatedItems = new List<SolicitationMaterials>();

            foreach (var itemReq in items)
            {
                var material = await _context
                    .ST_MATERIALS.Include(m => m.Status)
                    .FirstOrDefaultAsync(m => m.Id == itemReq.MaterialId);

                if (material == null)
                {
                    return null; // Ou você pode lançar uma exceção personalizada
                }

                var availableStatus = material.Status.FirstOrDefault(s =>
                    s.Status == EMaterialStatus.AVAILABLE
                );
                if (availableStatus == null || availableStatus.Quantity < itemReq.Quantity)
                {
                    return null; // Ou lançar uma exceção personalizada
                }

                validatedItems.Add(
                    new SolicitationMaterials
                    {
                        MaterialId = itemReq.MaterialId,
                        Quantity = itemReq.Quantity,
                        Status = ESolicitationStatus.WAITING,
                    }
                );
            }

            return validatedItems;
        }

        // Método auxiliar para criar a resposta da solicitação
        private GetSolicitationRes CreateSolicitationResponse(Solicitation newSolicitation)
        {
            var items = newSolicitation
                .Items.Select(item => new GetSolicitationItemsRes
                {
                    MaterialId = item.MaterialId,
                    MaterialName = _context
                        .ST_MATERIALS.FirstOrDefault(m => m.Id == item.MaterialId)
                        ?.Name,
                    Quantity = item.Quantity,
                    Status = item.Status.ToString(),
                })
                .ToList();

            return new GetSolicitationRes
            {
                Id = newSolicitation.Id,
                Description = newSolicitation.Description,
                Items = items,
                UserId = newSolicitation.UserId,
                InstitutionId = newSolicitation.InstitutionId,
                UserInstitution = new UserInstitutionRes()
                {
                    Active = newSolicitation.UserInstitution.Active,
                    UserRole = newSolicitation.UserInstitution.UserRole.ToString(),
                    UserName = newSolicitation.UserInstitution.User.Name,
                },
                SolicitedAt = newSolicitation.SolicitedAt,
                ExpectReturnAt = newSolicitation.ExpectReturnAt,
                Status = newSolicitation.Status.ToString(),
            };
        }

        private async Task<List<GetSolicitationRes>> GetSolicitationsAsync()
        {
            // Obtenha o ID da instituição e os dados do usuário
            int institutionId = _institutionService.GetInstitutionId();
            var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

            // Otimização: Buscando diretamente os dados necessários
            var solicitationsQuery = _context
                .ST_SOLICITATIONS.Include(s => s.Items)
                .Include(s => s.UserInstitution)
                .Where(s => s.InstitutionId == institutionId);

            // Filtragem por role do usuário
            switch (userInstitution.UserRole)
            {
                case EUserRole.USER:
                    solicitationsQuery = solicitationsQuery.Where(s => s.UserId == user.Id);
                    break;
                case EUserRole.WAREHOUSEMAN:
                    // Buscando os almoxarifados que o usuário pode acessar
                    var managedWarehouseIds = await _context
                        .ST_WAREHOUSE_USERS.Where(wu => wu.UserId == user.Id)
                        .Select(wu => wu.WarehouseId)
                        .ToListAsync();

                    solicitationsQuery = solicitationsQuery.Where(s =>
                        s.UserId == user.Id
                        || s.Items.Any(item =>
                            _context
                                .ST_MATERIAL_WAREHOUSES.Where(mw =>
                                    managedWarehouseIds.Contains(mw.WarehouseId)
                                )
                                .Select(mw => mw.MaterialId)
                                .Contains(item.MaterialId)
                        )
                    );
                    break;
            }

            // Efetua a consulta no banco com todos os itens necessários
            var list = await solicitationsQuery
                .Select(s => new GetSolicitationRes
                {
                    Id = s.Id,
                    Description = s.Description,
                    UserId = s.UserId,
                    InstitutionId = s.InstitutionId,
                    UserInstitution = new UserInstitutionRes()
                    {
                        Active = s.UserInstitution.Active,
                        UserRole = s.UserInstitution.UserRole.ToString(),
                        UserName = s.UserInstitution.User.Name,
                    },
                    SolicitedAt = s.SolicitedAt,
                    ExpectReturnAt = s.ExpectReturnAt,
                    Status = s.Status.ToString(),
                    Items = s
                        .Items.Select(item => new GetSolicitationItemsRes
                        {
                            MaterialId = item.MaterialId,
                            MaterialName = _context
                                .ST_MATERIALS.Where(m => m.Id == item.MaterialId)
                                .Select(m => m.Name)
                                .FirstOrDefault(),
                            Quantity = item.Quantity,
                            Status = item.Status.ToString(),
                        })
                        .ToList(),
                })
                .ToListAsync();

            return list;
        }
    }
}
