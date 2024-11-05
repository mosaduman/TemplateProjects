using Z.Dapper.Plus;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Customer
{
    public static class AddressMapper
    {
        public static void Run()
        {
            DapperPlusManager.Entity<Models.Databases.SqlServer.Address>().Table("Address")
                .Key(s => s.Id)
                .Identity(s => s.Id);
        }
    }
}
