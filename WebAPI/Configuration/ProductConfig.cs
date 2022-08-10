using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(10);
            builder.Property(p => p.Price).HasDefaultValue(5);
            builder.Property(p => p.isActive);
        }
    }
}
