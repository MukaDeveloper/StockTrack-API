using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class UserAuthReq
    {
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;

        [NotMapped]
        public int InstitutionId { get; set; }
    }
}
