using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Server.Services
{
    public class MultiplyService : IMultiplyService
    {
        public Task<MultiplyResponse> MultiplyAsync(MultiplyRequest request)
        {
            return Task.FromResult(new MultiplyResponse { Result = request.X * request.Y });
        }
    }
}
