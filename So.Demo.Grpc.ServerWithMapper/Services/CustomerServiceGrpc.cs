using AutoMapper;
using So.Demo.Common.Entities;
using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;
using So.Demo.Grpc.Common.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace So.Demo.Grpc.ServerWithMapper.Services
{
    /// <summary>
    /// Implements the <see cref="ICustomerServiceGrpc"/> interface with gRPC
    /// </summary>
    public class CustomerServiceGrpc : ICustomerServiceGrpc
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly IMapper _mapper;

        public CustomerServiceGrpc(ICustomerFactory customerFactory, IMapper mapper)
        {
            _customerFactory = customerFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// This implementation generates random customers for test purposes.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var customers = _customerFactory.CreateRandom(request.CustomersCount);
            var response = new CustomersResponse
            {
                Customers = _mapper.Map<List<Customer>>(customers)
            };
            stopwatch.Stop();
            response.Duration = stopwatch.ElapsedMilliseconds;
            return Task.FromResult(response);
        }
    }
}
