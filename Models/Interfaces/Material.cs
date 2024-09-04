using System.Text.Json.Serialization;
using StockTrack_API.Models.Enums;

namespace StockTrack_API.Models.Interfaces
{
    public class Material : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int RecordNumber { get; set; }
        public MaterialType MaterialType { get; set; }

        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        [JsonIgnore]
        public Warehouse? Warehouse { get; set; }


    }
}