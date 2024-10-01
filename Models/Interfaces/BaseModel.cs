namespace StockTrack_API.Models.Interfaces
{
    public abstract class BaseModel
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UpdatedBy { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool Active { get; set; } = true;

        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }
    }
}
