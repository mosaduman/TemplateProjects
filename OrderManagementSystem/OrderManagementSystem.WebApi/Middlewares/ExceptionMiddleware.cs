using BasicExtensions;
using OrderManagementSystem.WebApi.Configurations;
using System.Net;

namespace OrderManagementSystem.WebApi.Middlewares
{
 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessages = new List<string>();
            errorMessages.Add(exception.Message);
            if (exception?.StackTrace != null)
                errorMessages.Add(exception.StackTrace);
            if (exception?.InnerException != null)
                errorMessages.Add(exception.InnerException.Message);

            errorMessages.ToJson().ErrorLog();
            return httpContext.Response.WriteAsync(new ServiceResponse()
            {
                ErrorMessages = errorMessages,
                Messages = new List<string>(),

            }.ToJson());
        }
    }
}
