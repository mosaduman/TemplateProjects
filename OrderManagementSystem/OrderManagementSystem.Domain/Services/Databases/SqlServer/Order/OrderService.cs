using Dapper;
using Microsoft.Extensions.Configuration;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using System;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Order
{
    public class OrderService : BaseDataEntityService<Models.Databases.SqlServer.Order>, IOrderService
    {
        public OrderService(IConfiguration configuration) : base(configuration)
        {
        }

        public OrderService(IConfiguration configuration, string connectionString) : base(configuration, connectionString)
        {
        }

        public OrderService(IConfiguration configuration, ConnectionString connectionString) : base(configuration, connectionString)
        {
        }
        public async Task<ServiceResponse<Models.Databases.SqlServer.Order>> GetOrderWithIdAsync(int id)
        {
            var sqlQuery = "select * from [Order] Where Id=@Id";
            var parameters = new { Id = id };
            return await GetAsync(sqlQuery, parameters);
        }

        public async Task<ServiceResponse<Models.Databases.SqlServer.FullModels.Order>> GetFullOrderAsync(ServiceRequest<int> request)
        {
            await using (var connection = Connection.GetConnection(ConnectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT o.*, \n"
                           + "       a.*, \n"
                           + "       c.*, \n"
                           + "       oi.*, \n"
                           + "       p.*\n"
                           + "FROM [Order] o\n"
                           + "     INNER JOIN Address a ON a.Id IN(o.InvoiceAddressId, o.ShipmentAddressId)   \n"
                           + "     INNER JOIN Customer c ON c.Id = o.CustomerId\n"
                           + "     INNER JOIN [OrderItem] oi ON oi.OrderId = o.Id\n"
                           + "     INNER JOIN Product p ON p.Id = oi.ProductId\n"
                           + $" WHERE o.Id={request.Result}";

                var lookup = new Dictionary<int, Models.Databases.SqlServer.FullModels.Order>();
                var fullOrder = (await connection.QueryAsync<
                       Models.Databases.SqlServer.FullModels.Order,
                        Address,
                        Models.Databases.SqlServer.Customer,
                       OrderItem,
                       Models.Databases.SqlServer.Product,
                       Models.Databases.SqlServer.FullModels.Order>
                       (sql, (order,address, customer, orderitems,products) =>
                       {
                           Models.Databases.SqlServer.FullModels.Order o;
                           if (!lookup.TryGetValue(order.Id, out o))
                           {
                               lookup.Add(order.Id, o = order);
                           }
                           if (customer != null)
                           {
                               if (o.Customer == null || o.Customer.Id != customer.Id)
                                   o.Customer = customer;
                           }
                           if (address != null)
                           {
                               if (o.Addresses == null)
                                   o.Addresses = new List<Address>();
                               if (o.Addresses.All(s => s.Id != address.Id))
                                   o.Addresses.Add(address);
                           }
                           if (orderitems != null)
                           {
                               if (o.OrderItems == null)
                                   o.OrderItems = new List<OrderItem>();
                               if (o.OrderItems.All(s => s.Id != orderitems.Id))
                                   o.OrderItems.Add(orderitems);
                           }
                           if (products != null)
                           {
                               if (o.Products == null)
                                   o.Products = new List<Models.Databases.SqlServer.Product>();
                               if (o.Products.All(s => s.Id != products.Id))
                                   o.Products.Add(products);
                           }

                           return o;
                       })).ToList();
                return new ServiceResponse<Models.Databases.SqlServer.FullModels.Order>()
                {
                    Result = lookup.Values.FirstOrDefault()
                };
            }
        }



    }
}
