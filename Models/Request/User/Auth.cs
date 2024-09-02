using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Request.User
{
    public class Auth
    {
        public string Email { get; set; } = string.Empty;
        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
        [NotMapped]
        public int InstitutionId { get; set; }
    }
}