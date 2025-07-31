using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sales.Domain.Entities;
using Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Users.Any()) return; // Nếu đã có dữ liệu thì bỏ qua

            var hasher = new PasswordHasher<User>();

            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    Role = UserRole.Admin,
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                new User
                {
                    Username = "staff",
                    Role = UserRole.Staff,
                    PasswordHash = hasher.HashPassword(null, "Staff@123")
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
