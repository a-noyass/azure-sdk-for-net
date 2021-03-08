// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// Collection of <see cref="CustomClassification"/> objects in a document,
    /// and warnings encountered while processing the document.
    /// </summary>
    [DebuggerTypeProxy(typeof(CustomClassificationCollectionDebugView))]
    public class CustomClassificationCollection : ReadOnlyCollection<CustomClassification>
    {
        internal CustomClassificationCollection(IList<CustomClassification> classifications, IList<TextAnalyticsWarning> warnings)
            : base(classifications)
        {
            Warnings = new ReadOnlyCollection<TextAnalyticsWarning>(warnings);
        }

        /// <summary>
        /// Warnings encountered while processing the document.
        /// </summary>
        public IReadOnlyCollection<TextAnalyticsWarning> Warnings { get; }

        /// <summary>
        /// Debugger Proxy class for <see cref="CustomClassificationCollection"/>.
        /// </summary>
        internal class CustomClassificationCollectionDebugView
        {
            private CustomClassificationCollection BaseCollection { get; }

            public CustomClassificationCollectionDebugView(CustomClassificationCollection collection)
            {
                BaseCollection = collection;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public List<CustomClassification> Items
            {
                get
                {
                    return BaseCollection.ToList();
                }
            }

            public IReadOnlyCollection<TextAnalyticsWarning> Warnings
            {
                get
                {
                    return BaseCollection.Warnings;
                }
            }
        }
    }
}
