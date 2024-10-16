namespace StockTrack_API.Models.Interfaces.Request
{
    public class UpdateAreaReq
    {
        public int? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}