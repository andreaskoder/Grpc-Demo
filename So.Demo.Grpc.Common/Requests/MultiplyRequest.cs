using System.Runtime.Serialization;

namespace So.Demo.Grpc.Common.Requests
{
    [DataContract]
    public class MultiplyRequest
    {
        [DataMember(Order = 1)]
        public int X { get; set; }

        [DataMember(Order = 2)]
        public int Y { get; set; }
    }
}
