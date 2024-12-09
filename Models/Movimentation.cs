using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class Movimentation
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MovimentationBy { get; set; } = string.Empty;

        public int? MaterialId { get; set; }
        public Material? Material { get; set; }

        public int? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public int? AreaId { get; set; }
        public Area? Area { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? SolicitationId { get; set; }
        public Solicitation? Solicitation { get; set; }

        public EMovimentationEvent Event { get; set; }
        public EMovimentationType Type { get; set; }
        public EMovimentationReason Reason { get; set; }

        public float Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
