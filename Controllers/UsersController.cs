using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Utils;

namespace StockTrack_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Authenticate(User credenciais)
        {
            try
            {
                User? user = await _context.ST_USERS.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(credenciais.Name.ToLower()));

                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }
                else if (!Cryptography.VerificarPasswordHash(credenciais.PasswordString, user.PasswordHash, user.PasswordSalt))
                {
                    throw new Exception("Senha incorreta.");
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            SymmetricSecurityKey key = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration.GetSection("TokenConfiguration:Key").Value!));

            SigningCredentials creds = 
                new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}