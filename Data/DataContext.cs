using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StockTrack_API.Models;

namespace StockTrack_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Area> ST_AREAS { get; set; }
        public DbSet<Warehouse> ST_WAREHOUSES { get; set; }
        public DbSet<Material> ST_MATERIALS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Material>().ToTable("ST_MATERIALS");
            modelBuilder.Entity<Material>().HasData(new Material() { Id = 1, Name = "Test" });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
            
        }
    }
}