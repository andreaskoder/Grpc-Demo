using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace So.Grpc.FluentApi
{
    /// <summary>
    /// Adds classes to the default RuntimTypeModel, so that they could be used by the gRPC serializer
    /// </summary>
    public static class GrpcTypeModel
    {
        /// <summary>
        /// Configures the models.
        /// </summary>
        /// <param name="cfg">Add messages and their fields in the config</param>
        public static void Configure(Action<IModelConfigurationOptions> cfg)
        {
            ApplyConfiguration(RuntimeTypeModel.Default, cfg);
        }

        /// <summary>
        /// A latch that can be used by unit-tests
        /// </summary>
        /// <param name="model">A RuntimeTypeModel to configure. Normally a Default model.</param>
        /// <param name="configureModel">Configuration from client</param>
        internal static void ApplyConfiguration(RuntimeTypeModel model, Action<IModelConfigurationOptions> configureModel)
        {
            var configuration = new ModelConfiguration();
            configureModel(configuration);

            foreach (var message in configuration.Messages)
            {
                model.Add(message.MessageType, true);
                var existingFields = new Dictionary<int, string>();
                foreach (var property in message.ManualFields)
                {
                    model[message.MessageType].Add(property.Number, property.Name);
                    if (existingFields.TryGetValue(property.Number, out var existingFieldName))
                        throw new InvalidOperationException(
                            $"Duplicate manual field number assignment: FieldNumber {property.Number}, Fields 'existingFieldName' and '{property.Name}'");
                    existingFields[property.Number] = property.Name;
                }
                if (message.AutoAddFieldsEnabled)
                {
                    var order = 1;
                    var properties = message.MessageType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead && p.CanWrite)
                        .ToList();

                    foreach (var property in properties)
                    {
                        while (existingFields.ContainsKey(order))
                            order++;

                        if (!existingFields.ContainsValue(property.Name))
                        {
                            existingFields[order] = property.Name;
                            model[message.MessageType].Add(order, property.Name);
                        }
                    }
                }

            }
        }
    }
}
