// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// The results of an analyze batch actions operation.
    /// </summary>
    public class AnalyzeBatchActionsResult
    {
        internal AnalyzeBatchActionsResult(
            IReadOnlyCollection<ExtractKeyPhrasesActionResult> extractKeyPhrasesActionResult,
            IReadOnlyCollection<RecognizeEntitiesActionResult> recognizeEntitiesActionResults,
            IReadOnlyCollection<RecognizePiiEntitiesActionResult> recognizePiiEntitiesActionResults,
            IReadOnlyCollection<RecognizeLinkedEntitiesActionResult> recognizeLinkedEntitiesActionsResults,
            IReadOnlyCollection<RecognizeCustomEntitiesActionResult> recognizeCustomEntitiesActionResults,
            IReadOnlyCollection<CustomClassificationActionResult> customClassificationActionResult,
            IReadOnlyCollection<CustomMultiClassificationActionResult> customMultiClassificationActionResult,
            TextDocumentBatchStatistics statistics)
        {
            ExtractKeyPhrasesActionsResults = extractKeyPhrasesActionResult;
            RecognizeEntitiesActionsResults = recognizeEntitiesActionResults;
            RecognizePiiEntitiesActionsResults = recognizePiiEntitiesActionResults;
            RecognizeLinkedEntitiesActionsResults = recognizeLinkedEntitiesActionsResults;
            RecognizeCustomEntitiesActionsResults = recognizeCustomEntitiesActionResults;
            CustomClassificationActionsResults = customClassificationActionResult;
            CustomMultiClassificationActionsResults = customMultiClassificationActionResult;
            Statistics = statistics;
        }

        internal AnalyzeBatchActionsResult(AnalyzeJobState jobState, IDictionary<string, int> map)
        {
            AnalyzeBatchActionsResult actionResults = Transforms.ConvertToAnalyzeBatchActionsResult(jobState, map);
            ExtractKeyPhrasesActionsResults = actionResults.ExtractKeyPhrasesActionsResults;
            RecognizeEntitiesActionsResults = actionResults.RecognizeEntitiesActionsResults;
            RecognizePiiEntitiesActionsResults = actionResults.RecognizePiiEntitiesActionsResults;
            RecognizeLinkedEntitiesActionsResults = actionResults.RecognizeLinkedEntitiesActionsResults;
            RecognizeCustomEntitiesActionsResults = actionResults.RecognizeCustomEntitiesActionsResults;
            CustomClassificationActionsResults = actionResults.CustomClassificationActionsResults;
            CustomMultiClassificationActionsResults = actionResults.CustomMultiClassificationActionsResults;
            Statistics = actionResults.Statistics;
        }

        /// <summary>
        /// Determines the collection of ExtractKeyPhrasesActionResult.
        /// </summary>
        public IReadOnlyCollection<ExtractKeyPhrasesActionResult> ExtractKeyPhrasesActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizeEntitiesActionResult.
        /// </summary>
        public IReadOnlyCollection<RecognizeEntitiesActionResult> RecognizeEntitiesActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizePiiEntitiesActionResult.
        /// </summary>
        public IReadOnlyCollection<RecognizePiiEntitiesActionResult> RecognizePiiEntitiesActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizeLinkedEntitiesActionsResult.
        /// </summary>
        public IReadOnlyCollection<RecognizeLinkedEntitiesActionResult> RecognizeLinkedEntitiesActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizeCustomEntitiesActionResult.
        /// </summary>
        public IReadOnlyCollection<RecognizeCustomEntitiesActionResult> RecognizeCustomEntitiesActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizeCustomEntitiesActionResult.
        /// </summary>
        public IReadOnlyCollection<CustomClassificationActionResult> CustomClassificationActionsResults { get; }

        /// <summary>
        /// Determines the collection of RecognizeCustomEntitiesActionResult.
        /// </summary>
        public IReadOnlyCollection<CustomMultiClassificationActionResult> CustomMultiClassificationActionsResults { get; }

        /// <summary>
        /// Gets statistics about the operation executed and how it was processed
        /// by the service.
        /// </summary>
        public TextDocumentBatchStatistics Statistics { get; }
    }
}
