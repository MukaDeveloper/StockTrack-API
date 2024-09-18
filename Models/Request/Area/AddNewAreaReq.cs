using System.ComponentModel.DataAnnotations.Schema;

namespace StockTrack_API.Models.Request.Area
{
    public class AddNewAreaReq
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public int InstitutionId { get; set; }
    }
}
