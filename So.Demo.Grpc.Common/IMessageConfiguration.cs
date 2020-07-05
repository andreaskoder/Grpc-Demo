using System;
using System.Collections.Generic;
using System.Text;

namespace So.Demo.Grpc.Common
{
    public interface IMessageConfiguration<TMessage>
    {
        IMessageConfiguration<TMessage> With(string propertyName, int order);
    }
}
