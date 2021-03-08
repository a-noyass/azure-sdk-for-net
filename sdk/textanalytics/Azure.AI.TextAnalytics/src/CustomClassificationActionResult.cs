// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// CustomClassificationActionResult
    /// </summary>
    public class CustomClassificationActionResult : TextAnalyticsActionDetails
    {
        internal CustomClassificationActionResult(CustomClassificationResultCollection result, DateTimeOffset completedOn, TextAnalyticsErrorInternal error) : base(completedOn, error)
        {
            Result = result;
        }

        /// <summary>
        /// Results
        /// </summary>
        public CustomClassificationResultCollection Result { get; }
    }
}
