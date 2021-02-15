// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.AI.Luis.Models
{
    public partial class Prediction
    {
        internal static Prediction DeserializePrediction(JsonElement element)
        {
            Optional<string> alteredQuery = default;
            string topIntent = default;
            IReadOnlyDictionary<string, Intent> intents = default;
            IReadOnlyDictionary<string, object> entities = default;
            Optional<Sentiment> sentiment = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("alteredQuery"))
                {
                    alteredQuery = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("topIntent"))
                {
                    topIntent = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("intents"))
                {
                    Dictionary<string, Intent> dictionary = new Dictionary<string, Intent>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, Intent.DeserializeIntent(property0.Value));
                    }
                    intents = dictionary;
                    continue;
                }
                if (property.NameEquals("entities"))
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetObject());
                    }
                    entities = dictionary;
                    continue;
                }
                if (property.NameEquals("sentiment"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    sentiment = Sentiment.DeserializeSentiment(property.Value);
                    continue;
                }
            }
            return new Prediction(alteredQuery.Value, topIntent, intents, entities, sentiment.Value);
        }
    }
}
