using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;
using So.Demo.Grpc.Common.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Server.Services
{
    /// <summary>
    /// Implements the <see cref="ICustomerServiceGrpc"/> interface with gRPC
    /// </summary>
    public class CustomerServiceGrpc : ICustomerServiceGrpc
    {
        private readonly ICustomerFactory _customerFactory;

        public CustomerServiceGrpc(ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
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
            stopwatch.Stop();

            return Task.FromResult(new CustomersResponse
            {
                Customers = customers,
                Duration = stopwatch.ElapsedMilliseconds
            });
        }

    }
}
