using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SALE_ITEM");

            builder.HasKey(i => i.Id);

            builder.Property(si => si.Id)
                   .HasColumnName("ID")
                   .IsRequired();

            builder.Ignore(s => s.TotalAmount);

            builder.Property(si => si.Quantity)
                   .HasColumnName("QUANTITY")
                   .IsRequired();

            builder.Property(i => i.Discount)
                   .HasColumnName("DISCOUNT")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.IsCancelled)
            .HasColumnName("IS_CANCELLED")
            .IsRequired();

            builder.Property(si => si.SaleId)
                   .HasColumnName("SALE_ID")
                   .IsRequired();

            builder.HasOne(i => i.Sale)
                   .WithMany(s => s.Items)
                   .HasForeignKey(i => i.SaleId);

            builder.Property(si => si.ProductId)
                   .HasColumnName("PRODUCT_ID")
                   .IsRequired();

            builder.HasOne(si => si.Product)
                   .WithMany()
                   .HasForeignKey(si => si.ProductId);

        }

    }
}