// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.Luis.Models
{
    public partial class PredictionRequest : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("query");
            writer.WriteStringValue(Query);
            if (Optional.IsDefined(Options))
            {
                writer.WritePropertyName("options");
                writer.WriteObjectValue(Options);
            }
            if (Optional.IsCollectionDefined(ExternalEntities))
            {
                writer.WritePropertyName("externalEntities");
                writer.WriteStartArray();
                foreach (var item in ExternalEntities)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsCollectionDefined(DynamicLists))
            {
                writer.WritePropertyName("dynamicLists");
                writer.WriteStartArray();
                foreach (var item in DynamicLists)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
    }
}
