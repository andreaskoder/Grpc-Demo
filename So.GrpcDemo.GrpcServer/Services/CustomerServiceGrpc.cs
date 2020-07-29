using So.Demo.Common.Services;
using So.Demo.Grpc.Common.Services;

namespace So.Demo.Grpc.Server.Services
{
    public class CustomerServiceGrpc : CustomerService, ICustomerServiceGrpc
    {
        public CustomerServiceGrpc(ICustomerFactory customerFactory) : base(customerFactory)
        {
        }
    }
}
