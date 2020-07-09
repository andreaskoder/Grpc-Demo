using So.Demo.Common.Entities;
using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Server.Services
{
    public class CustomerService : ICustomerService
    {
        public string Name => "Grpc Server";

        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var customerProperties = GetProperties<Customer>();
            var customers = new List<Customer>();
            for (var i = 1; i < request.CustomersCount + 1; i++)
            {
                var customer = new Customer();
                foreach (var property in customerProperties)
                    AssignRandomValue(customer, property);
                customers.Add(customer);
            }
            return Task.FromResult(new CustomersResponse
            {
                Customers = customers
            });
        }

        private List<PropertyInfo> GetProperties<TEntity>()
        {
            return typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();
        }

        private void AssignRandomValue<TEntity>(TEntity entity, PropertyInfo info)
        {
            var random = new Random();
            if (info.PropertyType == typeof(int))
            {
                info.SetValue(entity, random.Next());
            }
            else if (info.PropertyType == typeof(double))
            {
                info.SetValue(entity, random.NextDouble());
            }
            else if (info.PropertyType == typeof(decimal))
            {
                info.SetValue(entity, (decimal)random.NextDouble());
            }
            else if (info.PropertyType == typeof(Guid))
            {
                info.SetValue(entity, Guid.NewGuid());
            }
            else if (info.PropertyType == typeof(DateTime))
            {
                info.SetValue(entity, new DateTime(2000, 1, 1).AddMinutes(random.Next()));
            }
            else if (info.PropertyType == typeof(string))
            {
                info.SetValue(entity, Guid.NewGuid().ToString());
            }
        }
    }
}
