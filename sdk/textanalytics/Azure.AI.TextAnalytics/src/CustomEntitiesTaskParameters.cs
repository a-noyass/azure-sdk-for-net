// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    [CodeGenModel("CustomEntitiesTaskParameters")]
    public partial class CustomEntitiesTaskParameters
    {
        internal StringIndexType StringIndexType { get; set; } = StringIndexType.Utf16CodeUnit;
    }
}
