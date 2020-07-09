namespace So.Grpc.FluentApi
{
    /// <summary>
    /// Contains message configurations required to build the runtime type model
    /// </summary>
    public interface IModelConfigurationOptions
    {
        /// <summary>
        /// Adds a message to the configuration
        /// </summary>
        /// <typeparam name="TMessage">Type of the added message</typeparam>
        /// <returns>Message configuration for <typeparamref name="TMessage"/> to configure fields if needed. The fields will be autoconfigured by default.</returns>
        IMessageConfiguration<TMessage> AddMessage<TMessage>();
    }
}
