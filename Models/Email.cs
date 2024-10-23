namespace StockTrack_API.Models
{
    public class Email 
    {
        public required string Sender { get; set; }
        public required string SenderPassword { get; set; }
        public required string Receiver { get; set; }
        public string? ReceiverCopy { get; set; }
        public required string PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
    }
}
