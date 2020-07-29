using AutoMapper;
using Grpc.Net.Client;
using So.Demo.Common.Services;

using System.Threading.Tasks;

namespace So.Demo.Grpc.ClientFromProto
{
    class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly IMapper _mapper;

        public CustomerServiceClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string Name => "Proto gRPC client";

        internal static string ServiceUri { get; set; }

        public async Task<Demo.Common.Responses.CustomersResponse> GetCustomersAsync(Demo.Common.Requests.CustomersRequest request)
        {
            using var channel = GrpcChannel.ForAddress(ServiceUri);
            var client = new CustomerServiceGrpc.CustomerServiceGrpcClient(channel);
            var grpcRequest = _mapper.Map<CustomersRequest>(request);
            var response = await client.GetCustomersAsyncAsync(grpcRequest);
            return _mapper.Map<Demo.Common.Responses.CustomersResponse>(response);
        }
    }
}
