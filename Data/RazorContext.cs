using Microsoft.EntityFrameworkCore;
using RazorPagesUI.Data.Configurations;
using RazorPagesUI.Models;
namespace RazorPagesUI.Data
{
    public class RazorContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=./Test1.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()).Seed();
        }
    }
}
