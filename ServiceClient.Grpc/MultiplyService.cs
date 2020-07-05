using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Client
{
    public class MultiplyService : Common.Services.IMultiplyService
    {
        public async Task<MultiplyResponse> MultiplyAsync(MultiplyRequest request)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            using (var channel = GrpcChannel.ForAddress("http://localhost:5000"))
            {
                var calculator = channel.CreateGrpcService<Common.Services.IMultiplyService>();
                return await calculator.MultiplyAsync(request);
            }
        }
    }
}
