using So.Demo.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace So.Demo.Common.Services
{
    public class CustomerFactory : ICustomerFactory
    {
        private static readonly List<PropertyInfo> _customerProperties;

        static CustomerFactory()
        {
            _customerProperties = GetProperties<Customer>();
        }

        public List<Customer> CreateRandom(int amount)
        {
            var customers = new List<Customer>();
            for (var i = 0; i < amount; i++)
            {
                var customer = new Customer();
                foreach (var property in _customerProperties)
                    AssignRandomValue(customer, property);
                customers.Add(customer);
            }
            return customers;
        }

        private static List<PropertyInfo> GetProperties<TEntity>()
        {
            return typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();
        }

        /// <summary>
        /// Populates entity's property with a random value
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="info"></param>
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
