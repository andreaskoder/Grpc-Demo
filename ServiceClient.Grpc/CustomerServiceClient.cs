using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using So.Demo.Grpc.Common;
using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System.Threading.Tasks;
using So.Demo.Common.Services;

namespace So.GrpcDemo.ServiceClient.Grpc
{
    /// <summary>
    /// Implements the <see cref="ICustomerService"/> interface as a gRPC-client.
    /// </summary>
    public class CustomerServiceClient : ICustomerService
    {
        static CustomerServiceClient()
        {
            //Allow unencrypted transfer for demo purposes (we don't have a valid certificate)
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            //Create models before any attempts to create a service proxy
            ModelCreator.CreateModels();
            _channelOptions = new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 1024 * 1024 * 1024 // 1GB
            };
        }


        private static GrpcChannelOptions _channelOptions;
        internal static string ServiceUri { get; set; }

        public string Name => "Grpc client";

        /// <summary>
        /// Request and response are taken from Common, no mapping required
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            using (var channel = GrpcChannel.ForAddress(ServiceUri, _channelOptions))
            {
                var service = channel.CreateGrpcService<ICustomerServiceGrpc>();
                return await service.GetCustomersAsync(request);
            }
        }
    }
}
