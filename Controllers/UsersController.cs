using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Enums;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(DataContext context, 
        IConfiguration configuration, 
        IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            try
            {
                User user = await _context.ST_USERS
                   .FirstOrDefaultAsync(x => x.Id == userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync(User credentials)
        {
            if (credentials.Email == null || credentials.PasswordString == null)
            {
                return BadRequest("Email e senha são obrigatórios.");
            }
            try
            {
                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(credentials.Email.ToLower()));

                if (user == null || !Cryptography.VerifyPasswordHash(credentials.PasswordString, user.PasswordHash!, user.PasswordSalt!))
                {
                    throw new Exception("Usuário ou senha incorreto(s).");
                }
                else
                {
                    user.AccessDate = DateTime.Now;
                    _context.ST_USERS.Update(user);
                    await _context.SaveChangesAsync();

                    user.PasswordHash = null;
                    user.PasswordSalt = null;
                    user.Token = CreateToken(user);

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            try
            {

                if (await ExistUser(user.Email))
                    throw new Exception("Usuário já existente.");

                Cryptography.CreatePasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _context.ST_USERS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
            };

            SymmetricSecurityKey key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration.GetSection("TokenConfiguration:Key").Value!));

            SigningCredentials creds =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds
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
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private async Task<bool> VerifyAdmin()
        {
            if (await _context.ST_USERS.AnyAsync(x => x.UserType == UserType.ADMIN))
            {
                return true;
            }
            return false;
        }
    }
}