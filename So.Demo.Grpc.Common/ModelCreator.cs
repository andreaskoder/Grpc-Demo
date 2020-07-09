using ProtoBuf.Meta;
using So.Demo.Common.Entities;
using So.Grpc.FluentApi;

namespace So.Demo.Grpc.Common
{
    public static class ModelCreator
    {
        public static void CreateModels()
        {
            var model = RuntimeTypeModel.Default;
            GrpcTypeModel.Configure(cfg =>
            {
                cfg.AddMessage<Customer>(); // Add Customer with auto-properties
            });
            //Test the model
            var customer = new Customer { Int1 = 1, String1 = "test" };
            model.DeepClone(customer);            
        }
    }
}
