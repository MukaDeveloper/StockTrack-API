using System.Text.Json.Serialization;

namespace StockTrack_API.Models.Interfaces
{
    public class Warehouse : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<MaterialWarehouses>? MaterialWarehouses { get; set; }
        public List<WarehouseUsers>? WarehouseUsers { get; set; }

        public int AreaId { get; set; }
        public Area? Area { get; set; }
    }
}
