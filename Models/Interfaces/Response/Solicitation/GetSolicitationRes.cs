using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class GetSolicitationRes
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public required List<GetSolicitationItemsRes> Items { get; set; }

        public int UserId { get; set; }
        public int InstitutionId { get; set; }
        public required UserInstitutionRes UserInstitution { get; set; }

        public DateTime SolicitedAt { get; set; }
        public DateTime ExpectReturnAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class GetSolicitationItemsRes
    {
        public int MaterialId { get; set; }
        public string? MaterialName { get; set; } = string.Empty;
        public float Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UserInstitutionRes {
        public bool Active { get; set; }
        public string UserRole { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
