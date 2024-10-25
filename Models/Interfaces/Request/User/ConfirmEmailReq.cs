namespace StockTrack_API.Models.Interfaces.Request
{
    public class ConfirmEmailRequest
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
