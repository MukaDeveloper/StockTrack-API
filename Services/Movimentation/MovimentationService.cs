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
            string userName,
            string name,
            string description = "Adição de área"
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Nova área",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                AreaId = areaId,
                Event = MovimentationEvent.Area,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Insertion, // Inserção de nova área
                Date = DateTime.Now,
                Description = description,
                Quantity = 1
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> UpdateArea(
            int institutionId,
            int areaId,
            string userName,
            string name,
            string description = "Edição de área"
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Área editada",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                AreaId = areaId,
                Event = MovimentationEvent.Area,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Edit,
                Date = DateTime.Now,
                Description = description,
                Quantity = 1,
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> AddWarehouse(
            int institutionId,
            int warehouseId,
            string userName,
            string name,
            string description = "Adição de almoxarifado"
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Novo almoxarifado",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                WarehouseId = warehouseId,
                Event = MovimentationEvent.Warehouse,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Insertion,
                Date = DateTime.Now,
                Description = description,
                Quantity = 1
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> UpdateWarehouse(
            int institutionId,
            int warehouseId,
            string userName,
            string name,
            string description = "Edição de almoxarifado"
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Almoxarifado editado",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                WarehouseId = warehouseId,
                Event = MovimentationEvent.Warehouse,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Edit,
                Date = DateTime.Now,
                Description = description,
                Quantity = 1
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> AddMaterial(
            int institutionId,
            int materialId,
            string userName,
            string name,
            string description = "Adição de material",
            decimal quantity = 1
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Adição de material",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                MaterialId = materialId,
                Event = MovimentationEvent.Material,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Insertion,
                Date = DateTime.Now,
                Description = description,
                Quantity = quantity
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> UpdateMaterial(
            int institutionId,
            int materialId,
            string userName,
            string name,
            string description = "Edição de material",
            decimal quantity = 1
        )
        {
            Movimentation mov = new()
            {
                Name = name ?? "Edição de material",
                InstitutionId = institutionId,
                MovimentationBy = userName,
                MaterialId = materialId,
                Event = MovimentationEvent.Material,
                Type = MovimentationType.Entry,
                Reason = MovimentationReason.Edit,
                Date = DateTime.Now,
                Description = description,
                Quantity = quantity
            };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }
    }
}
