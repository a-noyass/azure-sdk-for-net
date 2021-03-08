// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// The result of the Custom Multi Classification operation on a
    /// document, containing the list of classifications predicted for that document.
    /// </summary>
    public class CustomMultiClassificationResult : TextAnalyticsResult
    {
        private readonly IList<CustomClassification> _classifications;

        internal CustomMultiClassificationResult(string id, TextDocumentStatistics statistics, IList<CustomClassification> classifications)
            : base(id, statistics)
        {
            _classifications = classifications;
        }

        internal CustomMultiClassificationResult(string id, TextAnalyticsError error) : base(id, error) { }

        /// <summary>
        /// Gets the collection of classifications predicted for the document.
        /// </summary>
        public IList<CustomClassification> Classifications
        {
            get
            {
                if (HasError)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new InvalidOperationException($"Cannot access result for document {Id}, due to error {Error.ErrorCode}: {Error.Message}");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return _classifications;
            }
        }
    }
}
