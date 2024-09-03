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
            modelBuilder.Entity<UserInstitution>().HasData(
                new UserInstitution() {
                    UserId = 1,
                    InstitutionId = 001
                },
                new UserInstitution() {
                    UserId = 1,
                    InstitutionId = 064
                }
            );

            modelBuilder.Entity<User>().Property(u => u.UserType).HasDefaultValue(UserType.USER);

            modelBuilder.Entity<Area>().HasKey(a => a.Id);
            modelBuilder.Entity<Institution>().HasKey(i => i.Id);
            modelBuilder.Entity<Material>().HasKey(m => m.Id);
            modelBuilder.Entity<Movimentation>().HasKey(m => m.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Warehouse>().HasKey(w => w.Id);
            modelBuilder
                .Entity<UserInstitution>()
                .HasKey(ui => new { ui.UserId, ui.InstitutionId });

            modelBuilder
                .Entity<Institution>()
                .HasMany(a => a.Areas)
                .WithOne(a => a.Institution)
                .HasForeignKey(a => a.InstitutionId);

            modelBuilder
                .Entity<Area>()
                .HasMany(w => w.Warehouses)
                .WithOne(w => w.Area)
                .HasForeignKey(w => w.AreaId);

            modelBuilder
                .Entity<Warehouse>()
                .HasMany(w => w.Materials)
                .WithOne(w => w.Warehouse)
                .HasForeignKey(w => w.WarehouseId);

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
        }

        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder
        ) { }
    }
}
