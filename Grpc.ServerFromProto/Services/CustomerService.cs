using AutoMapper;
using Grpc.Core;
using So.Demo.Common.Services;
using So.Demo.Grpc.ServerFromProto;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Grpc.ServerFromProto.Services
{
    public class CustomerService: CustomerServiceGrpc.CustomerServiceGrpcBase
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerFactory customerFactory, IMapper mapper)
        {
            _customerFactory = customerFactory;
            _mapper = mapper;
        }

        public override Task<CustomersResponse> GetCustomersAsync(CustomersRequest request, ServerCallContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = new CustomersResponse();
            var customers = _customerFactory.CreateRandom(request.CustomersCount);
            response.Customers.AddRange(_mapper.Map<List<Customer>>(customers));
            stopwatch.Stop();
            response.Duration = stopwatch.ElapsedMilliseconds;
            return Task.FromResult(response);
        }
    }
}
