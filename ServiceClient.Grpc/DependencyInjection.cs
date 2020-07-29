using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.Common;
using So.Demo.Common.Services;
using So.GrpcDemo.ServiceClient.Grpc;
using System;

namespace So.Demo.Grpc.Client
{
    /// <summary>
    /// Adds necessary dependencies to Dependency Injection.
    /// </summary>
    public class DependencyInjection : IServiceClientDI
    {
        public void RegisterClient(IServiceCollection services, IConfiguration configuration)
        {
            var serviceUri = configuration["ServiceUri"];
            if (string.IsNullOrWhiteSpace(serviceUri))
                throw new InvalidOperationException($"The required 'ServiceUri' configuration value is not supplied");
            CustomerServiceClient.ServiceUri = serviceUri;

            services.AddSingleton<ICustomerServiceClient, CustomerServiceClient>();
            
            //Interceptor.ServiceUri = serviceUri;
            //var interceptor = new Interceptor();
            //var generator = new ProxyGenerator();
            //services.AddSingleton(services =>
            //    generator.CreateInterfaceProxyWithTarget<ICustomerServiceClient>(new ServiceClientDummy(), interceptor));
        }
    }
}
