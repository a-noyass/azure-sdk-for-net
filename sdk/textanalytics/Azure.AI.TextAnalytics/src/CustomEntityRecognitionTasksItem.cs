// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    [CodeGenModel("TasksStateTasksCustomEntityRecognitionTasksItem")]
    internal partial class CustomEntityRecognitionTasksItem
    {
        /// <summary> Initializes a new instance of <see cref="CustomEntityRecognitionTasksItem"/>. </summary>
        internal CustomEntityRecognitionTasksItem(CustomEntityRecognitionTasksItem task, IDictionary<string, int> idToIndexMap) : base(task.LastUpdateDateTime, task.Name, task.Status)
        {
            Results = Transforms.ConvertToRecognizeCustomEntitiesResultCollection(task.ResultsInternal, idToIndexMap);
        }

        /// <summary>
        /// RecognizeCustomEntitiesResultCollection Result
        /// </summary>
        public RecognizeCustomEntitiesResultCollection Results { get; }

        /// <summary>
        /// Results for CustomEntityRecognitionTasksItem
        /// </summary>
        [CodeGenMember("Results")]
        internal CustomEntitiesResult ResultsInternal { get; }
    }
}
