using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockTrack_API.Models;
using StockTrack_API.Models.Entities;
using StockTrack_API.Models.Enums;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Utils;

namespace StockTrack_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Area> ST_AREAS { get; set; }
        public DbSet<Institution> ST_INSTITUTIONS { get; set; }
        public DbSet<Material> ST_MATERIALS { get; set; }
        public DbSet<Movimentation> ST_MOVIMENTATIONS { get; set; }
        public DbSet<User> ST_USERS { get; set; }
        public DbSet<Warehouse> ST_WAREHOUSES { get; set; }
        public DbSet<UserInstitution> ST_USER_INSTITUTIONS { get; set; }
        public DbSet<MaterialWarehouses> ST_MATERIAL_WAREHOUSES { get; set; }

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

            modelBuilder
                .Entity<UserInstitution>()
                .HasKey(ui => new { ui.UserId, ui.InstitutionId });

            modelBuilder
                .Entity<UserInstitution>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInstitutions)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder
                .Entity<UserInstitution>()
                .HasOne(ui => ui.Institution)
                .WithMany(u => u.Users)
                .HasForeignKey(ui => ui.InstitutionId);

            modelBuilder
                .Entity<Area>()
                .HasOne(a => a.Institution)
                .WithMany(i => i.Areas)
                .HasForeignKey(a => a.InstitutionId);

            modelBuilder
                .Entity<Warehouse>()
                .HasOne(a => a.Area)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Movimentations
            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Area)
                .WithMany()
                .HasForeignKey(m => m.AreaId);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Warehouse)
                .WithMany()
                .HasForeignKey(m => m.WarehouseId);

            modelBuilder
                .Entity<Movimentation>()
                .HasOne(m => m.Material)
                .WithMany()
                .HasForeignKey(m => m.MaterialId);

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

            Cryptography.CreatePasswordHash("admin12345", out byte[] hash, out byte[] salt);
            User admin = new User()
            {
                Active = true,
                Id = 1,
                Name = "Admin",
                Email = "admin@stocktrack.com",
                PhotoUrl = "https://imgur.com/mOXzZLE.png",
                CreatedAt = DateTime.Now,
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
                    Name = "Servidor de testes",
                    Nickname = "Testes",
                    StreetName = "Rua Alcantara",
                    StreetNumber = "113",
                    Complement = "",
                    Neightboor = "Vila Guilherme",
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
                    Neightboor = "Vila Guilherme",
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
                        UserRole = UserRole.SUPPORT,
                    },
                    new UserInstitution()
                    {
                        UserId = admin.Id,
                        InstitutionId = institutionHAS.Id,
                        UserRole = UserRole.COORDINATOR,
                    }
                );

            Area area =
                new()
                {
                    Active = true,
                    Id = 1,
                    Name = "Teste",
                    Description = "Área de Testes",
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

            modelBuilder
                .Entity<Material>()
                .HasData(
                    new Material()
                    {
                        Active = true,
                        Id = 1,
                        Name = "Notebook",
                        Description = "Notebook ThinkPad",
                        Manufacturer = "ThinkPad",
                        Measure = "UN",
                        Quantity = 3,
                        RecordNumber = 123456,
                        InstitutionId = institutionTest.Id,
                    }
                );

            modelBuilder
                .Entity<Movimentation>()
                .HasData(
                    new Movimentation()
                    {
                        Id = 1,
                        Name = "Área Teste",
                        Description = "Adição de área \"Teste\"",
                        MovimentationBy = admin.Name,
                        InstitutionId = 1,
                        AreaId = 1,
                        Date = DateTime.Now,
                        Event = MovimentationEvent.ENTRY,
                        Type = MovimentationType.AREA,
                        Reason = MovimentationReason.INSERTION,
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
                    new MovimentationTypeEntity { Id = 7, Type = "GENERAL" }
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
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder
        ) { }
    }
}
