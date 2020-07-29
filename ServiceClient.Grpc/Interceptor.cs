using Castle.DynamicProxy;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using So.Demo.Common.Services;
using So.Demo.Grpc.Common;
using So.Demo.Grpc.Common.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace So.Demo.Grpc.Client
{
    class Interceptor : IInterceptor
    {
        static Dictionary<string, MethodInfo> _serviceMethods = new Dictionary<string, MethodInfo>();
        static Dictionary<string, MethodInfo> _clientMethods = new Dictionary<string, MethodInfo>();
        static ICustomerServiceGrpc _originalService;
        private static GrpcChannelOptions _channelOptions;
        internal static string ServiceUri { get; set; }

        static Interceptor()
        {
            foreach (var method in typeof(ICustomerServiceClient).GetMethods())
            {
                _clientMethods[MethodToString(method)] = method;
            }
            //Allow unencrypted transfer for demo purposes (we don't have a valid certificate)
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            //Create models before any attempts to create a service proxy
            ModelCreator.CreateModels();

            _channelOptions = new GrpcChannelOptions
            {
                MaxReceiveMessageSize = 1024 * 1024 * 1024 // 1GB
            };
        }

        private static void Initialize()
        {
            if (_originalService == null)
            {
                using var channel = GrpcChannel.ForAddress(ServiceUri, _channelOptions);
                _originalService = channel.CreateGrpcService<ICustomerServiceGrpc>();

                foreach (var method in _originalService.GetType().GetMethods())
                {
                    _serviceMethods[MethodToString(method)] = method;
                }
            }
        }

        public void Intercept(IInvocation invocation)
        {
            Initialize();
            var methodString = MethodToString(invocation.Method);
            if (_serviceMethods.TryGetValue(methodString, out var originalMethod))
            {
                invocation.ReturnValue = originalMethod.Invoke(_originalService, invocation.Arguments);
            }
            else invocation.Proceed();
        }

        private static string MethodToString(MethodInfo method)
        {
            var methodDescriptionBuilder = new StringBuilder(method.ReturnType?.FullName ?? "void")
                .Append(" ")
                .Append(method.Name)
                .Append("(");
            foreach (var parameter in method.GetParameters())
            {
                methodDescriptionBuilder
                    .Append(parameter.ParameterType.FullName)
                    .Append(", ");
            }
            if (methodDescriptionBuilder[methodDescriptionBuilder.Length - 1] == ' ')
                methodDescriptionBuilder.Length -= 2;
            methodDescriptionBuilder.Append(")");
            var methodDescription = methodDescriptionBuilder.ToString();
            return methodDescription;
        }
    }
}
