using System.ComponentModel.DataAnnotations;

namespace StockTrack_API.Models.Interfaces
{
    public abstract class BaseModel
    {
        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool Active { get; set; } = true;
    }
}
