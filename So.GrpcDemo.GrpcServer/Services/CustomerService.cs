using So.Demo.Common.Entities;
using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Server.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var customers = new List<Customer>();
            for (var i = 1; i < request.CustomersCount + 1; i++)
            {
                customers.Add(new Customer
                {
                    Id = i,
                    Name = $"Customer {i}"
                });
            }
            return Task.FromResult(new CustomersResponse
            {
                Customers = customers
            });
        }
    }
}
