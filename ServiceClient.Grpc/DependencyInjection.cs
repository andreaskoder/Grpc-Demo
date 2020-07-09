using Microsoft.Extensions.DependencyInjection;
using So.Demo.Common;
using So.Demo.Common.Services;
using So.GrpcDemo.ServiceClient.Grpc;

namespace So.Demo.Grpc.Client
{
    /// <summary>
    /// Adds necessary dependencies to Dependency Injection.
    /// </summary>
    public class DependencyInjection : IServiceClientDI
    {
        public void RegisterClient(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, CustomerServiceClient>();
        }
    }
}
