using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Request.User
{
    public class UserRegisterReq
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
    }
}
