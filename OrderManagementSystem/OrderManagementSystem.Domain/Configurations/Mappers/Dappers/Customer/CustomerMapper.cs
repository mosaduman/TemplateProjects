using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Customer
{
    public static class CustomerMapper
    {
        public static void Run()
        {
            DapperPlusManager.Entity<Models.Databases.SqlServer.Customer>().Table("Customer")
                .Key(s => s.Id)
                .Identity(s => s.Id);
        }
    }
}
