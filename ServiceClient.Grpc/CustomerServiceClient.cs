using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using So.Demo.Grpc.Common;
using So.Demo.Common.Requests;
using So.Demo.Common.Responses;
using So.Demo.Grpc.Common.Services;
using System.Threading.Tasks;
using So.Demo.Common.Services;
using ProtoBuf.Meta;
using System.Linq;
using System;

namespace So.GrpcDemo.ServiceClient.Grpc
{
    /// <summary>
    /// Implements the <see cref="ICustomerService"/> interface as a gRPC-client.
    /// </summary>
    public class CustomerServiceClient : ICustomerServiceClient
    {
        static CustomerServiceClient()
        {
            //Allow unencrypted transfer for demo purposes (we don't have a valid certificate)
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            //Create models before any attempts to create a service proxy
            ModelCreator.CreateModels();

            //Save model as a proto file
            //var customerServiceDescription = new Service { Name = "CustomerService" };
            //foreach (var method in typeof(ICustomerServiceGrpc).GetMethods())
            //{
            //    var outputType = method.ReturnType;
            //    if (outputType.IsConstructedGenericType)
            //        outputType = outputType.GetGenericArguments().First();
            //    var serviceMethod = new ServiceMethod
            //    {
            //        InputType = method.GetParameters().First().ParameterType,
            //        Name = method.Name,
            //        OutputType = outputType
            //    };
            //    customerServiceDescription.Methods.Add(serviceMethod);                    
            //}

            //var schemaOptions = new SchemaGenerationOptions
            //{
            //    Flags = SchemaGenerationFlags.MultipleNamespaceSupport | SchemaGenerationFlags.PreserveSubType,
            //    Package = "CustomerService",
            //    Syntax = ProtoSyntax.Proto3,
            //    Services = { customerServiceDescription }
            //};
            //var schema = RuntimeTypeModel.Default.GetSchema(schemaOptions);

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
        public Task<CustomersResponse> GetCustomersAsync(CustomersRequest request)
            => InvokeFunctionAsync(service => service.GetCustomersAsync(request));

        protected async Task<T> InvokeFunctionAsync<T> (Func<ICustomerServiceGrpc, Task<T>> function)
        {
            using var channel = GrpcChannel.ForAddress(ServiceUri, _channelOptions);
            var service = channel.CreateGrpcService<ICustomerServiceGrpc>();
            return await function.Invoke(service);
        }
    }
}
