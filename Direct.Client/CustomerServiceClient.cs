using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace So.Demo.Direct.Client
{
    /// <summary>
    /// Direct access is supposed to access the db directly, so it just generates the customers and gives them back
    /// </summary>
    class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly ICustomerFactory _customerFactory;

        public CustomerServiceClient(ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
        }

        public string Name => "Direct Db access";

        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var customers = _customerFactory.CreateRandom(request.CustomersCount);
            stopwatch.Stop();
            return Task.FromResult(
                new CustomersResponse
                {
                    Customers = customers,
                    Duration = stopwatch.ElapsedMilliseconds
                });
        }
    }
}
