using System.Text.Json.Serialization;

namespace StockTrack_API.Models.Interfaces
{
    public class Warehouse : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Material> Materials { get; set; } = new List<Material>();

        public int AreaId { get; set; }
        public Area? Area { get; set; }

    }
}