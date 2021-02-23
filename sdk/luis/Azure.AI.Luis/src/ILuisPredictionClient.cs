// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.Luis.Models;

namespace Azure.AI.Luis
{
    internal interface ILuisPredictionClient
    {
        public Response<PredictionResponse> GetPrediction(string query);

        public Response<PredictionResponse> GetPrediction(string query, PredictionOptions options);
    }
}
