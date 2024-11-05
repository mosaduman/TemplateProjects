using BasicExtensions;
using Microsoft.Extensions.Configuration;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Customer
{
    public class CustomerService : BaseDataEntityService<Models.Databases.SqlServer.Customer>, ICustomerService
    {
        public CustomerService(IConfiguration configuration) : base(configuration)
        {
        }

        public CustomerService(IConfiguration configuration, string connectionString) : base(configuration, connectionString)
        {
        }

        public CustomerService(IConfiguration configuration, ConnectionString connectionString) : base(configuration, connectionString)
        {
        }
        public async Task<ServiceResponse<Models.Databases.SqlServer.Customer>> GetCustomerWithIdAsync(int id)
        {
            var sqlQuery = "select * from [Customer] Where Id=@Id";
            var parameters = new { Id = id };
            return await GetAsync(sqlQuery,parameters);
        }

        public async Task<ServiceResponse> DeleteCustomersWithIdsAsync(List<int> ids)
        {
            var sqlQuery = $"delete from [Customer] Where Id in ({ids.ToStringJoin(",")})";
            return await ExecuteAsync(sqlQuery, null);
        }

    }
}
