using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class UpdateMaterialReq
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Manufacturer { get; set; }
        public string? MaterialType { get; set; } // Deve ser um valor válido de EMaterialType
        public string? Measure { get; set; }
        public int? RecordNumber { get; set; }
        public int? WarehouseId { get; set; } // ID do almoxarifado a ser vinculado ao material
        public bool? Active { get; set; }
    }
}