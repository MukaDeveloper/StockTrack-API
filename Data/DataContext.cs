using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().ToTable("ST_AREAS");
            modelBuilder.Entity<Institution>().ToTable("ST_INSTITUTIONS");
            modelBuilder.Entity<Material>().ToTable("ST_MATERIALS");
            modelBuilder.Entity<Movimentation>().ToTable("ST_MOVIMENTATIONS");
            modelBuilder.Entity<User>().ToTable("ST_USERS");
            modelBuilder.Entity<Warehouse>().ToTable("ST_WAREHOUSES");

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
                .WithMany(u => u.UserInstitutions)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder
                .Entity<Area>()
                .HasOne(a => a.Institution)
                .WithMany(i => i.Areas)
                .HasForeignKey(a => a.InstitutionId);

            modelBuilder
                .Entity<Warehouse>()
                .HasOne(a => a.Area)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.AreaId);

            modelBuilder
                .Entity<Material>()
                .HasOne(a => a.Warehouse)
                .WithMany(m => m.Materials)
                .HasForeignKey(w => w.WarehouseId);

            Cryptography.CreatePasswordHash("admin12345", out byte[] hash, out byte[] salt);
            User admin = new User()
            {
                Active = true,
                Id = 1,
                Name = "Admin",
                Email = "admin@stocktrack.com",
                PhotoUrl = "https://imgur.com/mOXzZLE.png",
                PasswordString = string.Empty,
                PasswordHash = hash,
                PasswordSalt = salt,
            };
            modelBuilder.Entity<User>().HasData(admin);

            Institution institutionTest =
                new()
                {
                    Id = 001,
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
            modelBuilder
                .Entity<Institution>()
                .HasData(
                    institutionTest,
                    new Institution()
                    {
                        Id = 064,
                        Name = "Horácio Augusto da Silveira",
                        Nickname = "ETEC Prof. Horácio",
                        StreetName = "Rua Alcantara",
                        StreetNumber = "113",
                        Complement = "",
                        Neightboor = "Vila Guilherme",
                        City = "Sao Paulo",
                        State = "SP",
                        CEP = "02110010",
                    }
                );

            modelBuilder
                .Entity<UserInstitution>()
                .HasData(
                    new UserInstitution()
                    {
                        UserId = 1,
                        UserName = admin.Name,
                        InstitutionId = institutionTest.Id,
                        InstitutionName = institutionTest.Name,
                        UserType = UserType.SUPPORT,
                    },
                    new UserInstitution()
                    {
                        UserId = 1,
                        UserName = admin.Name,
                        InstitutionId = 064,
                        InstitutionName = "Horácio Augusto da Silveira",
                        UserType = UserType.COORDINATOR,
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
                    InstitutionName = institutionTest.Name,
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
                    InstitutionName = institutionTest.Name,
                    AreaId = area.Id,
                    AreaName = area.Name,
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
                        RecordNumber = 123456,
                        InstitutionId = institutionTest.Id,
                        InstitutionName = institutionTest.Name,
                        AreaId = area.Id,
                        AreaName = area.Name,
                        WarehouseId = warehouse.Id,
                        WarehouseName = warehouse.Name,
                    }
                );

            modelBuilder
                .Entity<Movimentation>()
                .HasData(
                    new Movimentation()
                    {
                        Id = 1,
                        Name = "Área Teste",
                        InstitutionId = 1,
                        AreaId = 1,
                        UserId = admin.Id,
                        Description = "Adição de área \"Teste\"",
                        Date = DateTime.Now,
                        Type = MovimentationType.Entry,
                        Reason = MovimentationReason.Insertion,
                        Quantity = 1,
                    }
                );

            /*
            * Definindo tabelas de referência
            */
            modelBuilder
                .Entity<UserTypeEntity>()
                .HasData(
                    new UserTypeEntity { Id = 1, Name = "USER" },
                    new UserTypeEntity { Id = 2, Name = "WAREHOUSEMAN" },
                    new UserTypeEntity { Id = 3, Name = "COORDINATOR" },
                    new UserTypeEntity { Id = 4, Name = "SUPPORT" }
                );

            modelBuilder
                .Entity<MovimentationTypeEntity>()
                .HasData(
                    new MovimentationTypeEntity { Id = 1, Type = "ENTRY" },
                    new MovimentationTypeEntity { Id = 2, Type = "EXIT" }
                );

            modelBuilder
                .Entity<MovimentationReasonEntity>()
                .HasData(
                    new MovimentationReasonEntity { Id = 1, Reason = "Insertion" },
                    new MovimentationReasonEntity { Id = 2, Reason = "Edit" },
                    new MovimentationReasonEntity { Id = 3, Reason = "ReturnFromLoan" },
                    new MovimentationReasonEntity { Id = 4, Reason = "ReturnFromMaintenance" },
                    new MovimentationReasonEntity { Id = 5, Reason = "Disposal" },
                    new MovimentationReasonEntity { Id = 6, Reason = "Loan" },
                    new MovimentationReasonEntity { Id = 7, Reason = "SentToMaintenance" },
                    new MovimentationReasonEntity { Id = 8, Reason = "Removed" },
                    new MovimentationReasonEntity { Id = 9, Reason = "Other" }
                );
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder
        ) { }
    }
}
