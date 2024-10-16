using System.Text.Json.Serialization;

namespace StockTrack_API.Models
{
    public class MaterialWarehouses
    {
        public int MaterialId { get; set; }
        [JsonIgnore]
        public Material? Material { get; set; }

        public int WarehouseId { get; set; }
        [JsonIgnore]
        public Warehouse? Warehouse { get; set; }
    }
}