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
    internal partial class InternalCustomClassificationResult
    {
        internal static InternalCustomClassificationResult DeserializeInternalCustomClassificationResult(JsonElement element)
        {
            IReadOnlyList<DocumentCustomClassification> documents = default;
            IReadOnlyList<DocumentError> errors = default;
            Optional<TextDocumentBatchStatistics> statistics = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("documents"))
                {
                    List<DocumentCustomClassification> array = new List<DocumentCustomClassification>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(DocumentCustomClassification.DeserializeDocumentCustomClassification(item));
                    }
                    documents = array;
                    continue;
                }
                if (property.NameEquals("errors"))
                {
                    List<DocumentError> array = new List<DocumentError>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(DocumentError.DeserializeDocumentError(item));
                    }
                    errors = array;
                    continue;
                }
                if (property.NameEquals("statistics"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    statistics = TextDocumentBatchStatistics.DeserializeTextDocumentBatchStatistics(property.Value);
                    continue;
                }
            }
            return new InternalCustomClassificationResult(documents, errors, statistics.Value);
        }
    }
}
