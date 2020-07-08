namespace So.Grpc.FluentApi
{
    public struct GrpcFieldDescriptor
    {
        public GrpcFieldDescriptor(int number, string name)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; }
        public int Number { get; }
    }
}
