using System.ComponentModel.DataAnnotations.Schema;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class AddNewMaterialReq
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int RecordNumber { get; set; }
        public EMaterialType MaterialType { get; set; }
        // public List<MaterialStatus> Status { get; set; } = new List<MaterialStatus>();
        public string Measure { get; set; } = string.Empty;
    }
}