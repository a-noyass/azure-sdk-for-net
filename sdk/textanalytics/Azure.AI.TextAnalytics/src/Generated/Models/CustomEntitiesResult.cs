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
    /// <summary> The CustomEntitiesResult. </summary>
    internal partial class CustomEntitiesResult
    {
        /// <summary> Initializes a new instance of CustomEntitiesResult. </summary>
        /// <param name="documents"> Response by document. </param>
        /// <param name="errors"> Errors by document id. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="documents"/> or <paramref name="errors"/> is null. </exception>
        internal CustomEntitiesResult(IEnumerable<DocumentCustomEntities> documents, IEnumerable<DocumentError> errors)
        {
            if (documents == null)
            {
                throw new ArgumentNullException(nameof(documents));
            }
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            Documents = documents.ToList();
            Errors = errors.ToList();
        }

        /// <summary> Initializes a new instance of CustomEntitiesResult. </summary>
        /// <param name="documents"> Response by document. </param>
        /// <param name="errors"> Errors by document id. </param>
        /// <param name="statistics"> if showStats=true was specified in the request this field will contain information about the request payload. </param>
        internal CustomEntitiesResult(IReadOnlyList<DocumentCustomEntities> documents, IReadOnlyList<DocumentError> errors, TextDocumentBatchStatistics statistics)
        {
            Documents = documents;
            Errors = errors;
            Statistics = statistics;
        }

        /// <summary> Response by document. </summary>
        public IReadOnlyList<DocumentCustomEntities> Documents { get; }
        /// <summary> Errors by document id. </summary>
        public IReadOnlyList<DocumentError> Errors { get; }
        /// <summary> if showStats=true was specified in the request this field will contain information about the request payload. </summary>
        public TextDocumentBatchStatistics Statistics { get; }
    }
}
