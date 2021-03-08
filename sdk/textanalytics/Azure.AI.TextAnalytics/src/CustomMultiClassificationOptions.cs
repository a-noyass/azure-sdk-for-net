﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// Options that allow callers to specify details about how the operation
    /// is run. For example set PublishId, whether to include statistics.
    /// </summary>
    public class CustomMultiClassificationOptions : TextAnalyticsRequestOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMultiClassificationOptions"/>
        /// class which allows callers to specify details about how the operation
        /// is run. For example set PublishId, whether to include statistics.
        /// </summary>
        public CustomMultiClassificationOptions()
        {
        }

        /// <summary>
        /// PublishId
        /// </summary>
        public string PublishId { get; set; }
    }
}
