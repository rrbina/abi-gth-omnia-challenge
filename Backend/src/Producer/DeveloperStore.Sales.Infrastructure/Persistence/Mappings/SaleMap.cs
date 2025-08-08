using DeveloperStore.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("SALE");

            builder.HasKey(s => s.SaleNumber);

            builder.Property(s => s.SaleNumber)
                   .HasColumnName("ID")
                   .IsRequired();           

            builder.Property(s => s.SaleDate)
                   .HasColumnName("SALE_DATE")
                   .IsRequired();

            builder.Property(s => s.BranchName)
                   .HasColumnName("BRANCH_NAME")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasColumnName("TOTAL_AMOUNT")
                   .IsRequired();

            builder.Property(s => s.TotalDiscount)
                   .HasColumnType("decimal(18,2)")
                   .HasColumnName("TOTAL_DISCOUNT")
                   .IsRequired();

            builder.Property(s => s.CustomerId)
                   .HasColumnName("CUSTOMER_ID")
                   .IsRequired();

            builder.Property(s => s.IsCancelled)
                   .HasColumnName("IS_CANCELLED")
                   .IsRequired();

            builder.HasMany(s => s.Items)
                   .WithOne(i => i.Sale)
                   .HasForeignKey(i => i.SaleId);

            builder.HasOne(s => s.Customer)
                   .WithMany(c => c.Sales)
                   .HasForeignKey(s => s.CustomerId);
        }
    }
}
