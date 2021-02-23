// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.AI.DocumentTranslation.Models
{
    public partial class StorageSourceListResult
    {
        internal static StorageSourceListResult DeserializeStorageSourceListResult(JsonElement element)
        {
            IReadOnlyList<StorageSource> value = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"))
                {
                    List<StorageSource> array = new List<StorageSource>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(new StorageSource(item.GetString()));
                    }
                    value = array;
                    continue;
                }
            }
            return new StorageSourceListResult(value);
        }
    }
}
