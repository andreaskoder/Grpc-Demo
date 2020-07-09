using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using System.ServiceModel;
using System.Threading.Tasks;

namespace So.Demo.Grpc.Common.Services
{
    /// <summary>
    /// Service to handle operations with customers.
    /// At the moment this interface has to repeat the <see cref="So.Demo.Common.Services.ICustomerService"/>
    /// Simply deriving from it and adding the ServiceContract attribute doesn't work, so there's room for improvement.
    /// </summary>
    [ServiceContract]
    public interface ICustomerServiceGrpc
    {
        /// <summary>
        /// Gets a specified amount of customers from the customers repository
        /// </summary>
        /// <param name="request">Specifies which customers to get</param>
        /// <returns>A response containing customers or error message if something went wrong</returns>
        Task<CustomersResponse> GetCustomersAsync(CustomersRequest request);

    }
}
