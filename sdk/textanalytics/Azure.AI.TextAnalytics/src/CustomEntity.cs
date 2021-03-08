// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    /// <summary>
    /// A word or phrase identified as a custom entity.
    /// </summary>
    public readonly struct CustomEntity
    {
        internal CustomEntity(Entity entity)
        {
            Category = entity.Category;
            Text = entity.Text;
            ConfidenceScore = entity.ConfidenceScore;
            Offset = entity.Offset;
            Length = entity.Length;
        }

        /// <summary>
        /// Gets the entity text as it appears in the input document.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the entity category inferred by the Text Analytics custom model.
        /// </summary>
        public EntityCategory Category { get; }

        /// <summary>
        /// Gets a score between 0 and 1, indicating the confidence that the
        /// text substring matches this inferred entity.
        /// </summary>
        public double ConfidenceScore { get; }

        /// <summary>
        /// Gets the starting position for the matching text in the input document.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the length of the matching text in the input document.
        /// </summary>
        public int Length { get; }
    }
}
