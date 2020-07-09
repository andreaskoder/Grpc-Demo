using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.Common;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace So.Demo.ServiceClientLoader
{
    /// <summary>
    /// An entry point to find the client and register it for dependency injection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// The loader looks into configuration for service client parameters.
        /// A configuration must have a ServiceClient section with AssemblyPath parameter.
        /// The path is relative to the startup directory
        /// </summary>
        /// <param name="services">Service collection to pass to the client</param>
        /// <param name="configuration">Configuration containing assembly path</param>
        public static void LoadServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            //Check if the assembly path is configured
            var assemblyFileName = configuration.GetSection("ServiceClient")["AssemblyPath"];
            if (string.IsNullOrWhiteSpace(assemblyFileName))
                throw new InvalidOperationException("The assembly path is not defined in configuration. Please supply the AssemblyPath value in the ServiceClient section.");

            //Check if the assembly exists
            if (!File.Exists(assemblyFileName))
                throw new FileNotFoundException($"The service client assembly wasn't found. Tried '{assemblyFileName}' from configuration.");

            //Load the assembly
            var assembly = Assembly.LoadFrom(assemblyFileName);
            
            //Find the class implementing IServiceClientDI interface and instantiate it
            var types = assembly.GetTypes().ToList();
            var diType = types.FirstOrDefault(t => t.GetInterfaces().Any(i => i == typeof(IServiceClientDI)));
            if (diType is null)
                throw new InvalidOperationException($"The Assembly '{assembly.GetName().Name}' does not contain any class that implements 'So.Demo.Common.IServiceClientDI'");
            var diInstance = (IServiceClientDI)Activator.CreateInstance(diType);
            
            //Register the client
            diInstance.RegisterClient(services);
        }
    }
}
