// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.AI.TextAnalytics;
using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    internal partial class CustomClassificationTasksItem
    {
        internal static CustomClassificationTasksItem DeserializeCustomClassificationTasksItem(JsonElement element)
        {
            InternalCustomClassificationResult results = default;
            DateTimeOffset lastUpdateDateTime = default;
            Optional<string> name = default;
            TextAnalyticsOperationStatus status = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("results"))
                {
                    results = InternalCustomClassificationResult.DeserializeInternalCustomClassificationResult(property.Value);
                    continue;
                }
                if (property.NameEquals("lastUpdateDateTime"))
                {
                    lastUpdateDateTime = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("status"))
                {
                    status = new TextAnalyticsOperationStatus(property.Value.GetString());
                    continue;
                }
            }
            return new CustomClassificationTasksItem(lastUpdateDateTime, name.Value, status, results);
        }
    }
}
