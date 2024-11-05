using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace OrderManagementSystem.WebUI.Configurations.Containers
{
    public static class AutofacConfigure
    {
        public static IHostBuilder AddAutofac(this IHostBuilder builder)
        {
            return builder.ConfigureServices(services => services.AddAutofac())
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
        public static void AutofacConfigureBuilder(this ContainerBuilder builder)
        {

            builder.RegisterType<Api.OrderService>().As<Api.IOrderService>();
            builder.RegisterType<Api.AddressService>().As<Api.IAddressService>();
            builder.RegisterType<Api.CustomerService>().As<Api.ICustomerService>();
            builder.RegisterType<Api.ProductService>().As<Api.IProductService>();
        }
    }
}
