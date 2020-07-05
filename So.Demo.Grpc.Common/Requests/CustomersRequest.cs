using System.Runtime.Serialization;

namespace So.Demo.Grpc.Common.Requests
{
    [DataContract]
    public class CustomersRequest : RequestBase
    {
        [DataMember(Order = 1)]
        public int CustomersCount { get; set; }
    }
}
