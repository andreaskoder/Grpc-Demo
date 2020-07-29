namespace So.Demo.Common.Services
{
    public interface ICustomerServiceClient: ICustomerService
    {
        /// <summary>
        /// Name of the current interface implementation
        /// </summary>
        string Name { get; }
    }
}
