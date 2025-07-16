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
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Configure the Customer entity
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(200);
            
            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(c => c.Address)
                   .HasMaxLength(250);
            
            builder.Property(c => c.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");
          
        }
    }
}
