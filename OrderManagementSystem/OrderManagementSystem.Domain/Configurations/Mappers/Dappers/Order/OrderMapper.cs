using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Order
{
    public static class OrderMapper
    {
        public static void Run()
        {
            DapperPlusManager.Entity<Models.Databases.SqlServer.Order>().Table("Order")
                .Key(s => s.Id)
                .Identity(s => s.Id);
        }
    }
}
