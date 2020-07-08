using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    class ModelConfiguration : IModelConfigurationOptions
    {
        public IEnumerable<IMessageConfiguration> Messages => _messages.AsReadOnly();
        private readonly List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();

        public IMessageConfiguration<TMessage> AddMessage<TMessage>()
        {
            var newMessage = new MessageConfiguration<TMessage>();
            _messages.Add(newMessage);
            return newMessage;
        }
    }
}
