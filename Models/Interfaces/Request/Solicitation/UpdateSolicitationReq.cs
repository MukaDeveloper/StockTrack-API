using StockTrack_API.Models.Interfaces.Request;

namespace StockTrack_API.Models
{
    public class UpdateSolicitationReq
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<SolicitationMaterialsReq> Items { get; set; } = new List<SolicitationMaterialsReq>();
        public DateTime MovimentedAt { get; set; }
    }
}
