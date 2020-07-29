using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using System.Diagnostics;
using System.Threading.Tasks;

namespace So.Demo.Common.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerFactory _customerFactory;

        public CustomerService(ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
        }

        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var customers = _customerFactory.CreateRandom(request.CustomersCount);
            stopwatch.Stop();
            var response = new CustomersResponse
            {
                Customers = customers,
                Duration = stopwatch.ElapsedMilliseconds
            };
            return Task.FromResult(response);
        }
    }
}
