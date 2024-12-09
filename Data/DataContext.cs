using Microsoft.EntityFrameworkCore;
using StockTrack_API.Models;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Interfaces.Entities;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Utils;

namespace StockTrack_API.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public required DbSet<Area> ST_AREAS { get; set; }
        public required DbSet<Institution> ST_INSTITUTIONS { get; set; }
        public required DbSet<Material> ST_MATERIALS { get; set; }
        public required DbSet<MaterialStatus> ST_MATERIALS_STATUS { get; set; }
        public required DbSet<Movimentation> ST_MOVIMENTATIONS { get; set; }
        public required DbSet<User> ST_USERS { get; set; }
        public required DbSet<Warehouse> ST_WAREHOUSES { get; set; }
        public required DbSet<UserInstitution> ST_USER_INSTITUTIONS { get; set; }
        public required DbSet<MaterialWarehouses> ST_MATERIAL_WAREHOUSES { get; set; }
        public required DbSet<WarehouseUsers> ST_WAREHOUSE_USERS { get; set; }
        public required DbSet<Solicitation> ST_SOLICITATIONS { get; set; }
        public required DbSet<SolicitationMaterials> ST_SOLICITATION_MATERIALS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().ToTable("ST_AREAS");
            modelBuilder.Entity<Institution>().ToTable("ST_INSTITUTIONS");
            modelBuilder.Entity<Material>().ToTable("ST_MATERIALS");
            modelBuilder.Entity<Movimentation>().ToTable("ST_MOVIMENTATIONS");
            modelBuilder.Entity<User>().ToTable("ST_USERS");
            modelBuilder.Entity<Warehouse>().ToTable("ST_WAREHOUSES");
            modelBuilder.Entity<MaterialWarehouses>().ToTable("ST_MATERIAL_WAREHOUSES");
            modelBuilder.Entity<WarehouseUsers>().ToTable("ST_WAREHOUSE_USERS");

            modelBuilder.Entity<Solicitation>().ToTable("ST_SOLICITATIONS");
            modelBuilder.Entity<SolicitationMaterials>().ToTable("ST_SOLICITATION_MATERIALS");

            modelBuilder
                .Entity<UserInstitution>()
                .HasKey(ui => new { ui.UserId, ui.InstitutionId });

            modelBuilder
                .Entity<UserInstitution>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInstitutions)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<UserInstitution>()
                .HasOne(ui => ui.Institution)
                .WithMany(u => u.Users)
                .HasForeignKey(ui => ui.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<UserInstitution>()
                .HasMany(ui => ui.Solicitations)
                .WithOne(s => s.UserInstitution)
                .HasForeignKey(s => new { s.UserId, s.InstitutionId })
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Area>()
                .HasOne(a => a.Institution)
                .WithMany(i => i.Areas)
                .HasForeignKey(a => a.InstitutionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Warehouse>()
                .HasOne(a => a.Area)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar o relacionamento um-para-muitos entre Material e MaterialStatus
            modelBuilder
                .Entity<Material>()
                .HasMany(m => m.Status)
                .WithOne(ms => ms.Material)
                .HasForeignKey(ms => ms.MaterialId);

            modelBuilder.Entity<MaterialStatus>().HasKey(ui => new { ui.MaterialId, ui.Status });

            base.OnModelCreating(modelBuilder);

            // Movimentations
            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Area)
                .WithMany()
                .HasForeignKey(m => m.AreaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Warehouse)
                .WithMany()
                .HasForeignKey(m => m.WarehouseId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Material)
                .WithMany()
                .HasForeignKey(m => m.MaterialId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Solicitation)
                .WithMany()
                .HasForeignKey(m => m.SolicitationId)
                .OnDelete(DeleteBehavior.SetNull);

            // MaterialWarehouses
            modelBuilder
                .Entity<MaterialWarehouses>()
                .HasKey(mw => new { mw.MaterialId, mw.WarehouseId });
            modelBuilder
                .Entity<MaterialWarehouses>()
                .HasOne(mw => mw.Material)
                .WithMany(m => m.MaterialWarehouses)
                .HasForeignKey(mw => mw.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<MaterialWarehouses>()
                .HasOne(mw => mw.Warehouse)
                .WithMany(w => w.MaterialWarehouses)
                .HasForeignKey(mw => mw.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // WarehouseUsers
            modelBuilder.Entity<WarehouseUsers>().HasKey(mw => new { mw.UserId, mw.WarehouseId });
            modelBuilder
                .Entity<WarehouseUsers>()
                .HasOne(mw => mw.Warehouse)
                .WithMany(w => w.Warehousemans)
                .HasForeignKey(mw => mw.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder
                .Entity<WarehouseUsers>()
                .HasOne(mw => mw.User)
                .WithMany(m => m.WarehouseUsers)
                .HasForeignKey(mw => mw.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Solicitações
            modelBuilder.Entity<Solicitation>().HasKey(s => s.Id);

            modelBuilder.Entity<Solicitation>()
                .HasOne(s => s.UserInstitution)
                .WithMany(ui => ui.Solicitations)
                .HasForeignKey(s => new { s.UserId, s.InstitutionId });

            modelBuilder.Entity<Solicitation>()
                .HasMany(s => s.Items)
                .WithOne(sm => sm.Solicitation)
                .HasForeignKey(sm => sm.SolicitationId);

            // Items da solicitação
            modelBuilder.Entity<SolicitationMaterials>().HasKey(sm => new { sm.MaterialId, sm.SolicitationId });

            modelBuilder.Entity<SolicitationMaterials>()
                .HasOne(sm => sm.Material)
                .WithMany()
                .HasForeignKey(sm => sm.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SolicitationMaterials>()
                .HasOne(sm => sm.Solicitation)
                .WithMany(s => s.Items)
                .HasForeignKey(sm => sm.SolicitationId)
                .OnDelete(DeleteBehavior.Cascade);

            Cryptography.CryptographyHashHmac("admin12345", out byte[] hash, out byte[] salt);
            User admin = new User()
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@stocktrack.com",
                PhotoUrl = "https://imgur.com/mOXzZLE.png",
                Verified = true,
                CreatedAt = DateTime.Now,
                VerifiedAt = DateTime.Now,
                PasswordString = string.Empty,
                PasswordHash = hash,
                PasswordSalt = salt,
            };
            modelBuilder.Entity<User>().HasData(admin);

            Institution institutionTest =
                new()
                {
                    Id = 1,
                    AccessCode = "000",
                    Name = "Manutenção",
                    Nickname = "Manutenção",
                    StreetName = "Rua Alcantara",
                    StreetNumber = "113",
                    Complement = "",
                    Neighborhood = "Vila Guilherme",
                    City = "Sao Paulo",
                    State = "SP",
                    CEP = "02110010",
                };
            Institution institutionHAS =
                new()
                {
                    Id = 2,
                    AccessCode = "064",
                    Name = "Horácio Augusto da Silveira",
                    Nickname = "ETEC Prof. Horácio",
                    StreetName = "Rua Alcantara",
                    StreetNumber = "113",
                    Complement = "",
                    Neighborhood = "Vila Guilherme",
                    City = "Sao Paulo",
                    State = "SP",
                    CEP = "02110010",
                };
            modelBuilder.Entity<Institution>().HasData(institutionTest, institutionHAS);

            modelBuilder
                .Entity<UserInstitution>()
                .HasData(
                    new UserInstitution()
                    {
                        UserId = admin.Id,
                        InstitutionId = institutionTest.Id,
                        UserRole = EUserRole.SUPPORT,
                    },
                    new UserInstitution()
                    {
                        UserId = admin.Id,
                        InstitutionId = institutionHAS.Id,
                        UserRole = EUserRole.COORDINATOR,
                    }
                );

            Area area =
                new()
                {
                    Active = true,
                    Id = 1,
                    Name = "Norte",
                    Description = "Conjunto de almoxarifados da área norte",
                    InstitutionId = institutionTest.Id,
                    CreatedBy = admin.Name,
                };
            modelBuilder.Entity<Area>().HasData(area);

            Warehouse warehouse =
                new()
                {
                    Active = true,
                    Id = 1,
                    Name = "Informática",
                    Description = "Almoxarifado de informática",
                    InstitutionId = institutionTest.Id,
                    AreaId = area.Id,
                };
            modelBuilder.Entity<Warehouse>().HasData(warehouse);

            Material notebook =
                new()
                {
                    Active = true,
                    Id = 1,
                    Name = "Notebook",
                    Description = "Notebook ThinkPad",
                    Manufacturer = "ThinkPad",
                    Measure = "UN",
                    RecordNumber = 123456,
                    InstitutionId = institutionTest.Id,
                };
            modelBuilder.Entity<Material>().HasData(notebook);

            modelBuilder
                .Entity<MaterialStatus>()
                .HasData(
                    new MaterialStatus
                    {
                        Status = EMaterialStatus.AVAILABLE,
                        MaterialId = notebook.Id,
                        Quantity = 3,
                    },
                    new MaterialStatus
                    {
                        Status = EMaterialStatus.UNABAILABLE,
                        MaterialId = notebook.Id,
                        Quantity = 1,
                    },
                    new MaterialStatus
                    {
                        Status = EMaterialStatus.MAINTENANCE,
                        MaterialId = notebook.Id,
                        Quantity = 1,
                    },
                    new MaterialStatus
                    {
                        Status = EMaterialStatus.BORROWED,
                        MaterialId = notebook.Id,
                        Quantity = 5,
                    },
                    new MaterialStatus
                    {
                        Status = EMaterialStatus.OBSOLETE,
                        MaterialId = notebook.Id,
                        Quantity = 1,
                    }
                );

            modelBuilder
                .Entity<MaterialWarehouses>()
                .HasData(
                    new MaterialWarehouses()
                    {
                        MaterialId = notebook.Id,
                        WarehouseId = warehouse.Id,
                    }
                );

            modelBuilder
                .Entity<Movimentation>()
                .HasData(
                    new Movimentation()
                    {
                        Id = 1,
                        Name = "Norte",
                        Description = "Adição de área \"Norte\"",
                        MovimentationBy = admin.Name,
                        InstitutionId = 1,
                        AreaId = 1,
                        Date = DateTime.Now,
                        Event = EMovimentationEvent.ENTRY,
                        Type = EMovimentationType.AREA,
                        Reason = EMovimentationReason.INSERTION,
                        Quantity = 1,
                    },
                    new Movimentation()
                    {
                        Id = 2,
                        Name = warehouse.Name,
                        Description = "Adição de almoxarifado \"Informática\"",
                        MovimentationBy = admin.Name,
                        InstitutionId = 1,
                        WarehouseId = 1,
                        Date = DateTime.Now,
                        Event = EMovimentationEvent.ENTRY,
                        Type = EMovimentationType.WAREHOUSE,
                        Reason = EMovimentationReason.INSERTION,
                        Quantity = 1,
                    }
                );

            ConfigureEntities(modelBuilder);
        }

        protected void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserRoleEntity>()
                .HasData(
                    new UserRoleEntity { Id = 1, Role = "USER" },
                    new UserRoleEntity { Id = 2, Role = "WAREHOUSEMAN" },
                    new UserRoleEntity { Id = 3, Role = "COORDINATOR" },
                    new UserRoleEntity { Id = 4, Role = "SUPPORT" }
                );

            modelBuilder
                .Entity<MovimentationEventEntity>()
                .HasData(
                    new MovimentationEventEntity { Id = 1, Event = "ENTRY" },
                    new MovimentationEventEntity { Id = 2, Event = "EDIT" },
                    new MovimentationEventEntity { Id = 3, Event = "EXIT" }
                );

            modelBuilder
                .Entity<MovimentationTypeEntity>()
                .HasData(
                    new MovimentationTypeEntity { Id = 1, Type = "USER" },
                    new MovimentationTypeEntity { Id = 2, Type = "AREA" },
                    new MovimentationTypeEntity { Id = 3, Type = "WAREHOUSE" },
                    new MovimentationTypeEntity { Id = 4, Type = "MATERIAL" },
                    new MovimentationTypeEntity { Id = 5, Type = "LOAN" },
                    new MovimentationTypeEntity { Id = 6, Type = "MAINTENANCE" },
                    new MovimentationTypeEntity { Id = 7, Type = "GENERAL" },
                    new MovimentationTypeEntity { Id = 8, Type = "CONSUMPTION" }
                );

            modelBuilder
                .Entity<MovimentationReasonEntity>()
                .HasData(
                    new MovimentationReasonEntity { Id = 1, Reason = "INSERTION" },
                    new MovimentationReasonEntity { Id = 2, Reason = "EDIT" },
                    new MovimentationReasonEntity { Id = 3, Reason = "RETURNFROMLOAN" },
                    new MovimentationReasonEntity { Id = 4, Reason = "RETURNFROMMAINTENANCE" },
                    new MovimentationReasonEntity { Id = 5, Reason = "DISPOSAL" },
                    new MovimentationReasonEntity { Id = 6, Reason = "LOAN" },
                    new MovimentationReasonEntity { Id = 7, Reason = "SENTTOMAINTENANCE" },
                    new MovimentationReasonEntity { Id = 8, Reason = "REMOVED" },
                    new MovimentationReasonEntity { Id = 9, Reason = "OTHER" }
                );

            modelBuilder
                .Entity<MaterialTypeEntity>()
                .HasData(
                    new MaterialTypeEntity { Id = 1, Type = "LOAN" },
                    new MaterialTypeEntity { Id = 2, Type = "CONSUMPTION" }
                );
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder
        )
        { }
    }
}
