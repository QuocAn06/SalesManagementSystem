using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Data.Configurations
{
    public class OrderDetailConfiguration: IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderDetail> builder)
        {

            builder.Property(od => od.Quantity)
                .IsRequired();
            
            builder.Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)");
            
            builder.Property(od => od.TotalPrice)
                .HasColumnType("decimal(18,2)");
            
            builder.Property(od => od.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");
            
            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);
            
            builder.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }
    }
}
