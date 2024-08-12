using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockTrack_API.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Material> Materials { get; set; } = new List<Material>();

        public int AreaId { get; set; }
        [JsonIgnore]
        public Area? Area { get; set; }

    }
}