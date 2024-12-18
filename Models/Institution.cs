using System.Text.Json.Serialization;

namespace StockTrack_API.Models
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;

        public List<UserInstitution>? Users { get; set; }

        [JsonIgnore]
        public List<Area>? Areas { get; set; } = new List<Area>();
    }
}