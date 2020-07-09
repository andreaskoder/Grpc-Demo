namespace So.Grpc.FluentApi
{
    /// <summary>
    /// Contains information required to arrange a field
    /// </summary>
    public struct GrpcFieldDescriptor
    {
        public GrpcFieldDescriptor(int number, string name)
        {
            Name = name;
            Number = number;
        }
        /// <summary>
        /// Name of the message field, should be equal to the name of the class property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Field number in the message structure
        /// </summary>
        public int Number { get; }
    }
}
