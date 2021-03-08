// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.AI.TextAnalytics.Models;
using Azure.Core;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// JobManifestTasks.
    /// </summary>
    [CodeGenModel("JobManifestTasks")]
    internal partial class JobManifestTasks
    {
        /// <summary>
        /// EntityRecognitionTasks
        /// </summary>
        internal IList<EntitiesTask> EntityRecognitionTasks { get; set; }

        /// <summary>
        /// EntityRecognitionPiiTasks
        /// </summary>
        internal IList<PiiTask> EntityRecognitionPiiTasks { get; set; }

        /// <summary>
        /// KeyPhraseExtractionTasks
        /// </summary>
        internal IList<KeyPhrasesTask> KeyPhraseExtractionTasks { get; set; }

        /// <summary>
        /// EntityLinkingTasks
        /// </summary>
        internal IList<EntityLinkingTask> EntityLinkingTasks { get; set; }

        /// <summary>
        /// CustomEntityRecognitionTasks
        /// </summary>
        internal IList<CustomEntitiesTask> CustomEntityRecognitionTasks { get; set; }

        /// <summary>
        /// CustomClassificationTasks
        /// </summary>
        internal IList<CustomClassificationTask> CustomClassificationTasks { get; set; }

        /// <summary>
        /// CustomMultiClassificationTasks
        /// </summary>
        internal IList<CustomMultiClassificationTask> CustomMultiClassificationTasks { get; set; }
    }
}
