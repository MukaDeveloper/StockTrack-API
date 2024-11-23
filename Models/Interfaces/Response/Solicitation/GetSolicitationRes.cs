using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class GetSolicitationRes
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public required List<SolicitationMaterials> Items { get; set; }

        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public UserInstitution? UserInstitution { get; set; }

        public DateTime SolicitedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
