using So.Demo.Common.Entities;
using So.Demo.Grpc.Common.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace So.Demo.Grpc.Common.Responses
{
    [DataContract]
    public class CustomersResponse : ResponseBase
    {
        [DataMember(Order = 1)]
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
