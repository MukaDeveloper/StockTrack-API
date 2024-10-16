using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class Material : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        
        public string Manufacturer { get; set; } = string.Empty;
        public int RecordNumber { get; set; }
        public EMaterialType MaterialType { get; set; }

        public List<MaterialStatus> Status { get; set; } = new List<MaterialStatus>();

        [NotMapped]
        public float Quantity => Status.Sum(s => s.Quantity);
        // UN | KG | L
        public string Measure { get; set; } = string.Empty;
        public List<MaterialWarehouses> MaterialWarehouses { get; set; } = new List<MaterialWarehouses>();
    }
}