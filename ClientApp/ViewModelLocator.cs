using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.ServiceClientLoader;
using So.GrpcDemo.ClientApp.ViewModel;
using System;

namespace So.GrpcDemo.ClientApp
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _services;

        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic) return;

            Configuration = CreateConfiguration();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _services = serviceCollection.BuildServiceProvider();
        }

        private IConfiguration CreateConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false);
            return configurationBuilder.Build();
        }

        internal void ConfigureServices(IServiceCollection services)
        {
            services.LoadServiceClient(Configuration);
            services.AddSingleton<MainWindowViewModel>();
        }

        public IConfiguration Configuration { get; }

        public MainWindowViewModel MainWindowViewModel => _services?.GetService<MainWindowViewModel>();
    }
}
