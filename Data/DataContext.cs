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

        public DbSet<Item> ST_ITENS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Item>().ToTable("ST_ITENS");
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 1, Name = "Test" });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
            
        }
    }
}