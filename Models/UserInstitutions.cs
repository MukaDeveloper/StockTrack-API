namespace StockTrack_API.Models
{
    public class UserInstitution
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }
    }
}