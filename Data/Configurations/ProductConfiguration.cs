using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RazorPagesUI.Models;

namespace RazorPagesUI.Data.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) 
        {
            builder.Property(p => p.ImageName).HasColumnName("ImageFileName");
        }
    }
}
