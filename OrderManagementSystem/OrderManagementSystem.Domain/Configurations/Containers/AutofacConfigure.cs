using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OrderManagementSystem.Domain.Configurations.Containers
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
            builder.RegisterType<Services.DbConnection>().As<Services.IDbConnection>().AsSelf().SingleInstance();

            builder.RegisterType<Services.Databases.SqlServer.Order.OrderService>()
                .As<Services.Databases.SqlServer.Order.IOrderService>();

            builder.RegisterType<Services.Databases.SqlServer.Order.OrderItemService>()
                .As<Services.Databases.SqlServer.Order.IOrderItemService>();

            builder.RegisterType<Services.Databases.SqlServer.Customer.AddressService>()
                .As<Services.Databases.SqlServer.Customer.IAddressService>();

            builder.RegisterType<Services.Databases.SqlServer.Customer.CustomerService>()
                .As<Services.Databases.SqlServer.Customer.ICustomerService>();

            builder.RegisterType<Services.Databases.SqlServer.Product.ProductService>()
                .As<Services.Databases.SqlServer.Product.IProductService>();
        }
    }
}
