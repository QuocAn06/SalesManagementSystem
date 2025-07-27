using Sales.Domain.Common;
using Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } // "Admin", "Staff", "Manager"
        public ICollection<Order> Orders { get; set; }
    }
}
