// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Azure.Core;

namespace Azure.AI.TextAnalytics.Models
{
    [CodeGenModel("TasksStateTasksCustomClassificationTasksItem")]
    internal partial class CustomClassificationTasksItem
    {
        /// <summary> Initializes a new instance of <see cref="CustomClassificationTasksItem"/>. </summary>
        internal CustomClassificationTasksItem(CustomClassificationTasksItem task, IDictionary<string, int> idToIndexMap) : base(task.LastUpdateDateTime, task.Name, task.Status)
        {
            Results = Transforms.ConvertToCustomClassificationResultCollection(task.ResultsInternal, idToIndexMap);
        }

        /// <summary>
        /// CustomClassificationResultCollection Result
        /// </summary>
        public CustomClassificationResultCollection Results { get; }

        /// <summary>
        /// Results for CustomClassificationTasksItem
        /// </summary>
        [CodeGenMember("Results")]
        internal InternalCustomClassificationResult ResultsInternal { get; }
    }
}
