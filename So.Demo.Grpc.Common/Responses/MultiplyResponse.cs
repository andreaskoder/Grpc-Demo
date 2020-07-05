using System.Runtime.Serialization;

namespace So.Demo.Grpc.Common.Responses
{
    [DataContract]
    public class MultiplyResponse
    {
        [DataMember(Order = 1)]
        public int Result { get; set; }
    }
}
