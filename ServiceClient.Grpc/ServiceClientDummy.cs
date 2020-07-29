using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;
using System;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Client
{
    class ServiceClientDummy : ICustomerServiceClient
    {
        public string Name => "Grpc client";

        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
