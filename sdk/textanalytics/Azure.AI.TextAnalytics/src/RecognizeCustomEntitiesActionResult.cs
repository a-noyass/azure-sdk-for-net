// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// RecognizeCustomEntitiesActionResult
    /// </summary>
    public class RecognizeCustomEntitiesActionResult : TextAnalyticsActionDetails
    {
        internal RecognizeCustomEntitiesActionResult(RecognizeCustomEntitiesResultCollection result, DateTimeOffset completedOn, TextAnalyticsErrorInternal error) : base(completedOn, error)
        {
            Result = result;
        }

        /// <summary>
        /// Results
        /// </summary>
        public RecognizeCustomEntitiesResultCollection Result { get; }
    }
}
