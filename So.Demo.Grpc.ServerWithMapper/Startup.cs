using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;
using So.Demo.Common.Entities;
using So.Demo.Grpc.Common;
using So.Demo.Grpc.ServerWithMapper.CoreEntities;
using So.Demo.Grpc.ServerWithMapper.Services;

namespace So.Demo.Grpc.ServerWithMapper
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ModelCreator.CreateModels(); //Configure models so that the service could find all schemas
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
                config.MaxSendMessageSize = 1024 * 1024 * 1024; //1GB
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<CustomerEntity, Customer>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CustomerServiceGrpc>();
            });
        }
    }
}
