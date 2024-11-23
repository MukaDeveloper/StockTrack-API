using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Services;
using StockTrack_API.Utils;

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
                            .Include(s => s.Items)
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
        public IActionResult CreateSolicitation(CreateSolicitationReq solicitation)
        {
            ArgumentNullException.ThrowIfNull(solicitation);

            try
            {
                Console.WriteLine("New Solicitation Request => " + solicitation);
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}