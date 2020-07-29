using RestSharp;
using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Common.Services;
using System.Threading.Tasks;

namespace So.Demo.Json.Client
{
    class CustomerServiceClient : ICustomerServiceClient
    {
        public static string ServiceUri { get; set; }

        public string Name => "Json client";

        public async Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
        {
            var client = new RestClient(ServiceUri);
            var restRequest = new RestRequest($"Customers/?amount={request.CustomersCount}");
            var response = await client.GetAsync<CustomersResponse>(restRequest);
            return response;
        }
    }
}
