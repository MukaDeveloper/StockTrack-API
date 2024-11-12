namespace StockTrack_API.Models.Interfaces.Request
{
    public class UpdateUserInstitutionReq
    {
        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public string? UserRole { get; set; }
    }
}