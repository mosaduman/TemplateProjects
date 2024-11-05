using BasicExtensions;
using OrderManagementSystem.WebApi.Middlewares;
using System.Net;

namespace OrderManagementSystem.WebApi.Configurations
{

    public static class ExceptionMiddlewareConfigure
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
