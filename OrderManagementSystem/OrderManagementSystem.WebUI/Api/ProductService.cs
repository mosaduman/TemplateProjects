using BasicExtensions;
using OrderManagementSystem.Core;
using OrderManagementSystem.Core.Filters;
using OrderManagementSystem.WebUI.Api.Models;
using System.Reflection;

namespace OrderManagementSystem.WebUI.Api
{
    public class ProductService : BaseApiService, IProductService
    {
        private readonly IConfiguration _configuration;
        private string _host;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _host = _configuration.ReadSetting("WebApiUrl");
        }

        public async Task<ServiceResponse<Product>> GetProductWithId(int id)
        {
            var result = await GetAsync<Product>(new ApiRequest<object>(_host, $"/Product/GetProduct/{id}"));
            if (result.StatusCodeIsOK)
                return result.Result;
            var r = new ServiceResponse<Product>();
            r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
            if (result.Content.HasValue())
                r.ErrorMessages.Add($"Content: {result.Content}");
            return r;
        }
        public async Task<ServiceResponse<List<Product>>> GetProducts(DataSourceRequest request)
        {
            var result = await PostAsync<List<Product>>(new ApiRequest<object>(_host, $"/Product/GetProducts", request));
            if (result.StatusCodeIsOK)
                return result.Result;
            var r = new ServiceResponse<List<Product>>();
            r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
            if (result.Content.HasValue())
                r.ErrorMessages.Add($"Content: {result.Content}");
            return r;
        }
        public async Task<ServiceResponse> InsertProducts(List<Product> request)
        {
            var result = await PostAsync(new ApiRequest<object>(_host, $"/Product/InsertProducts", request));
            if (result.StatusCodeIsOK)
                return result.Result;
            var r = new ServiceResponse();
            r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
            if (result.Content.HasValue())
                r.ErrorMessages.Add($"Content: {result.Content}");
            return r;
        }
        public async Task<ServiceResponse> UpdateProducts(List<Product> request)
        {
            var result = await PostAsync(new ApiRequest<object>(_host, $"/Product/UpdateProducts", request));
            if (result.StatusCodeIsOK)
                return result.Result;
            var r = new ServiceResponse();
            r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
            if (result.Content.HasValue())
                r.ErrorMessages.Add($"Content: {result.Content}");
            return r;
        }
        public async Task<ServiceResponse> DeleteProducts(List<int> request)
        {
            var result = await PostAsync(new ApiRequest<object>(_host, $"/Product/DeleteProducts", request));
            if (result.StatusCodeIsOK)
                return result.Result;
            var r = new ServiceResponse();
            r.ErrorMessages.Add($"{MethodBase.GetCurrentMethod().GetMethodFullName()} -> StatusCode : {(int)result.StatusCode}-{result.StatusCode}");
            if (result.Content.HasValue())
                r.ErrorMessages.Add($"Content: {result.Content}");
            return r;
        }
    }
}
