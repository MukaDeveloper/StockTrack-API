using StockTrack_API.Models.Enums;

namespace StockTrack_API.Models.Interfaces
{
    public class UserInstitution
    {
        public int UserId { get; set; }
        public string UserName { get; set;} = string.Empty;
        public User? User { get; set; }

        public int InstitutionId { get; set; }
        public string InstitutionName { get; set;} = string.Empty;
        public Institution? Institution { get; set; }

        public UserType? UserType { get; set; }
    }
}