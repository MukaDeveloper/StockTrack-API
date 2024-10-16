using System.Text.Json.Serialization;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces
{
    public class MaterialStatus
    {
        public EMaterialStatus Status { get; set; }
        public float Quantity { get; set; }

        // Relacionamento com Material
        public int MaterialId { get; set; }
        [JsonIgnore]
        public Material? Material { get; set; }
    }
}