using So.Demo.Common.Entities;
using System.Collections.Generic;

namespace So.Demo.Common.Responses
{
    public class CustomersResponse : ResponseBase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
