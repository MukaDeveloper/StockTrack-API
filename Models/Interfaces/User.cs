using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using StockTrack_API.Models.Enums;

namespace StockTrack_API.Models.Interfaces
{
    public class User
    {
        public bool Active { get; set; } = true;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? AccessDate { get; set; }

        [JsonIgnore]
        public List<UserInstitution>? UserInstitutions { get; set; }
        [JsonIgnore]
        public List<WarehouseUsers>? WarehouseUsers { get; set; }

        [NotMapped, JsonIgnore]
        public UserRole Role { get; set; }

        [NotMapped, JsonIgnore]
        public string PasswordString { get; set; } = string.Empty;

        [NotMapped, JsonIgnore]
        public int InstitutionId { get; set; }

        [NotMapped]
        public string Token { get; set; } = string.Empty;
    }
}
