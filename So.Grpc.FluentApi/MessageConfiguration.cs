using System;
using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    internal class MessageConfiguration<TMessage> : IMessageConfiguration<TMessage>
    {
        public Type MessageType => typeof(TMessage);
        public IEnumerable<GrpcFieldDescriptor> ManualFields => _manualFields.AsReadOnly();
        private List<GrpcFieldDescriptor> _manualFields = new List<GrpcFieldDescriptor>();

        bool IMessageConfiguration.AutoAddFields => _autoAddFields;
        private bool _autoAddFields = true;

        public IMessageConfiguration<TMessage> With(string fieldName, int order)
        {
            _manualFields.Add(new GrpcFieldDescriptor(order, fieldName));
            return this;
        }

        public void AutoAddFields(bool autoAddFields = true)
        {
            _autoAddFields = autoAddFields;
        }
    }
}
