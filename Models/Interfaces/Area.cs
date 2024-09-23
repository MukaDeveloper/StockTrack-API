using System.Text.Json.Serialization;

namespace StockTrack_API.Models.Interfaces
{
    public class Area : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        [JsonIgnore]
        public Institution? Institution { get; set; }
    }
}
