using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using System.Threading.Tasks;

namespace So.Demo.Common.Services
{
    /// <summary>
    /// Service to handle operations with customers.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Gets a specified amount of customers from the customers repository
        /// </summary>
        /// <param name="request">Specifies which customers to get</param>
        /// <returns>A response containing customers or error message if something went wrong</returns>
        Task<CustomersResponse> GetCustomersAsync(CustomersRequest request);

        /// <summary>
        /// Name of the current interface implementation
        /// </summary>
        string Name { get; }
    }
}
