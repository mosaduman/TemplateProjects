using System.Reflection;
using BasicExtensions;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public class CustomerService : BaseApiService, ICustomerService
{
    private readonly IConfiguration _configuration;
    private string _host;
    public CustomerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _host = _configuration.ReadSetting("WebApiUrl");
    }

    public async Task<ServiceResponse<Customer>> GetCustomerWithId(int id)
    {
        var result = await GetAsync<Customer>(new ApiRequest<object>(_host, $"/Customer/GetCustomer/{id}"));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<Customer>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse<List<Customer>>> GetCustomers(DataSourceRequest request)
    {
        var result = await PostAsync<List<Customer>>(new ApiRequest<object>(_host, $"/Customer/GetCustomers", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<List<Customer>>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> InsertCustomers(List<Customer> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Customer/InsertCustomers", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> UpdateCustomers(List<Customer> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Customer/UpdateCustomers", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> DeleteCustomers(List<int> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Customer/DeleteCustomers", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
}