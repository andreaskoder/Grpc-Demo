using System;
using System.Collections.Generic;

namespace So.Grpc.FluentApi
{
    /// <summary>
    /// An abstraction of the message configuration once it is configured
    /// </summary>
    public interface IMessageConfiguration
    {
        /// <summary>
        /// Type of the configured message
        /// </summary>
        Type MessageType { get; }

        /// <summary>
        /// Manually configured fields
        /// </summary>
        IEnumerable<GrpcFieldDescriptor> ManualFields { get; }

        /// <summary>
        /// If the not configured fields must be configured automatically. True by default
        /// </summary>
        bool AutoAddFieldsEnabled { get; }
    }

    /// <summary>
    /// Defines configuration of the message's fields
    /// </summary>
    /// <typeparam name="TMessage">Type of the configured message</typeparam>
    public interface IMessageConfiguration<TMessage> : IMessageConfiguration
    {
        /// <summary>
        /// Manually configure a field.
        /// </summary>
        /// <param name="fieldName">Name of the message field/class property</param>
        /// <param name="order">Order of the field during serialization</param>
        /// <returns>Message configuration to continue adding fields</returns>
        IMessageConfiguration<TMessage> With(string fieldName, int order);

        /// <summary>
        /// Specify if the not configured fields should be added automatically (default behavior).
        /// This method should come last during configuration.
        /// There's no need to use it if you need auto add.
        /// </summary>
        /// <param name="autoaddFields">If the fields should be added automatically</param>
        void AutoAddFields(bool autoaddFields = true);
    }
}
