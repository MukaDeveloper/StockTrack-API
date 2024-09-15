using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces;

namespace StockTrack_API.Services
{
    public class MovimentationService
    {
        private readonly DataContext _context;

        public MovimentationService(DataContext context)
        {
            _context = context;
        }

        public async Task<Movimentation> AddArea(
            int institutionId,
            int areaId,
            int userId,
            string description = "Área adicionada"
        )
        {
            Movimentation mov =
                new()
                {
                    InstitutionId = institutionId,
                    AreaId = areaId,
                    Type = MovimentationType.Entry,
                    Reason = MovimentationReason.Insertion, // Inserção de nova área
                    Date = DateTime.Now,
                    UserId = userId,
                    Description = description,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }
    }
}
