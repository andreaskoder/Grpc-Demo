using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace So.Demo.Grpc.ServerWithMapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options =>
                {
                    options.ListenLocalhost(5000, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                })
                .UseStartup<Startup>();
    }
}
