using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces;

namespace StockTrack_API.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUser()
        {
            string? context = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");

            if (context == null)
            {
                throw new Exception("Requisição inválida.");
            }

            int userId = int.Parse(context);

            User? user = _context.ST_USERS.FirstOrDefault(u => u.Id == userId);

            if (user == null) {
              throw new Exception("Usuário não encontrado.");
            }

            return user;
        }
        
        public (User, UserInstitution) GetUserAndInstitution(int institutionId)
        {
            string? context = _httpContextAccessor.HttpContext?.User.FindFirstValue("id");

            if (context == null)
            {
                throw new Exception("Requisição inválida.");
            }

            int userId = int.Parse(context);

            User? user = _context.ST_USERS.FirstOrDefault(u => u.Id == userId);
            UserInstitution? userInstitution = _context.ST_USER_INSTITUTIONS.FirstOrDefault(ui =>
                ui.UserId == userId && ui.InstitutionId == institutionId
            );

            if (user == null) {
              throw new Exception("Usuário não encontrado.");
            }

            if (userInstitution == null) {
              throw new Exception("Usuário não pertence a instituição.");
            }

            return (user, userInstitution);
        }

        public string CreateToken(User user, UserInstitution userInstitution)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("photoUrl", user.PhotoUrl),
                new Claim("institutionId", userInstitution.InstitutionId.ToString()),
                new Claim("role", userInstitution.UserRole.ToString()),
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

        public async Task<bool> ExistUser(string email)
        {
            if (await _context.ST_USERS.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
