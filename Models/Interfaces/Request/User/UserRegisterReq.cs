using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class UserRegisterReq
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
