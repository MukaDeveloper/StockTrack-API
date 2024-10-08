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
            Movimentation mov =
                new()
                {
                    Name = name ?? "Nova área",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    AreaId = areaId,
                    Type = MovimentationType.AREA,
                    Event = MovimentationEvent.ENTRY,
                    Reason = MovimentationReason.INSERTION, // Inserção de nova área
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
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
            Movimentation mov =
                new()
                {
                    Name = name ?? "Área editada",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    AreaId = areaId,
                    Event = MovimentationEvent.EDIT,
                    Type = MovimentationType.AREA,
                    Reason = MovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> DeleteArea(
            int institutionId,
            int areaId,
            string userName,
            string name,
            string description = "Remoção de área"
        )
        {
            Movimentation mov =
                new()
                {
                    Name = name ?? "Área excluída",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    AreaId = areaId,
                    Event = MovimentationEvent.EXIT,
                    Type = MovimentationType.AREA,
                    Reason = MovimentationReason.REMOVED,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> AddWarehouse(
            int warehouseId,
            string wareHouseName,
            string userName,
            int institutionId,
            string description = "Adição de almoxarifado"
        )
        {
            Movimentation mov =
                new()
                {
                    Name = wareHouseName ?? "Novo almoxarifado",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    WarehouseId = warehouseId,
                    Event = MovimentationEvent.ENTRY,
                    Type = MovimentationType.WAREHOUSE,
                    Reason = MovimentationReason.INSERTION,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
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
            Movimentation mov =
                new()
                {
                    Name = name ?? "Almoxarifado editado",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    WarehouseId = warehouseId,
                    Event = MovimentationEvent.EDIT,
                    Type = MovimentationType.WAREHOUSE,
                    Reason = MovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> DeleteWarehouse(
            int institutionId,
            int warehouseId,
            string userName,
            string name,
            string description = "Remoção de almoxarifado"
        )
        {
            Movimentation mov =
                new()
                {
                    Name = name ?? "Almoxarifado excluído",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    WarehouseId = warehouseId,
                    Event = MovimentationEvent.EXIT,
                    Type = MovimentationType.WAREHOUSE,
                    Reason = MovimentationReason.REMOVED,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = 1,
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
            float quantity = 1
        )
        {
            Movimentation mov =
                new()
                {
                    Name = name ?? "Adição de material",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    MaterialId = materialId,
                    Event = MovimentationEvent.ENTRY,
                    Type = MovimentationType.MATERIAL,
                    Reason = MovimentationReason.INSERTION,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = quantity,
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
            float quantity = 1
        )
        {
            Movimentation mov =
                new()
                {
                    Name = name ?? "Edição de material",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    MaterialId = materialId,
                    Event = MovimentationEvent.EDIT,
                    Type = MovimentationType.MATERIAL,
                    Reason = MovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = quantity,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> AddUser(
            int institutionId,
            int userId,
            string userName,
            string movBy
        )
        {
            Movimentation mov =
                new()
                {
                    Name = userName ?? "Adição de usuário",
                    InstitutionId = institutionId,
                    MovimentationBy = movBy,
                    UserId = userId,
                    Event = MovimentationEvent.ENTRY,
                    Type = MovimentationType.USER,
                    Reason = MovimentationReason.INSERTION,
                    Date = DateTime.Now,
                    Description = "Adição de usuário",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> UpdateUser(
            int institutionId,
            int userId,
            string userName,
            string movBy
        )
        {
            Movimentation mov =
                new()
                {
                    Name = userName ?? "Edição de usuário",
                    InstitutionId = institutionId,
                    MovimentationBy = movBy,
                    UserId = userId,
                    Event = MovimentationEvent.EDIT,
                    Type = MovimentationType.MATERIAL,
                    Reason = MovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = "Edição de usuário",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }
    }
}
