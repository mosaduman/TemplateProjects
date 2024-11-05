using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Order
{
    public static class OrderItemMapper
    {
        public static void Run()
        {
            DapperPlusManager.Entity<Models.Databases.SqlServer.OrderItem>().Table("OrderItem")
                .Key(s => s.Id)
                .Identity(s => s.Id);
        }
    }
}
