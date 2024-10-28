namespace StockTrack_API.Models.Interfaces.Request
{
    public class ConfirmEmailRequest
    {
        public required string Token { get; set; }
        public int UserId { get; set; }
    }
}
