// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// CustomMultiClassificationActionResult
    /// </summary>
    public class CustomMultiClassificationActionResult : TextAnalyticsActionDetails
    {
        internal CustomMultiClassificationActionResult(CustomMultiClassificationResultCollection result, DateTimeOffset completedOn, TextAnalyticsErrorInternal error) : base(completedOn, error)
        {
            Result = result;
        }

        /// <summary>
        /// Results
        /// </summary>
        public CustomMultiClassificationResultCollection Result { get; }
    }
}
