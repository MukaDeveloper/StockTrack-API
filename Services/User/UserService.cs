using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Utils;

namespace StockTrack_API.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailService _emailService;

        public UserService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public User GetUser()
        {
            string? context = _httpContextAccessor.HttpContext?.User.FindFirstValue("id")
                ?? throw new Exception("Requisição inválida.");

            int userId = int.Parse(context);

            User? user = _context.ST_USERS.FirstOrDefault(u => u.Id == userId) 
                ?? throw new Exception("Usuário não encontrado.");

            return user;
        }

        public User GetUserByEmail(string email)
        {
            // string? email = _httpContextAccessor.HttpContext?.User.FindFirstValue("email");

            if (email == null)
                throw new Exception("Requisição inválida.");

            User? user = _context.ST_USERS.FirstOrDefault(u => u.Email == email) 
                ?? throw new Exception("Usuário não encontrado.");

            return user;
        }

        public (User, UserInstitution) GetUserAndInstitution(int institutionId)
        {
            string? context = _httpContextAccessor.HttpContext?.User.FindFirstValue("id")
                ?? throw new Exception("Requisição inválida.");

            int userId = int.Parse(context);

            User? user = _context.ST_USERS.FirstOrDefault(u => u.Id == userId);
            UserInstitution? userInstitution = _context.ST_USER_INSTITUTIONS.FirstOrDefault(ui =>
                ui.UserId == userId && ui.InstitutionId == institutionId
            );

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (userInstitution == null)
            {
                throw new Exception("Usuário não pertence a instituição.");
            }

            return (user, userInstitution);
        }

        public string CreateToken(User user, UserInstitution userInstitution)
        {
            List<Claim> claims = new()
            {
                new Claim("active", userInstitution.Active.ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("email", user.Email),
                new Claim("photoUrl", user.PhotoUrl),
                new Claim("institutionId", userInstitution.InstitutionId.ToString()),
                new Claim("role", userInstitution.UserRole.ToString()),
                new Claim("verified", user.Verified.ToString())
            };

            SymmetricSecurityKey key = new(
                Encoding.UTF8.GetBytes(_configuration.GetSection("TokenConfiguration:Key").Value!)
            );

            SigningCredentials creds = new(
                key,
                SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new();
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

        public async Task<byte[]> SendConfirmationEmail(string userEmail, string userName)
        {
            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            Cryptography.CryptographyHashSHA256(token, out byte[] hash);

            // ENVIAR O EMAIL AQUI
            string url = $"{_configuration.GetSection("FrontEndURL:Url").Value!}/confirm?token={hash}";
            Email email = new()
            {
                Sender = "noreply.stocktrack@gmail.com",
                SenderPassword = "bukk dzhp gecy cyub",
                Receiver = userEmail,
                Subject = "Confirmação de E-mail",
                Message = EmailBody.ConfirmationEmail(userName, url),
                PrimaryDomain = "smtp.gmail.com",
                PrimaryPort = 587,
            };

            await _emailService.SendEmail(email);

            return hash;
        }
    }
}
