using System;
using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    public interface IMessageConfiguration
    {
        Type MessageType { get; }
        IEnumerable<GrpcFieldDescriptor> ManualFields { get; }
        bool AutoAddFields { get; }
    }

    public interface IMessageConfiguration<TMessage> : IMessageConfiguration
    {
        IMessageConfiguration<TMessage> With(string propertyName, int order);
    }
}
