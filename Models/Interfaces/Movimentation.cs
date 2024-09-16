namespace StockTrack_API.Models.Interfaces
{
    public class Movimentation
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? MaterialId { get; set; }
        public Material? Material { get; set; }

        // Foreign key to Warehouse or Area (optional)
        public int? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public int? AreaId { get; set; }
        public Area? Area { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public MovimentationType Type { get; set; }
        public MovimentationReason Reason { get; set; }

        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
