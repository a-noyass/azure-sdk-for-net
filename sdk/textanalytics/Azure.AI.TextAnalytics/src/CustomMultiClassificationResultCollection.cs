// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// Collection of <see cref="CustomMultiClassificationResult"/> objects corresponding
    /// to a batch of documents, and information about the batch operation.
    /// </summary>
    [DebuggerTypeProxy(typeof(CustomMultiClassificationResultCollectionDebugView))]
    public class CustomMultiClassificationResultCollection : ReadOnlyCollection<CustomMultiClassificationResult>
    {
        internal CustomMultiClassificationResultCollection(IList<CustomMultiClassificationResult> list, TextDocumentBatchStatistics statistics) : base(list)
        {
            Statistics = statistics;
        }

        /// <summary>
        /// Gets statistics about the documents and how it was processed
        /// by the service.  This property will have a value when IncludeStatistics
        /// is set to true in the client call.
        /// </summary>
        public TextDocumentBatchStatistics Statistics { get; }

        /// <summary>
        /// Debugger Proxy class for <see cref="CustomMultiClassificationResultCollection"/>.
        /// </summary>
        internal class CustomMultiClassificationResultCollectionDebugView
        {
            private CustomMultiClassificationResultCollection BaseCollection { get; }

            public CustomMultiClassificationResultCollectionDebugView(CustomMultiClassificationResultCollection collection)
            {
                BaseCollection = collection;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public List<CustomMultiClassificationResult> Items
            {
                get
                {
                    return BaseCollection.ToList();
                }
            }

            public TextDocumentBatchStatistics Statistics
            {
                get
                {
                    return BaseCollection.Statistics;
                }
            }
        }
    }
}
