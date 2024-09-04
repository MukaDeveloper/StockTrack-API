using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Enums;
using StockTrack_API.Utils;
using StockTrack_API.Models.Request.User;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(
            DataContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> AuthenticateAsync(AuthReq credentials)
        {
            try
            {
                int? institutionId = credentials.InstitutionId;

                if (
                    credentials.Email.IsNullOrEmpty()
                    || credentials.PasswordString.IsNullOrEmpty()
                    || (!institutionId.HasValue && institutionId.Value <= 0)
                )
                {
                    throw new Exception("Todos os campos são obrigatórios.");
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
                    ui.UserId == user.Id &&
                    ui.InstitutionId == institutionId
                );

                if (!hasInstitution)
                {
                    throw new Exception("Usuário não pertence à instituição especificada.");
                }

                user.AccessDate = DateTime.Now;
                _context.ST_USERS.Update(user);
                await _context.SaveChangesAsync();

                user.PasswordHash = null;
                user.PasswordSalt = null;
                user.InstitutionId = institutionId ?? credentials.InstitutionId;
                string Token = CreateToken(user);

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
                if (await ExistUser(user.Email))
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

                return Ok(EnvelopeFactory.factoryEnvelope(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("photoUrl", user.PhotoUrl),
                new Claim("institutionId", user.InstitutionId.ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("TokenConfiguration:Key").Value!)
            );

            SigningCredentials creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> ExistUser(string email)
        {
            if (await _context.ST_USERS.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        private int GetUserId()
        {
            return int.Parse(
                _httpContextAccessor.HttpContext?.User.FindFirstValue("id")!
            );
        }
    }
}
