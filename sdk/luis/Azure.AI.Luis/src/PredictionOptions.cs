// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.AI.Luis.Models
{
    /// <summary>
    /// Custom options for prediction.
    /// </summary>
    public partial class PredictionOptions
    {
        /// <summary> Initializes a new instance of PredictionRequestOptions. </summary>
        public PredictionOptions()
        {
        }

        /// <summary> The reference DateTime used for predicting datetime entities. </summary>
        public DateTimeOffset? DatetimeReference { get; set; }
        /// <summary> Whether to make the external entities resolution override the predictions if an overlap occurs. </summary>
        public bool? PreferExternalEntities { get; set; }
        /// <summary>
        /// Indicates whether to get extra metadata for the entities predictions or not.
        /// </summary>
        public bool? Verbose { get; set; }
        /// <summary>
        /// Indicates whether to return all the intents in the response or just the top intent.
        /// </summary>
        public bool? ShowAllIntents { get; set; }
        /// <summary>
        /// Indicates whether to log the endpoint query or not.
        /// </summary>
        public bool? Log { get; set; }
        /// <summary> The externally predicted entities for this request. </summary>
        public IList<ExternalEntity> ExternalEntities { get; }
        /// <summary> The dynamically created list entities for this request. </summary>
        public IList<DynamicList> DynamicLists { get; }
    }
}
