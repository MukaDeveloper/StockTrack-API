using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.User;
using StockTrack_API.Models.Response;
using StockTrack_API.Services;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly UserService _userService;
        private readonly InstitutionService _institutionService;
        private readonly MovimentationService _movimentationService;

        public UsersController(
            DataContext context,
            UserService userService,
            InstitutionService institutionService,
            MovimentationService movimentationService
        )
        {
            _context = context;
            _userService = userService;
            _institutionService = institutionService;
            _movimentationService = movimentationService;
        }

        [HttpGet("by-institution")]
        public async Task<IActionResult> GetByInstitutionAsync()
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                List<UserInstitution>? userInstitution = await _context
                    .ST_USER_INSTITUTIONS.Where(ui => ui.InstitutionId == institutionId)
                    .ToListAsync();

                if (userInstitution.Count > 0)
                {
                    List<GetMembersRes> members = new();
                    for (int i = 0; i < userInstitution.Count; i++)
                    {
                        User? user = await _context.ST_USERS.FirstOrDefaultAsync(u =>
                            u.Id == userInstitution[i].UserId
                        );

                        if (user != null)
                        {
                            GetMembersRes member =
                                new()
                                {
                                    Id = user.Id.ToString(),
                                    Name = user.Name,
                                    Email = user.Email,
                                    PhotoUrl = user.PhotoUrl,
                                    Role = userInstitution[i].UserRole.ToString(),
                                };

                            members.Add(member);
                        }
                    }
                    return Ok(EnvelopeFactory.factoryEnvelopeArray(members));
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchByName(string query)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    (
                        userInstitution.UserRole != UserRole.COORDINATOR
                        && userInstitution.UserRole != UserRole.SUPPORT
                    )
                    || user.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                // Pesquisa todos os usuários que tenha um e-mail parecido com a query
                // e que não pertença a instituição
                List<User> listEmail = await _context
                    .ST_USERS.Where(ui =>
                        EF.Functions.Like(ui.Email, "%" + query + "%")
                        && !_context.ST_USER_INSTITUTIONS.Any(i =>
                            i.UserId == ui.Id && i.InstitutionId == institutionId
                        )
                    )
                    .ToListAsync();

                // Pesquisa todos os usuários que tenha um NOME parecido com a query
                // e que não pertença a instituição
                List<User> listName = await _context
                    .ST_USERS.Where(ui =>
                        EF.Functions.Like(ui.Name, "%" + query + "%")
                        && !_context.ST_USER_INSTITUTIONS.Any(i =>
                            i.UserId == ui.Id && i.InstitutionId == institutionId
                        )
                    )
                    .ToListAsync();

                // Unindo as duas listas e removendo duplicatas com base no ID
                List<GetMembersRes> list = listEmail
                    .Union(listName)
                    .DistinctBy(u => u.Id)
                    .Select(u => new GetMembersRes
                    {
                        Id = u.Id.ToString(),
                        Name = u.Name,
                        Email = u.Email,
                        PhotoUrl = u.PhotoUrl,
                    })
                    .ToList();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(list));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-uid/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            try
            {
                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                return Ok(EnvelopeFactory.factoryEnvelope(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> AuthenticateAsync(UserAuthReq credentials)
        {
            try
            {
                int? institutionId = credentials.InstitutionId;

                if (credentials.Email.IsNullOrEmpty() || credentials.PasswordString.IsNullOrEmpty())
                {
                    throw new Exception("Todos os campos são obrigatórios.");
                }

                if (institutionId == null)
                {
                    throw new Exception("Instituição não informada.");
                }

                User? user = await _context
                    .ST_USERS.Where(x => x.Email.ToLower() == credentials.Email.ToLower())
                    .FirstOrDefaultAsync();

                if (
                    user == null
                    || !Cryptography.VerifyPasswordHash(
                        credentials.PasswordString,
                        user.PasswordHash!,
                        user.PasswordSalt!
                    )
                )
                {
                    throw new Exception("Usuário ou senha incorreto(s).");
                }

                UserInstitution? userInstitution =
                    await _context.ST_USER_INSTITUTIONS.FirstOrDefaultAsync(ui =>
                        ui.UserId == user.Id && ui.InstitutionId == institutionId
                    );

                if (userInstitution == null)
                {
                    throw new Exception("Usuário não pertence a essa instituição.");
                }

                user.AccessDate = DateTime.Now;
                user.Role = userInstitution.UserRole;
                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                user.PasswordHash = null;
                user.PasswordSalt = null;
                string Token = _userService.CreateToken(user, userInstitution);

                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterReq user)
        {
            try
            {
                if (user.Name == null || user.Email == null || user.Password == null)
                    throw new Exception("Todos os campos são obrigatórios.");

                if (await _userService.ExistUser(user.Email))
                    throw new Exception("Usuário já existente.");

                Cryptography.CreatePasswordHash(user.Password, out byte[] hash, out byte[] salt);

                if (hash == null || salt == null)
                    throw new Exception("[ERR500] Erro ao cadastrar usuário.");

                User newUser =
                    new()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Active = true,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        CreatedAt = DateTime.Now,
                    };

                if (user.PhotoUrl != null)
                {
                    newUser.PhotoUrl = user.PhotoUrl;
                }

                await _context.ST_USERS.AddAsync(newUser);
                await _context.SaveChangesAsync();

                User? returnUser = await _context.ST_USERS.FirstOrDefaultAsync(x =>
                    x.Id == newUser.Id
                );

                if (returnUser == null)
                {
                    throw new Exception("[ERR501] Erro ao cadastrar usuário.");
                }
                return Ok(EnvelopeFactory.factoryEnvelope(returnUser));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-institution-member")]
        public async Task<IActionResult> AddMemberAsync(AddUserInstitutionReq member)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (institutionId != member.InstitutionId)
                {
                    throw new Exception("Instituição inválida.");
                }

                if (
                    userInstitution.UserRole != UserRole.COORDINATOR
                        && userInstitution.UserRole != UserRole.SUPPORT
                    || user.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                User? userToAdd = await _context.ST_USERS.FirstOrDefaultAsync(x =>
                    x.Id == member.UserId
                );

                if (userToAdd == null)
                {
                    throw new Exception("Usuário inválido.");
                }

                UserInstitution? userInstitutionToAdd =
                    await _context.ST_USER_INSTITUTIONS.FirstOrDefaultAsync(ui =>
                        ui.UserId == member.UserId && ui.InstitutionId == member.InstitutionId
                    );

                if (userInstitutionToAdd != null)
                {
                    throw new Exception("Usuário já cadastrado na instituição.");
                }

                Console.WriteLine($"Valor de member.UserRole: '{member.UserRole}'"); // Exibe o valor exato

                UserRole userRole;
                if (!Enum.TryParse(member.UserRole, true, out userRole)) // true para ignorar case-sensitive
                {
                    throw new Exception($"Cargo inválido! Valor fornecido: '{member.UserRole}'");
                }
                UserInstitution newUserInstitution =
                    new()
                    {
                        UserId = member.UserId,
                        InstitutionId = member.InstitutionId,
                        UserRole = userRole,
                    };

                await _movimentationService.AddUser(
                    member.InstitutionId,
                    member.UserId,
                    userToAdd.Name,
                    user.Name
                );

                await _context.ST_USER_INSTITUTIONS.AddAsync(newUserInstitution);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope("OK"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
