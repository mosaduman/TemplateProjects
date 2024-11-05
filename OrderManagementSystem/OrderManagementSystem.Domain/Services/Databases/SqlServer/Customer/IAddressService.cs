using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer
{
    public interface IAddressService : IBaseDataEntityService<Address>
    {
        Task<ServiceResponse<Models.Databases.SqlServer.Address>> GetAddressWithIdAsync(int id);
        Task<ServiceResponse<List<Models.Databases.SqlServer.Address>>> GetAddressesWithCustomerIdAsync(int id);
        Task<ServiceResponse> DeleteAddressesWithIdsAsync(List<int> ids);
    }
}
