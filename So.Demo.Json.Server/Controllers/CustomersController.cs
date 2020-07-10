using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;

namespace So.Demo.Json.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerFactory _customerFactory;

        public CustomersController(ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
        }

        [HttpGet]
        public CustomersResponse Get(int amount)
        {
            var stopwatch = Stopwatch.StartNew();
            var customers = _customerFactory.CreateRandom(amount);
            stopwatch.Stop();

            return new CustomersResponse
            {
                Customers = customers,
                Duration = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
