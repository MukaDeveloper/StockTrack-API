using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Request.User;
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

        public UsersController(DataContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
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
                    throw new Exception("Usuário não pertence à instituição especificada.");
                }

                user.AccessDate = DateTime.Now;
                user.Role = userInstitution.UserRole;
                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                user.PasswordHash = null;
                user.PasswordSalt = null;
                string Token = _userService.CreateToken(user, institutionId ?? credentials.InstitutionId);

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
    }
}
