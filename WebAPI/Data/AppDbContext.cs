using Microsoft.EntityFrameworkCore;
using WebAPI.Configuration;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
        }

    }
}
