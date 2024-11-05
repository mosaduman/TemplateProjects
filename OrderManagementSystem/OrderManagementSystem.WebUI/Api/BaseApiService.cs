using BasicExtensions;
using OrderManagementSystem.WebUI.Api.Models;
using RestSharp;

namespace OrderManagementSystem.WebUI.Api
{
    public abstract class BaseApiService
    {
        private async Task<ApiResponse<T>> ExecuteAsync<T>(ApiRequest<object> r, Method method)
        {
            var result = new ApiResponse<T>();
            try
            {
                var options = new RestClientOptions(r.Host) { MaxTimeout = -1, };
                var client = new RestClient(options);
                var request = new RestRequest(r.Url, method);
                request.AddHeader("Content-Type", "application/json");
                if (r.Token.HasValue())
                    request.AddHeader("Authorization", $"Bearer {r.Token}");
                if (r.Data != null)
                    request.AddStringBody(r.Data.ToJson(), DataFormat.Json);
                RestResponse response = await client.ExecuteAsync(request);

                result.StatusCode = response.StatusCode;
                result.Content = response.Content;

                if (result.StatusCodeIsOK)
                    result.Result = response.Content.ToModelFromJson<ServiceResponse<T>>();
                else
                    result.ErrorMessages.Add($"StatusCode: {(int)result.StatusCode} - {result.StatusCode}");
            }
            catch (Exception e)
            {
                result.ErrorMessages.Add(e.Message);
            }
            return result;
        }
        private async Task<ApiResponse> ExecuteAsync(ApiRequest<object> r, Method method)
        {
            var result = new ApiResponse();
            try
            {
                var options = new RestClientOptions(r.Host) { MaxTimeout = -1, };
                var client = new RestClient(options);
                var request = new RestRequest(r.Url, method);
                request.AddHeader("Content-Type", "application/json");
                if (r.Token.HasValue())
                    request.AddHeader("Authorization", $"Bearer {r.Token}");
                if (r.Data != null)
                    request.AddStringBody(r.Data.ToJson(), DataFormat.Json);
                RestResponse response = await client.ExecuteAsync(request);

                result.StatusCode = response.StatusCode;
                result.Content = response.Content;

                if (result.StatusCodeIsOK)
                    result.Result = response.Content.ToModelFromJson<ServiceResponse>();
                else
                    result.ErrorMessages.Add($"StatusCode: {(int)result.StatusCode} - {result.StatusCode}");
            }
            catch (Exception e)
            {
                result.ErrorMessages.Add(e.Message);
            }
            return result;
        }
        protected Task<ApiResponse<T>> GetAsync<T>(ApiRequest<object> request) => ExecuteAsync<T>(request, Method.Get);
        protected Task<ApiResponse> GetAsync(ApiRequest<object> request) => ExecuteAsync(request, Method.Get);
        protected Task<ApiResponse<T>> PostAsync<T>(ApiRequest<object> request) => ExecuteAsync<T>(request, Method.Post);
        protected Task<ApiResponse> PostAsync(ApiRequest<object> request) => ExecuteAsync(request, Method.Post);
        protected Task<ApiResponse<T>> PutAsync<T>(ApiRequest<object> request) => ExecuteAsync<T>(request, Method.Put);
        protected Task<ApiResponse> PutAsync(ApiRequest<object> request) => ExecuteAsync(request, Method.Put);
        protected Task<ApiResponse<T>> DeleteAsync<T>(ApiRequest<object> request) => ExecuteAsync<T>(request, Method.Delete);
        protected Task<ApiResponse> DeleteAsync(ApiRequest<object> request) => ExecuteAsync(request, Method.Delete);
    }
}
