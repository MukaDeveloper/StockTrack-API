using StockTrack_API.Data;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces.Enums;

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
                    Type = EMovimentationType.AREA,
                    Event = EMovimentationEvent.ENTRY,
                    Reason = EMovimentationReason.INSERTION, // Inserção de nova área
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
                    Event = EMovimentationEvent.EDIT,
                    Type = EMovimentationType.AREA,
                    Reason = EMovimentationReason.EDIT,
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
                    Event = EMovimentationEvent.EXIT,
                    Type = EMovimentationType.AREA,
                    Reason = EMovimentationReason.REMOVED,
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
                    Event = EMovimentationEvent.ENTRY,
                    Type = EMovimentationType.WAREHOUSE,
                    Reason = EMovimentationReason.INSERTION,
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
                    Event = EMovimentationEvent.EDIT,
                    Type = EMovimentationType.WAREHOUSE,
                    Reason = EMovimentationReason.EDIT,
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
                    Event = EMovimentationEvent.EXIT,
                    Type = EMovimentationType.WAREHOUSE,
                    Reason = EMovimentationReason.REMOVED,
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
                    Event = EMovimentationEvent.ENTRY,
                    Type = EMovimentationType.MATERIAL,
                    Reason = EMovimentationReason.INSERTION,
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
                    Event = EMovimentationEvent.EDIT,
                    Type = EMovimentationType.MATERIAL,
                    Reason = EMovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = quantity,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> DeleteMaterial(
            int institutionId,
            int materialId,
            string userName,
            string name,
            string description = "Remoção de almoxarifado",
            float quantity = 1
        )
        {
            Movimentation mov =
                new()
                {
                    Name = name ?? "Baixa de material",
                    InstitutionId = institutionId,
                    MovimentationBy = userName,
                    MaterialId = materialId,
                    Event = EMovimentationEvent.EXIT,
                    Type = EMovimentationType.MATERIAL,
                    Reason = EMovimentationReason.REMOVED,
                    Date = DateTime.Now,
                    Description = description,
                    Quantity = quantity
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
                    Event = EMovimentationEvent.ENTRY,
                    Type = EMovimentationType.USER,
                    Reason = EMovimentationReason.INSERTION,
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
                    Event = EMovimentationEvent.EDIT,
                    Type = EMovimentationType.MATERIAL,
                    Reason = EMovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = "Edição de usuário",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> RemoveUser(
            int institutionId,
            int userId,
            string userName,
            string movBy
        )
        {
            Movimentation mov =
                new()
                {
                    Name = userName ?? "Remoção de usuário",
                    InstitutionId = institutionId,
                    MovimentationBy = movBy,
                    UserId = userId,
                    Event = EMovimentationEvent.EXIT,
                    Type = EMovimentationType.USER,
                    Reason = EMovimentationReason.REMOVED,
                    Date = DateTime.Now,
                    Description = "Remoção de usuário",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> NewSolicitation(
            int institutionId,
            int solicitationId,
            string solicitationName,
            string movBy
        )
        {
            Movimentation mov =
                new()
                {
                    Name = solicitationName ?? "Nova solicitação",
                    InstitutionId = institutionId,
                    MovimentationBy = movBy,
                    SolicitationId = solicitationId,
                    Event = EMovimentationEvent.ENTRY,
                    Type = EMovimentationType.SOLICITATION,
                    Reason = EMovimentationReason.INSERTION,
                    Date = DateTime.Now,
                    Description = "Nova solicitação",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }

        public async Task<Movimentation> UpdateSolicitation(
            int institutionId,
            int solicitationId,
            string solicitationName,
            string movBy
        )
        {
            Movimentation mov =
                new()
                {
                    Name = solicitationName ?? "Edição de solicitação",
                    InstitutionId = institutionId,
                    MovimentationBy = movBy,
                    SolicitationId = solicitationId,
                    Event = EMovimentationEvent.EDIT,
                    Type = EMovimentationType.SOLICITATION,
                    Reason = EMovimentationReason.EDIT,
                    Date = DateTime.Now,
                    Description = "Edição de solicitação",
                    Quantity = 1,
                };

            _context.ST_MOVIMENTATIONS.Add(mov);
            await _context.SaveChangesAsync();

            return mov;
        }
    }
}
