using System.Collections.Generic;

namespace So.Demo.Grpc.Common
{
    internal class MessageConfiguration<TMessage> : IMessageConfiguration<TMessage>, IMessageConfiguration
    {
        public Dictionary<int, string> Properties { get; } = new Dictionary<int, string>();
        public IMessageConfiguration<TMessage> With(string propertyName, int order)
        {
            Properties[order] = propertyName;
            return this;
        }
    }
}
