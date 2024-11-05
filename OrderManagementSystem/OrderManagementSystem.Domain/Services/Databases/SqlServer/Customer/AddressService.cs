using Microsoft.Extensions.Configuration;
using OrderManagementSystem.Domain.Models.Databases.SqlServer;
using BasicExtensions;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer
{
    public class AddressService : BaseDataEntityService<Models.Databases.SqlServer.Address>, IAddressService
    {
        public AddressService(IConfiguration configuration) : base(configuration)
        {
        }

        public AddressService(IConfiguration configuration, string connectionString) : base(configuration, connectionString)
        {
        }

        public AddressService(IConfiguration configuration, ConnectionString connectionString) : base(configuration, connectionString)
        {
        }

       

        public async Task<ServiceResponse<List<Address>>> GetAddressesWithCustomerIdAsync(int customerId)
        {
            var sqlQuery = "select * from Address Where CustomerId=@CustomerId";
            var parameters = new { CustomerId = customerId };
            return await GetListAsync(sqlQuery, parameters);
        }

        public async Task<ServiceResponse<Models.Databases.SqlServer.Address>> GetAddressWithIdAsync(int id)
        {
            var sqlQuery = "select * from Address Where Id=@Id";
            var parameters = new { Id = id };
            return await GetAsync(sqlQuery, parameters);
        }

        public async Task<ServiceResponse> DeleteAddressesWithIdsAsync(List<int> ids)
        {
            var sqlQuery = $"delete from Address Where Id in ({ids.ToStringJoin(",")})";
            return await ExecuteAsync(sqlQuery,null);
        }
    }
}
