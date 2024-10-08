using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockTrack_API.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public int InstitutionId { get; set; }
        [JsonIgnore]
        public Institution? Institution { get; set; }
    }
}