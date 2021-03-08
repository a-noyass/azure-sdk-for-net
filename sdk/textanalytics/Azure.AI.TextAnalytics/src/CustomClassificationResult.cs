// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// The result of the Custom Classification operation on a
    /// document, containing the classification predicted for that document.
    /// </summary>
    public class CustomClassificationResult : TextAnalyticsResult
    {
        private readonly CustomClassification _classification;

        internal CustomClassificationResult(string id, TextDocumentStatistics statistics, CustomClassification classification)
            : base(id, statistics)
        {
            _classification = classification;
        }

        internal CustomClassificationResult(string id, TextAnalyticsError error) : base(id, error) { }

        /// <summary>
        /// Gets the classification predicted for the document.
        /// </summary>
        public CustomClassification Classification
        {
            get
            {
                if (HasError)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new InvalidOperationException($"Cannot access result for document {Id}, due to error {Error.ErrorCode}: {Error.Message}");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return _classification;
            }
        }
    }
}
