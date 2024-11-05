using System.Net;

namespace OrderManagementSystem.WebUI.Api.Models;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public bool StatusCodeIsOK => StatusCode == HttpStatusCode.OK;
    public string Content { get; set; }
    public ServiceResponse<T> Result { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
}
public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public bool StatusCodeIsOK => StatusCode == HttpStatusCode.OK;
    public string Content { get; set; }
    public ServiceResponse Result { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
}