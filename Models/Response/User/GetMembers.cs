using System.ComponentModel.DataAnnotations.Schema;
using StockTrack_API.Models.Enums;

namespace StockTrack_API.Models.Response
{
    public class GetMembersRes
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Role { get; set; }
    }
}
