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

                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x =>
                    x.Email.ToLower().Equals(credentials.Email.ToLower())
                );

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

                bool hasInstitution = await _context.ST_USER_INSTITUTIONS.AnyAsync(ui =>
                    ui.UserId == user.Id && ui.InstitutionId == institutionId
                );

                if (!hasInstitution)
                {
                    throw new Exception("Usuário não pertence à instituição especificada.");
                }

                user.AccessDate = DateTime.Now;
                user.InstitutionId = institutionId ?? credentials.InstitutionId;
                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                user.PasswordHash = null;
                user.PasswordSalt = null;
                string Token = _userService.CreateToken(user);

                return Ok(EnvelopeFactory.factoryEnvelope(Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            try
            {
                if (await _userService.ExistUser(user.Email))
                    throw new Exception("Usuário já existente.");

                Cryptography.CreatePasswordHash(
                    user.PasswordString,
                    out byte[] hash,
                    out byte[] salt
                );
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _context.ST_USERS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(EnvelopeFactory.factoryEnvelope(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
