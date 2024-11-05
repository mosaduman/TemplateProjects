using System.Reflection;
using BasicExtensions;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public class OrderService : BaseApiService, IOrderService
{
    private readonly IConfiguration _configuration;
    private string _host;
    public OrderService(IConfiguration configuration)
    {
        _configuration = configuration;
        _host = _configuration.ReadSetting("WebApiUrl");
    }

    public async Task<ServiceResponse<Order>> GetAddressWithId(int id)
    {
        var result = await GetAsync<Order>(new ApiRequest<object>(_host, $"/Order/GetOrder/{id}"));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<Order>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse<List<Order>>> GetOrders(DataSourceRequest request)
    {
        var result = await PostAsync<List<Order>>(new ApiRequest<object>(_host, $"/Order/GetOrders", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<List<Order>>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
}