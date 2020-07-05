using System;
using System.Collections.Generic;
using System.Text;

namespace So.Demo.Grpc.Common
{
    class ModelConfigurationOptions : IModelConfigurationOptions
    {
        public IMessageConfiguration<TMessage> AddMessage<TMessage>()
        {
            throw new NotImplementedException();
        }
    }
}
