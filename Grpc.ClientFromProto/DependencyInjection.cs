using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So.Demo.Common;
using So.Demo.Common.Services;
using System;

namespace So.Demo.Grpc.ClientFromProto
{
    class DependencyInjection : IServiceClientDI
    {
        public void RegisterClient(IServiceCollection services, IConfiguration configuration)
        {
            var serviceUri = configuration["ServiceUri"];
            if (string.IsNullOrWhiteSpace(serviceUri))
                throw new InvalidOperationException($"The required 'ServiceUri' configuration value is not supplied");
            CustomerServiceClient.ServiceUri = serviceUri;

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<ProtoBuf.Bcl.DateTime, DateTime>()
                    .ConvertUsing(src => ConvertDateTime(src));
                cfg.CreateMap<ProtoBuf.Bcl.Decimal, decimal>();
                cfg.CreateMap<ProtoBuf.Bcl.Guid, Guid>();
                cfg.CreateMap<Customer, Demo.Common.Entities.Customer>();
                cfg.CreateMap<CustomersResponse, Demo.Common.Responses.CustomersResponse>();
                cfg.CreateMap<Demo.Common.Requests.CustomersRequest, CustomersRequest>();
            });
            services.AddSingleton<ICustomerServiceClient, CustomerServiceClient>();
        }

        private DateTime ConvertDateTime(ProtoBuf.Bcl.DateTime source) 
            => source.Scale switch
        {
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Ticks => DateTime.UnixEpoch.AddTicks(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Milliseconds => DateTime.UnixEpoch.AddMilliseconds(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Days => DateTime.UnixEpoch.AddDays(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Hours => DateTime.UnixEpoch.AddHours(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Minutes => DateTime.UnixEpoch.AddMinutes(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Seconds => DateTime.UnixEpoch.AddSeconds(source.Value),
            ProtoBuf.Bcl.DateTime.Types.TimeSpanScale.Minmax => DateTime.UnixEpoch,
            _ => DateTime.MinValue,
        };
    }
}
