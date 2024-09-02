using StockTrack_API.Models.Enums;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Interfaces
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserType? UserType { get; set; }
        public byte[]? PasswordHash { get; set; } 
        public byte[]? PasswordSalt { get; set; } 
        public string PhotoUrl { get; set; } = string.Empty;
        public DateTime? AccessDate { get; set; }

        [JsonIgnore]
        public List<UserInstitution>? UserInstitutions { get; set; }

        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
        [NotMapped]
        public int InstitutionId { get; set; }
        [NotMapped]
        public string Token { get; set; } = string.Empty;
    }
}