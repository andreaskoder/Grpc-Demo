using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using So.Demo.Grpc.Common;
using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System.Threading.Tasks;

namespace So.GrpcDemo.ServiceClient.Grpc
{
    public class CustomerService : ICustomerService
    {
        static CustomerService()
        {
            //Allow unencrypted transfer for demo purposes (we don't have a valid certificate)
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            ModelCreator.CreateModels();
            _channelOptions = new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 1024 * 1024 * 1024 // 1GB
            };
        }

        private static GrpcChannelOptions _channelOptions;
        public string Name => "Grpc client";

        public async Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {

            using (var channel = GrpcChannel.ForAddress("http://localhost:5000/", _channelOptions))
            {
                var service = channel.CreateGrpcService<ICustomerService>();
                return await service.GetCustomersAsync(request);
            }
        }
    }
}
