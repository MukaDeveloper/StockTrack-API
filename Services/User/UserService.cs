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

        public UserService(
            DataContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            EmailService emailService
        )
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public User GetUser()
        {
            string? context =
                _httpContextAccessor.HttpContext?.User.FindFirstValue("id")
                ?? throw new Exception("Requisição inválida.");

            int userId = int.Parse(context);

            User? user =
                _context.ST_USERS.FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("Usuário não encontrado.");

            return user;
        }

        public User GetUserByEmail(string email)
        {
            // string? email = _httpContextAccessor.HttpContext?.User.FindFirstValue("email");

            if (email == null)
                throw new Exception("Requisição inválida.");

            User? user =
                _context.ST_USERS.FirstOrDefault(u => u.Email == email)
                ?? throw new Exception("Usuário não encontrado.");

            return user;
        }

        public (User, UserInstitution) GetUserAndInstitution(int institutionId)
        {
            string? context =
                _httpContextAccessor.HttpContext?.User.FindFirstValue("id")
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

        public string CreateToken(User user, UserInstitution? userInstitution)
        {
            List<Claim> claims =
                new()
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("email", user.Email),
                    new Claim("photoUrl", user.PhotoUrl),
                    new Claim("verified", user.Verified.ToString()),
                    new Claim("verifiedScheduled", user.VerifiedScheduled.ToString() ?? ""),
                    new Claim("active", userInstitution?.Active.ToString() ?? ""),
                    new Claim("institutionId", userInstitution?.InstitutionId.ToString() ?? ""),
                    new Claim("role", userInstitution?.UserRole.ToString() ?? ""),
                };

            SymmetricSecurityKey key =
                new(
                    Encoding.UTF8.GetBytes(
                        _configuration.GetSection("TokenConfiguration:Key").Value!
                    )
                );

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor =
                new()
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

        public async Task<string> SendConfirmationEmail(User user)
        {
            // Gera um token aleatório
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(64);
            string token = ConvertToBase64Url(tokenBytes); // Converte para Base64 URL-safe

            // Gera o hash do token URL-safe para armazenamento no banco
            string tokenHash = Cryptography.HashToken(token);

            // Gera a URL com a versão original do token (segura para URL)
            string url =
                $"{_configuration.GetSection("FrontEndURL:Url").Value!}/confirm?token={tokenHash}&uid={user.Id}";

            // Envia o e-mail
            await _emailService.SendEmail(
                user.Email,
                "Confirmação de cadastro",
                EmailBody.ConfirmationEmail(user.Name, url)
            );

            // Retorna o hash que será armazenado no banco de dados
            return tokenHash;
        }

        private string ConvertToBase64Url(byte[] input)
        {
            return Convert
                .ToBase64String(input)
                .Replace('+', '-') // Substitui '+' por '-'
                .Replace('/', '_') // Substitui '/' por '_'
                .TrimEnd('=');
        }
    }
}
