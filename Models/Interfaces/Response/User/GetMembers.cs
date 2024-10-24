namespace StockTrack_API.Models.Interfaces.Response
{
    public class GetMembersRes
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Role { get; set; }
        public bool Verified { get; set; }
    }
}
