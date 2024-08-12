using StockTrack_API.Models.Enums;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int InstitutionId { get; set; }
        [JsonIgnore]
        public Institution? Institution { get; set; }

        public UserType UserType { get; set; }

        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; } 
        public byte[]? PasswordSalt { get; set; } 
        public string PhotoUrl { get; set; } = string.Empty;

        public DateTime? AccessDate { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}