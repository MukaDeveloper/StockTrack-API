using System.ComponentModel.DataAnnotations.Schema;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class CreateSolicitationReq
    {
        public string Description { get; set; } = string.Empty;
        public List<SolicitationMaterialsReq> Items { get; set; } = new List<SolicitationMaterialsReq>();
        public DateTime ExpectReturnAt { get; set; } = DateTime.Now.AddDays(7);
    }

    public class SolicitationMaterialsReq
    {
        public float Quantity { get; set; }
        public int MaterialId { get; set; }
    }
}