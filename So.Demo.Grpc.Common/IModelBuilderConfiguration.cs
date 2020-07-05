using System;
using System.Collections.Generic;
using System.Text;

namespace So.Demo.Grpc.Common
{
    public interface IModelConfigurationOptions
    {
        IMessageConfiguration<TMessage> AddMessage<TMessage>();
    }
}
