// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// Collection of <see cref="RecognizeCustomEntitiesResult"/> objects corresponding
    /// to a batch of documents, and information about the batch operation.
    /// </summary>
    [DebuggerTypeProxy(typeof(RecognizeCustomEntitiesResultCollectionDebugView))]
    public class RecognizeCustomEntitiesResultCollection : ReadOnlyCollection<RecognizeCustomEntitiesResult>
    {
        internal RecognizeCustomEntitiesResultCollection(IList<RecognizeCustomEntitiesResult> list, TextDocumentBatchStatistics statistics) : base(list)
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
        /// Debugger Proxy class for <see cref="RecognizeCustomEntitiesResultCollection"/>.
        /// </summary>
        internal class RecognizeCustomEntitiesResultCollectionDebugView
        {
            private RecognizeCustomEntitiesResultCollection BaseCollection { get; }

            public RecognizeCustomEntitiesResultCollectionDebugView(RecognizeCustomEntitiesResultCollection collection)
            {
                BaseCollection = collection;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public List<RecognizeCustomEntitiesResult> Items
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
