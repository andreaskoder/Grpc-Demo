using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    /// <summary>
    /// Implementation of the <see cref="IModelConfigurationOptions"/> interface.
    /// </summary>
    class ModelConfiguration : IModelConfigurationOptions
    {
        /// <summary>
        /// Configured messages
        /// </summary>
        public IEnumerable<IMessageConfiguration> Messages => _messages.AsReadOnly();
        private readonly List<IMessageConfiguration> _messages = new List<IMessageConfiguration>();


        IMessageConfiguration<TMessage> IModelConfigurationOptions.AddMessage<TMessage>()
        {
            var newMessage = new MessageConfiguration<TMessage>();
            _messages.Add(newMessage);
            return newMessage;
        }
    }
}
