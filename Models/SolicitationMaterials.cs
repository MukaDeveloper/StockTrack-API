using System.Text.Json.Serialization;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces
{
    public class SolicitationMaterials
    {
        public float Quantity { get; set; }
        public ESolicitationStatus Status { get; set; }

        public int MaterialId { get; set; }
        [JsonIgnore]
        public Material? Material { get; set; }

        public int SolicitationId { get; set; }
        [JsonIgnore]
        public Solicitation? Solicitation { get; set; }
    }
}