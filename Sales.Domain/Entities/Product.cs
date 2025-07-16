using Sales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Domain.Entities
{
    public class Product: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public int StockQuantity { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
