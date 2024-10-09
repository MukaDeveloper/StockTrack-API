using System.Text.Json.Serialization;

namespace StockTrack_API.Models.Interfaces
{
    public class WarehouseUsers
    {
        public int WarehouseId { get; set; }
        [JsonIgnore]
        public Warehouse? Warehouse { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}