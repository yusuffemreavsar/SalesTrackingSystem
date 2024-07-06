using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SalesDetailConfiguration : IEntityTypeConfiguration<SalesDetail>
{
    public void Configure(EntityTypeBuilder<SalesDetail> builder)
    {
        builder.ToTable("SalesDetails").HasKey(sd => sd.Id);

        builder.Property(sd => sd.Id).HasColumnName("Id").IsRequired();
        builder.Property(sd => sd.SaleId).HasColumnName("SaleId");
        builder.Property(sd => sd.Sale).HasColumnName("Sale");
        builder.Property(sd => sd.Product).HasColumnName("Product");
        builder.Property(sd => sd.Quantity).HasColumnName("Quantity");
        builder.Property(sd => sd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sd => sd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sd => sd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(sd => !sd.DeletedDate.HasValue);

        builder.HasKey(sd => new { sd.ProductId, sd.SaleId });
        builder.HasOne(sd => sd.Product).WithMany(p => p.SalesDetails).HasForeignKey(sd=>sd.ProductId);
        builder.HasOne(sd => sd.Sale).WithMany(p => p.SalesDetails).HasForeignKey(sd => sd.SaleId);

    }
}