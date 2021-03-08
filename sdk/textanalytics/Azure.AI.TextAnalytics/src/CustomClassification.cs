// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary> The Classification. </summary>
    public class CustomClassification
    {
        /// <summary> Initializes a new instance of CustomClassification. </summary>
        /// <param name="classification"> Classification. </param>
        internal CustomClassification(Classification classification)
        {
            Class = classification.Class;
            ConfidenceScore = classification.ConfidenceScore;
        }

        /// <summary> Classification type. </summary>
        public string Class { get; }
        /// <summary> Confidence score between 0 and 1 of the extracted entity. </summary>
        public double ConfidenceScore { get; }
    }
}
