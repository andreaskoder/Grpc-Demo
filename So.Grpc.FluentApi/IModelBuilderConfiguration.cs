namespace So.Grpc.FluentApi
{
    public interface IModelConfigurationOptions
    {
        IMessageConfiguration<TMessage> AddMessage<TMessage>();
    }
}
