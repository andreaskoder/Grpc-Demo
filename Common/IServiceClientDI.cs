using Microsoft.Extensions.DependencyInjection;

namespace So.Demo.Common
{
    /// <summary>
    /// An interface used for late binding to register the client's implementation of defined services.
    /// Every client assembly should have exactly one class that implements this interface.
    /// </summary>
    public interface IServiceClientDI
    {
        /// <summary>
        /// Registers service implementations in for dependency injection
        /// </summary>
        /// <param name="services">Service collection to register into</param>
        void RegisterClient(IServiceCollection services);
    }
}
