// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.Luis.Models
{
    public partial class ExternalEntity : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("entityName");
            writer.WriteStringValue(EntityName);
            writer.WritePropertyName("startIndex");
            writer.WriteNumberValue(StartIndex);
            writer.WritePropertyName("entityLength");
            writer.WriteNumberValue(EntityLength);
            if (Optional.IsDefined(Resolution))
            {
                writer.WritePropertyName("resolution");
                writer.WriteObjectValue(Resolution);
            }
            if (Optional.IsDefined(Score))
            {
                writer.WritePropertyName("score");
                writer.WriteNumberValue(Score.Value);
            }
            writer.WriteEndObject();
        }
    }
}
