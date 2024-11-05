using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public interface IOrderService
{
    Task<ServiceResponse<Order>> GetAddressWithId(int id);
    Task<ServiceResponse<List<Order>>> GetOrders(DataSourceRequest request);
}