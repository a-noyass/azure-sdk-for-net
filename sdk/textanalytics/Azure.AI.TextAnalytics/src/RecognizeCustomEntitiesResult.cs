// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// The result of the recognize custom entities operation on a
    /// document, containing a collection of the <see cref="CustomEntity"/>
    /// objects identified in that document.
    /// </summary>
    public class RecognizeCustomEntitiesResult : TextAnalyticsResult
    {
        private readonly CustomEntityCollection _customEntities;

        internal RecognizeCustomEntitiesResult(string id, TextDocumentStatistics statistics, CustomEntityCollection customEntities)
            : base(id, statistics)
        {
            _customEntities = customEntities;
        }

        internal RecognizeCustomEntitiesResult(string id, TextAnalyticsError error) : base(id, error) { }

        /// <summary>
        /// Gets the collection of custom entities identified in the document.
        /// </summary>
        public CustomEntityCollection Entities
        {
            get
            {
                if (HasError)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new InvalidOperationException($"Cannot access result for document {Id}, due to error {Error.ErrorCode}: {Error.Message}");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return _customEntities;
            }
        }
    }
}
