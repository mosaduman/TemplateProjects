namespace OrderManagementSystem.Domain.Services.Databases.SqlServer.Order
{
    public interface IOrderService : IBaseDataEntityService<Models.Databases.SqlServer.Order>
    {
        Task<ServiceResponse<Models.Databases.SqlServer.FullModels.Order>> GetFullOrderAsync(ServiceRequest<int> request);
        Task<ServiceResponse<Models.Databases.SqlServer.Order>> GetOrderWithIdAsync(int id);
    }
}
