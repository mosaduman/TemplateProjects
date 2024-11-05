using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer.FullModels
{
    public class Order:Models.Databases.SqlServer.Order
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<Product> Products { get; set; }
        public List<Address> Addresses { get; set; }
        public Customer Customer { get; set; }
    }
}
