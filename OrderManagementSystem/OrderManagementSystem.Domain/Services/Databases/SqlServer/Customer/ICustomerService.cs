using OrderManagementSystem.Core.Filters;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer
{
    public interface ICustomerService : IBaseDataEntityService<Models.Databases.SqlServer.Customer>
    {
        Task<ServiceResponse<Models.Databases.SqlServer.Customer>> GetCustomerWithIdAsync(int id);
        Task<ServiceResponse> DeleteCustomersWithIdsAsync(List<int> ids);
    }
}
