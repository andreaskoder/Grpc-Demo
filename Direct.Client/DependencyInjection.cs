using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.Common;
using So.Demo.Common.Services;

namespace So.Demo.Direct.Client
{
    /// <summary>
    /// Adds necessary dependencies to Dependency Injection.
    /// </summary>
    public class DependencyInjection : IServiceClientDI
    {
        public void RegisterClient(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICustomerFactory, CustomerFactory>();
            services.AddSingleton<ICustomerServiceClient, CustomerServiceClient>();
        }
    }
}
