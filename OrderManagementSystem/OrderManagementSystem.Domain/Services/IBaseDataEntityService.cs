using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.Domain.Models.Databases;

namespace OrderManagementSystem.Domain.Services
{
    public interface IBaseDataEntityService<T> where T : class, IBaseEntity, new()
    {
        Task<ServiceResponse> BulkInsertAsync(ServiceRequest<List<T>> entities);
        Task<ServiceResponse> BulkUpdateAsync(ServiceRequest<List<T>> entities);
        Task<ServiceResponse> BulkDeleteAsync(ServiceRequest<List<T>> entities);
        Task<ServiceResponse> BulkSaveAsync(ServiceRequest<List<T>> entities);
        Task<ServiceResponse<List<T>>> GetFilteredDataAsync(ServiceRequest<DataSourceRequest> request);
    }
}
