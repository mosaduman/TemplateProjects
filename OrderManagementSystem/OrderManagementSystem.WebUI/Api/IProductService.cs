using BasicExtensions;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;
using System.Reflection;

namespace OrderManagementSystem.WebUI.Api
{
    public interface IProductService
    {
        Task<ServiceResponse<Product>> GetProductWithId(int id);
        Task<ServiceResponse<List<Product>>> GetProducts(DataSourceRequest request);
        Task<ServiceResponse> InsertProducts(List<Product> request);
        Task<ServiceResponse> UpdateProducts(List<Product> request);
        Task<ServiceResponse> DeleteProducts(List<int> request);
    }
}
