// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.AI.DocumentTranslation.Models
{
    public partial class DocumentStatusResponse
    {
        internal static DocumentStatusResponse DeserializeDocumentStatusResponse(JsonElement element)
        {
            Optional<IReadOnlyList<DocumentStatusDetail>> value = default;
            Optional<string> nextLink = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<DocumentStatusDetail> array = new List<DocumentStatusDetail>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(DocumentStatusDetail.DeserializeDocumentStatusDetail(item));
                    }
                    value = array;
                    continue;
                }
                if (property.NameEquals("@nextLink"))
                {
                    nextLink = property.Value.GetString();
                    continue;
                }
            }
            return new DocumentStatusResponse(Optional.ToList(value), nextLink.Value);
        }
    }
}
