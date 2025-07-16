using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}

        // DbSet properties for your entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OrderDetail => Order
            modelBuilder.Entity<OrderDetail>()
                        .HasOne(od => od.Order)
                        .WithMany(o => o.OrderDetails)
                        .HasForeignKey(od => od.OrderId);

            // OrderDetail => Product
            modelBuilder.Entity<OrderDetail>()
                        .HasOne(od => od.Product)
                        .WithMany(p => p.OrderDetails)
                        .HasForeignKey(od => od.ProductId);

            // Order => Customer
            modelBuilder.Entity<Order>()
                        .HasOne(o => o.Customer)
                        .WithMany(c => c.Orders)
                        .HasForeignKey(o => o.CustomerId);

            // Order => User
            modelBuilder.Entity<Order>()
                        .HasOne(o => o.User)
                        .WithMany(u => u.Orders)
                        .HasForeignKey(o => o.UserId);
        }
    }
}
