﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProtoBuf.Meta;
using So.Demo.Common.Entities;

namespace So.Demo.Grpc.Common
{
    public static class ModelCreator
    {
        public static void CreateModels()
        {
            var model = RuntimeTypeModel.Default;
            var customer = new Customer { Id = 1, Name = "test" };
            var customerType = typeof(Customer);
            model.Add(customerType, true);
            var order = 1;
            var properties = customerType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();
            foreach (var property in properties)
                model[customerType].Add(order++, property.Name);

            model.DeepClone(customer);
        }
    }
}