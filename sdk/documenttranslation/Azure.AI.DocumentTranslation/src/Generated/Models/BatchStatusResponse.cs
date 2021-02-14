// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.AI.DocumentTranslation.Models
{
    /// <summary> Document Status Response. </summary>
    public partial class BatchStatusResponse
    {
        /// <summary> Initializes a new instance of BatchStatusResponse. </summary>
        internal BatchStatusResponse()
        {
            Value = new ChangeTrackingList<BatchStatusDetail>();
        }

        /// <summary> Initializes a new instance of BatchStatusResponse. </summary>
        /// <param name="value"> The summary status of individual operation. </param>
        /// <param name="nextLink"> Url for the next page.  Null if no more pages available. </param>
        internal BatchStatusResponse(IReadOnlyList<BatchStatusDetail> value, string nextLink)
        {
            Value = value;
            NextLink = nextLink;
        }

        /// <summary> The summary status of individual operation. </summary>
        public IReadOnlyList<BatchStatusDetail> Value { get; }
        /// <summary> Url for the next page.  Null if no more pages available. </summary>
        public string NextLink { get; }
    }
}
