using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrack_API.Models.Request.Warehouse
{
    public class UpdateReq
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int? AreaId { get; set; }
        public int? AreaIdAfter { get; set; }
    }
}