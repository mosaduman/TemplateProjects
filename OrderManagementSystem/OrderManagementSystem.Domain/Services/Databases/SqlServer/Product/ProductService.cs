using BasicExtensions;
using Microsoft.Extensions.Configuration;

namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Product
{
    public class ProductService : BaseDataEntityService<Models.Databases.SqlServer.Product>, IProductService
    {
        public ProductService(IConfiguration configuration) : base(configuration)
        {
        }

        public ProductService(IConfiguration configuration, string connectionString) : base(configuration, connectionString)
        {
        }

        public ProductService(IConfiguration configuration, ConnectionString connectionString) : base(configuration, connectionString)
        {
        }

        public async Task<ServiceResponse<Models.Databases.SqlServer.Product>> GetProductWithIdAsync(int id)
        {
            var sqlQuery = "select * from [Product] Where Id=@Id";
            var parameters = new { Id = id };
            return await GetAsync(sqlQuery, parameters);
        }

        public async Task<ServiceResponse> DeleteProductsWithIdsAsync(List<int> ids)
        {
            var sqlQuery = $"delete from [Product] Where Id in ({ids.ToStringJoin(",")})";
            return await ExecuteAsync(sqlQuery);
        }
    }
}
