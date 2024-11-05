using OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Customer;
using OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Order;
using OrderManagementSystem.Domain.Configurations.Mappers.Dappers.Product;

namespace OrderManagementSystem.Domain.Configurations.Mappers.Dappers
{
    public static class DapperPlusMapRoles
    {
        private static bool IsRun { get; set; }

        public static void Run()
        {
            if (!IsRun)
            {
                IsRun = true;
                OrderMapper.Run();
                AddressMapper.Run();
                CustomerMapper.Run();
                OrderItemMapper.Run();
                ProductMapper.Run();
            }
        }
    }
}
