using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Data.Configurations
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Configure the Order entity
            builder.Property(o => o.OrderDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.TotalAmount)
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId);
        }
    }
}
