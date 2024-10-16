namespace StockTrack_API.Models.Interfaces.Entities
{
    public class MovimentationReasonEntity
    {
        // Entry reasons
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
