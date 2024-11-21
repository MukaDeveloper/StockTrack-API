using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class Solicitation
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public List<SolicitationMaterials> Items { get; set; } = new List<SolicitationMaterials>();

        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public UserInstitution UserInstitution { get; set; }

        public DateTime SolicitedAt { get; set; } = DateTime.UtcNow;
        public ESolicitationStatus Status { get; set; }
    }
}
