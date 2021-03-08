// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Azure.AI.TextAnalytics;

namespace Azure.AI.TextAnalytics.Models
{
    /// <summary> The DocumentCustomMultiClassification. </summary>
    internal partial class DocumentCustomMultiClassification
    {
        /// <summary> Initializes a new instance of DocumentCustomMultiClassification. </summary>
        /// <param name="id"> Unique, non-empty document identifier. </param>
        /// <param name="classifications"> Recognized custom classifications for the document. </param>
        /// <param name="warnings"> Warnings encountered while processing document. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="id"/>, <paramref name="classifications"/>, or <paramref name="warnings"/> is null. </exception>
        internal DocumentCustomMultiClassification(string id, IEnumerable<Classification> classifications, IEnumerable<TextAnalyticsWarningInternal> warnings)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            if (classifications == null)
            {
                throw new ArgumentNullException(nameof(classifications));
            }
            if (warnings == null)
            {
                throw new ArgumentNullException(nameof(warnings));
            }

            Id = id;
            Classifications = classifications.ToList();
            Warnings = warnings.ToList();
        }

        /// <summary> Initializes a new instance of DocumentCustomMultiClassification. </summary>
        /// <param name="id"> Unique, non-empty document identifier. </param>
        /// <param name="classifications"> Recognized custom classifications for the document. </param>
        /// <param name="warnings"> Warnings encountered while processing document. </param>
        /// <param name="statistics"> if showStats=true was specified in the request this field will contain information about the document payload. </param>
        internal DocumentCustomMultiClassification(string id, IReadOnlyList<Classification> classifications, IReadOnlyList<TextAnalyticsWarningInternal> warnings, TextDocumentStatistics? statistics)
        {
            Id = id;
            Classifications = classifications;
            Warnings = warnings;
            Statistics = statistics;
        }

        /// <summary> Unique, non-empty document identifier. </summary>
        public string Id { get; }
        /// <summary> Recognized custom classifications for the document. </summary>
        public IReadOnlyList<Classification> Classifications { get; }
        /// <summary> Warnings encountered while processing document. </summary>
        public IReadOnlyList<TextAnalyticsWarningInternal> Warnings { get; }
        /// <summary> if showStats=true was specified in the request this field will contain information about the document payload. </summary>
        public TextDocumentStatistics? Statistics { get; }
    }
}
