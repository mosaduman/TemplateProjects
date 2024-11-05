using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Product
{
    public static class ProductMapper
    {
        public static void Run()
        {
            DapperPlusManager.Entity<Models.Databases.SqlServer.Product>().Table("Product")
                .Key(s => s.Id)
                .Identity(s => s.Id);
        }
    }
}
