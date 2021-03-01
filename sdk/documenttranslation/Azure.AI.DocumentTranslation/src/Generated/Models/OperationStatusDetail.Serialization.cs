// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Azure.AI.DocumentTranslation.Models
{
    public partial class OperationStatusDetail
    {
        internal static OperationStatusDetail DeserializeOperationStatusDetail(JsonElement element)
        {
            string id = default;
            DateTimeOffset createdDateTimeUtc = default;
            DateTimeOffset lastActionDateTimeUtc = default;
            TranslationStatus status = default;
            Optional<DocumentTranslationError> error = default;
            StatusSummary summary = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("createdDateTimeUtc"))
                {
                    createdDateTimeUtc = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("lastActionDateTimeUtc"))
                {
                    lastActionDateTimeUtc = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("status"))
                {
                    status = new TranslationStatus(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("error"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    error = DocumentTranslationError.DeserializeDocumentTranslationError(property.Value);
                    continue;
                }
                if (property.NameEquals("summary"))
                {
                    summary = StatusSummary.DeserializeStatusSummary(property.Value);
                    continue;
                }
            }
            return new OperationStatusDetail(id, createdDateTimeUtc, lastActionDateTimeUtc, status, error.Value, summary);
        }
    }
}
