using GalaSoft.MvvmLight;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.Grpc.Client;
using So.Demo.Grpc.Common.Services;
using So.GrpcDemo.ClientApp.ViewModel;
using So.GrpcDemo.ServiceClient.Grpc;
using System;

namespace So.GrpcDemo.ClientApp
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _services;

        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic) return;

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _services = serviceCollection.BuildServiceProvider();
        }

        internal void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IMultiplyService, MultiplyService>();
            services.AddSingleton<MainWindowViewModel>();
        }

        public MainWindowViewModel MainWindowViewModel => _services?.GetService<MainWindowViewModel>();
    }
}
