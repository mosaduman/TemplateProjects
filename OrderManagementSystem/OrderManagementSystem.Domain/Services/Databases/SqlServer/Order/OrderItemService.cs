using Microsoft.Extensions.Configuration;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Order
{
    public class OrderItemService : BaseDataEntityService<Models.Databases.SqlServer.OrderItem>, IOrderItemService
    {
        public OrderItemService(IConfiguration configuration) : base(configuration)
        {
        }

        public OrderItemService(IConfiguration configuration, string connectionString) : base(configuration, connectionString)
        {
        }

        public OrderItemService(IConfiguration configuration, ConnectionString connectionString) : base(configuration, connectionString)
        {
        }
    }
}
