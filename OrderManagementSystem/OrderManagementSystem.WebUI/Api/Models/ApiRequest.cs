using System.Net;

namespace OrderManagementSystem.WebUI.Api.Models
{
    public class ApiRequest<T>
    {
        public T Data { get; set; }
        public string Host { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public ApiRequest() { }
        public ApiRequest(T data) { Data = data; }
        public ApiRequest(string host, string url) { Host = host; Url = url; }
        public ApiRequest(string host, string url, string token) { Host = host; Url = url; Token = token; }
        public ApiRequest(string host, string url, T data) { Host = host; Url = url; Data = data; }
        public ApiRequest(string host, string url, string token, T data) { Host = host; Url = url; Token = token; Data = data; }
    }
}
