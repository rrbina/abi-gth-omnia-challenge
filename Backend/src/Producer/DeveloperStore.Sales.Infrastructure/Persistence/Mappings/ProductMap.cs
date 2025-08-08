using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT");

            builder.HasKey(i => i.Id);           

            builder.Property(i => i.ProductName)
                   .HasColumnName("PRODUCT_NAME")
                   .IsRequired();           

            builder.Property(i => i.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
           
        }
    }
}
