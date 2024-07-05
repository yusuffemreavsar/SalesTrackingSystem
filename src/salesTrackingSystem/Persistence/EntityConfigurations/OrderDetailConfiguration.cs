using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails").HasKey(od => od.Id);

        builder.Property(od => od.Id).HasColumnName("Id").IsRequired();
        builder.Property(od => od.OrderId).HasColumnName("OrderId");
        builder.Property(od => od.Order).HasColumnName("Order");
        builder.Property(od => od.ProductName).HasColumnName("ProductName");
        builder.Property(od => od.Quantity).HasColumnName("Quantity");
        builder.Property(od => od.UnitPrice).HasColumnName("UnitPrice");
        builder.Property(od => od.TotalPrice).HasColumnName("TotalPrice");
        builder.Property(od => od.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(od => od.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(od => od.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(od => !od.DeletedDate.HasValue);
    }
}