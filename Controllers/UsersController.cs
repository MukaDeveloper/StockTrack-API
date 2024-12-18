using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Models.Interfaces.Response;
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
                                    Verified = user.Verified,
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
                        userInstitution.UserRole != EUserRole.COORDINATOR
                        && userInstitution.UserRole != EUserRole.SUPPORT
                    )
                    || userInstitution.Active == false
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
                        Verified = u.Verified,
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
                if (credentials.Email.IsNullOrEmpty() || credentials.PasswordString.IsNullOrEmpty())
                {
                    throw new Exception("Todos os campos são obrigatórios.");
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

                user.AccessDate = DateTime.Now;
                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                user.PasswordHash = null;
                user.PasswordSalt = null;
                string Token = _userService.CreateToken(user, null);

                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterReq user)
        {
            try
            {
                if (user.Name == null || user.Email == null || user.Password == null)
                    throw new Exception("Todos os campos são obrigatórios.");

                if (await _userService.ExistUser(user.Email))
                    throw new Exception("Usuário já existente.");

                Cryptography.CryptographyHashHmac(user.Password, out byte[] hash, out byte[] salt);

                if (hash == null || salt == null)
                    throw new Exception("[ERR500] Erro ao cadastrar usuário.");

                User newUser =
                    new()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        CreatedAt = DateTime.Now,
                        Verified = false,
                    };

                if (user.PhotoUrl != null)
                {
                    newUser.PhotoUrl = user.PhotoUrl;
                }

                await _context.ST_USERS.AddAsync(newUser);
                await _context.SaveChangesAsync();

                newUser.VerifiedToken = await _userService.SendConfirmationEmail(newUser);
                newUser.VerifiedScheduled = DateTime.UtcNow.AddHours(2);

                _context.ST_USERS.Update(newUser);
                await _context.SaveChangesAsync();

                User? returnUser =
                    await _context.ST_USERS.FirstOrDefaultAsync(x => x.Id == newUser.Id)
                    ?? throw new Exception("[ERR501] Erro ao cadastrar usuário.");
                ;

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
                    userInstitution.UserRole != EUserRole.COORDINATOR
                        && userInstitution.UserRole != EUserRole.SUPPORT
                    || userInstitution.Active == false
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

                EUserRole userRole;
                if (!Enum.TryParse(member.UserRole, true, out userRole)) // true para ignorar case-sensitive
                {
                    throw new Exception($"Cargo inválido! Valor fornecido: '{member.UserRole}'");
                }
                UserInstitution newUserInstitution =
                    new()
                    {
                        Active = true,
                        UserId = member.UserId,
                        InstitutionId = member.InstitutionId,
                        UserRole = userRole,
                        Solicitations = new List<Solicitation>(),
                    };

                await _movimentationService.AddUser(
                    member.InstitutionId,
                    member.UserId,
                    userToAdd.Name,
                    user.Name
                );

                await _context.ST_USER_INSTITUTIONS.AddAsync(newUserInstitution);
                await _context.SaveChangesAsync();

                GetMembersRes memberRes =
                    new()
                    {
                        Id = userToAdd.Id.ToString(),
                        Name = userToAdd.Name,
                        Email = userToAdd.Email,
                        PhotoUrl = userToAdd.PhotoUrl,
                        Role = newUserInstitution.UserRole.ToString(),
                        Verified = userToAdd.Verified,
                    };

                return Ok(EnvelopeFactory.factoryEnvelope(memberRes));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update-institution-member")]
        public async Task<IActionResult> UpdateMemberAsync(UpdateUserInstitutionReq member)
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
                    userInstitution.UserRole != EUserRole.COORDINATOR
                        && userInstitution.UserRole != EUserRole.SUPPORT
                    || userInstitution.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                User? userToUpdate = await _context.ST_USERS.FirstOrDefaultAsync(x =>
                    x.Id == member.UserId
                );

                if (userToUpdate == null)
                {
                    throw new Exception("Usuário inválido.");
                }

                UserInstitution? userInstitutionToUpdate =
                    await _context.ST_USER_INSTITUTIONS.FirstOrDefaultAsync(ui =>
                        ui.UserId == member.UserId && ui.InstitutionId == member.InstitutionId
                    );

                if (userInstitutionToUpdate == null)
                {
                    throw new Exception("Usuário não identificado na instituição.");
                }

                EUserRole userRole;
                if (!Enum.TryParse(member.UserRole, true, out userRole)) // true para ignorar case-sensitive
                {
                    throw new Exception($"Cargo inválido! Valor fornecido: '{member.UserRole}'");
                }

                UserInstitution updatedUserInstitution =
                    new()
                    {
                        UserId = member.UserId,
                        InstitutionId = member.InstitutionId,
                        UserRole = userRole,
                    };

                await _movimentationService.UpdateUser(
                    member.InstitutionId,
                    member.UserId,
                    userToUpdate.Name,
                    user.Name
                );

                _context.ST_USER_INSTITUTIONS.Update(updatedUserInstitution);
                await _context.SaveChangesAsync();

                GetMembersRes memberRes =
                    new()
                    {
                        Id = userToUpdate.Id.ToString(),
                        Name = userToUpdate.Name,
                        Email = userToUpdate.Email,
                        PhotoUrl = userToUpdate.PhotoUrl,
                        Role = updatedUserInstitution.UserRole.ToString(),
                        Verified = userToUpdate.Verified,
                    };

                return Ok(EnvelopeFactory.factoryEnvelope(memberRes));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("remove-institution-member")]
        public async Task<IActionResult> RemoveMemberAsync(User member)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (
                    userInstitution.UserRole != EUserRole.COORDINATOR
                        && userInstitution.UserRole != EUserRole.SUPPORT
                    || userInstitution.Active == false
                )
                {
                    throw new Exception("Sem autorização.");
                }

                User? userToRemove = await _context.ST_USERS.FirstOrDefaultAsync(x =>
                    x.Id == member.Id
                );

                if (userToRemove == null)
                {
                    throw new Exception("Usuário inválido.");
                }

                UserInstitution? userInstitutionToRemove =
                    await _context.ST_USER_INSTITUTIONS.FirstOrDefaultAsync(ui =>
                        ui.UserId == member.Id && ui.InstitutionId == institutionId
                    );

                if (userInstitutionToRemove == null)
                {
                    throw new Exception("Usuário não identificado na instituição.");
                }

                await _movimentationService.RemoveUser(
                    institutionId,
                    member.Id,
                    member.Name,
                    user.Name
                );

                _context.ST_USER_INSTITUTIONS.Remove(userInstitutionToRemove);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope("Ok"));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("resend-email")]
        public async Task<IActionResult> ResendEmailAsync()
        {
            try
            {
                User user = _userService.GetUser();

                if (user.Verified == true)
                    return Ok(EnvelopeFactory.factoryEnvelope(user));

                // Verifica se o agendamento anterior foi há menos de 45 minutos
                // if (
                //     user.VerifiedScheduled.HasValue
                //     && DateTime.UtcNow < user.VerifiedScheduled.Value.AddMinutes(45)
                // )
                // {
                //     return BadRequest(
                //         new
                //         {
                //             message = "O e-mail de verificação só pode ser reenviado após 45 minutos da última tentativa.",
                //         }
                //     );
                // }

                user.VerifiedToken = await _userService.SendConfirmationEmail(user);
                user.VerifiedScheduled = DateTime.UtcNow.AddHours(2);

                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            try
            {
                // Passo 1: Busca o usuário pelo ID
                User? user = await _context.ST_USERS.FirstOrDefaultAsync(u =>
                    u.Id == request.UserId
                );

                if (user == null)
                    return NotFound("Usuário não encontrado.");

                if (user.Verified == true)
                {
                    string returnToken = _userService.CreateToken(user, null);
                    return Ok(EnvelopeFactory.factoryEnvelope(returnToken));
                }

                if (request.Token == null)
                    return BadRequest("Token não informado.");

                // Passo 3: Compara o hash gerado com o armazenado no banco de dados
                if (user.VerifiedToken != request.Token)
                {
                    return BadRequest("Token inválido ou expirado.");
                }

                // Passo 4: Atualiza o status do usuário para verificado
                user.Verified = true;
                user.VerifiedToken = null; // Limpa o campo do token de verificação
                user.VerifiedScheduled = null; // Limpa a data agendada para verificação, se houver

                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                // Passo 5: Retorna o token do usuário confirmado
                string Token = _userService.CreateToken(user, null);
                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                Institution? institution = await _context.ST_INSTITUTIONS.FirstOrDefaultAsync(i =>
                    i.Id == institutionId
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("change-institution")]
        public IActionResult ChangeInstitutionAsync()
        {
            try
            {
                User user = _userService.GetUser();

                string Token = _userService.CreateToken(user, null);

                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("select-institution")]
        public IActionResult SelectInstitutionAsync(SelectInstitutionReq req)
        {
            try
            {
                var (user, userInstitution) = _userService.GetUserAndInstitution(req.InstitutionId);

                if (userInstitution == null)
                    throw new Exception("Usuário não pertence a essa instituição.");

                string Token = _userService.CreateToken(user, userInstitution);

                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
