using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models.Interfaces.Request
{
    public class AddUserInstitutionReq
    {
        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public string? UserRole { get; set; }
    }
}