using Microsoft.EntityFrameworkCore;
using StockTrack_API.Models.Interfaces;
using StockTrack_API.Models.Enums;
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

            modelBuilder.Entity<User>().Property(u => u.UserType).HasDefaultValue(UserType.USER);

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

            modelBuilder.Entity<Area>()
                .HasOne(a => a.Institution)
                .WithMany(i => i.Areas)
                .HasForeignKey(a => a.InstitutionId);

            modelBuilder.Entity<Warehouse>()
                .HasOne(a => a.Area)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.AreaId);

            modelBuilder.Entity<Material>()
                .HasOne(a => a.Warehouse)
                .WithMany(m => m.Materials)
                .HasForeignKey(w => w.WarehouseId);


            Cryptography.CreatePasswordHash("admin12345", out byte[] hash, out byte[] salt);
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Name = "Admin",
                        Email = "admin@stocktrack.com",
                        PhotoUrl = "https://imgur.com/mOXzZLE.png",
                        UserType = UserType.SUPPORT,
                        PasswordString = string.Empty,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                    }
                );

            modelBuilder
                .Entity<Institution>()
                .HasData(
                    new Institution()
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
                    },
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

            modelBuilder.Entity<UserInstitution>().HasData(
                new UserInstitution()
                {
                    UserId = 1,
                    InstitutionId = 001
                },
                new UserInstitution()
                {
                    UserId = 1,
                    InstitutionId = 064
                }
            );

            modelBuilder.Entity<Area>().HasData(
                new Area()
                {
                    Id = 1,
                    Name = "Teste",
                    Description = "Área de Testes",
                    InstitutionId = 001
                }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse()
                {
                    Id = 1,
                    Name = "Informática",
                    Description = "Almoxarifado de informática",
                    AreaId = 1,
                }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material() 
                {
                    Id = 1,
                    Name = "Notebook",
                    Description = "Notebook ThinkPad",
                    Manufacturer = "ThinkPad",
                    RecordNumber = 123456,
                    WarehouseId = 1,
                }
            );
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder
        )
        { }
    }
}
