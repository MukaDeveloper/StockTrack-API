using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class UpdateSolicitationReq
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime MovimentedAt { get; set; }
    }
}
