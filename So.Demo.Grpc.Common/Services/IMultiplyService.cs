using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using System.ServiceModel;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Common.Services
{
    [ServiceContract]
    public interface IMultiplyService
    {
        Task<MultiplyResponse> MultiplyAsync(MultiplyRequest request);
    }
}
