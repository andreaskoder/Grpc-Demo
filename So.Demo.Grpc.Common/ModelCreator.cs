using So.Demo.Common.Entities;
using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Grpc.FluentApi;

namespace So.Demo.Grpc.Common
{
    public static class ModelCreator
    {
        /// <summary>
        /// The Grpc models are defined here. In order to skip mapping, we will use the common entity, request and response.
        /// We have to add all the required classes to the default model.
        /// This model creator is supposed to be used in both server and client to have identical models
        /// </summary>
        public static void CreateModels()
        {
            GrpcTypeModel.Configure(cfg =>
            {
                cfg.AddMessage<Customer>(); // Add Customer first, since it is used in the response
                cfg.AddMessage<CustomersRequest>();
                cfg.AddMessage<CustomersResponse>();
            });
        }
    }
}
