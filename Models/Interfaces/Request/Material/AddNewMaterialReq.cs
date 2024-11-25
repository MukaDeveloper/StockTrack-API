using System.ComponentModel.DataAnnotations.Schema;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class AddNewMaterialReq
    {
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string MaterialType { get; set; } = "LOAN";
        public string Measure { get; set; } = "UN";
        public string Name { get; set; } = string.Empty;
        public int quantity { get; set; }
        public int RecordNumber { get; set; }
        public int WarehouseId { get; set; }
    }
}