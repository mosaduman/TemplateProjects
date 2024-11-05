using System.Reflection;
using BasicExtensions;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;

namespace OrderManagementSystem.WebUI.Api;

public class AddressService : BaseApiService, IAddressService
{
    private readonly IConfiguration _configuration;
    private string _host;
    public AddressService(IConfiguration configuration)
    {
        _configuration = configuration;
        _host = _configuration.ReadSetting("WebApiUrl");
    }

    public async Task<ServiceResponse<Address>> GetAddressWithId(int id)
    {
        var result = await GetAsync<Address>(new ApiRequest<object>(_host, $"/Address/GetAddress/{id}"));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<Address>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse<List<Address>>> GetAddressesWithCustomerId(int customerId)
    {
        var result = await GetAsync<List<Address>>(new ApiRequest<object>(_host, $"/Address/GetAddresses/{customerId}"));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<List<Address>>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse<List<Address>>> GetAddresses(DataSourceRequest request)
    {
        var result = await PostAsync<List<Address>>(new ApiRequest<object>(_host, $"/Address/GetAddresses", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse<List<Address>>();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> InsertAddresses(List<Address> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Address/InsertAddresses", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> UpdateAddresses(List<Address> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Address/UpdateAddresses", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
    public async Task<ServiceResponse> DeleteAddresses(List<int> request)
    {
        var result = await PostAsync(new ApiRequest<object>(_host, $"/Address/DeleteAddresses", request));
        if (result.StatusCodeIsOK)
            return result.Result;
        var r = new ServiceResponse();
        r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
        if (result.Content.HasValue())
            r.ErrorMessages.Add($"Content: {result.Content}");
        return r;
    }
}