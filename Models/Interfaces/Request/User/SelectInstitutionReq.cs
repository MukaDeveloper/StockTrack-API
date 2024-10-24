namespace StockTrack_API.Models.Interfaces.Request
{
    public class SelectInstitutionReq
    {
        public int UserId { get; set; }
        public int InstitutionId { get; set; }
    }
}