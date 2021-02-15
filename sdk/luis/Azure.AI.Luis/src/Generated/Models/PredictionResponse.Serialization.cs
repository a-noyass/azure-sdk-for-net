// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.Luis.Models
{
    public partial class PredictionResponse
    {
        internal static PredictionResponse DeserializePredictionResponse(JsonElement element)
        {
            string query = default;
            Prediction prediction = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("query"))
                {
                    query = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("prediction"))
                {
                    prediction = Prediction.DeserializePrediction(property.Value);
                    continue;
                }
            }
            return new PredictionResponse(query, prediction);
        }
    }
}
