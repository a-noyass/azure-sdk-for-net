// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    [CodeGenModel("TasksStateTasksCustomMultiClassificationTasksItem")]
    internal partial class CustomMultiClassificationTasksItem
    {
        /// <summary> Initializes a new instance of <see cref="CustomMultiClassificationTasksItem"/>. </summary>
        internal CustomMultiClassificationTasksItem(CustomMultiClassificationTasksItem task, IDictionary<string, int> idToIndexMap) : base(task.LastUpdateDateTime, task.Name, task.Status)
        {
            Results = Transforms.ConvertToCustomMultiClassificationResultCollection(task.ResultsInternal, idToIndexMap);
        }

        /// <summary>
        /// CustomMultiClassificationResultCollection Result
        /// </summary>
        public CustomMultiClassificationResultCollection Results { get; }

        /// <summary>
        /// Results for CustomMultiClassificationTasksItem
        /// </summary>
        [CodeGenMember("Results")]
        internal InternalCustomMultiClassificationResult ResultsInternal { get; }
    }
}
