using StockTrack_API.Models.Enums;

namespace StockTrack_API.Models.Request.User
{
    public class AddUserInstitutionReq
    {
        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public string? UserRole { get; set; }
    }
}