namespace StockTrack_API.Models.Interfaces
{
    public class WarehouseUsers
    {
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}