using System;
using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    /// <summary>
    /// Implementation of the <see cref="IMessageConfiguration<typeparamref name="TMessage"/>"/> interface
    /// </summary>
    /// <typeparam name="TMessage">Type of the configured message</typeparam>
    internal class MessageConfiguration<TMessage> : IMessageConfiguration<TMessage>
    {
        public Type MessageType => typeof(TMessage);

        /// <summary>
        /// Manually configured fields
        /// </summary>
        public IEnumerable<GrpcFieldDescriptor> ManualFields => _manualFields.AsReadOnly();
        private List<GrpcFieldDescriptor> _manualFields = new List<GrpcFieldDescriptor>();

        /// <summary>
        /// 
        /// </summary>
        bool IMessageConfiguration.AutoAddFieldsEnabled => _autoAddFields;
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
