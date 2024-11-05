namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Product
{
    public interface IProductService : IBaseDataEntityService<Models.Databases.SqlServer.Product>
    {
        Task<ServiceResponse<Models.Databases.SqlServer.Product>> GetProductWithIdAsync(int id);
        Task<ServiceResponse> DeleteProductsWithIdsAsync(List<int> ids);
    }
}
