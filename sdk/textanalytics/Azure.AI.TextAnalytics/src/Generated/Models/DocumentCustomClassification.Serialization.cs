// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.AI.TextAnalytics;
using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    internal partial class DocumentCustomClassification
    {
        internal static DocumentCustomClassification DeserializeDocumentCustomClassification(JsonElement element)
        {
            string id = default;
            Classification classification = default;
            IReadOnlyList<TextAnalyticsWarningInternal> warnings = default;
            Optional<TextDocumentStatistics> statistics = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("classification"))
                {
                    classification = Classification.DeserializeClassification(property.Value);
                    continue;
                }
                if (property.NameEquals("warnings"))
                {
                    List<TextAnalyticsWarningInternal> array = new List<TextAnalyticsWarningInternal>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(TextAnalyticsWarningInternal.DeserializeTextAnalyticsWarningInternal(item));
                    }
                    warnings = array;
                    continue;
                }
                if (property.NameEquals("statistics"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    statistics = TextDocumentStatistics.DeserializeTextDocumentStatistics(property.Value);
                    continue;
                }
            }
            return new DocumentCustomClassification(id, classification, warnings, Optional.ToNullable(statistics));
        }
    }
}
