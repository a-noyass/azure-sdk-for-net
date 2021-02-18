// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.Luis.Models;

namespace Azure.AI.Luis
{
    internal interface ILuisPredictionClient
    {
        public Response<PredictionResponse> GetSlotPrediction(Guid appId, string slotName, string query);

        public Response<PredictionResponse> GetSlotPrediction(Guid appId, string slotName, string query, PredictionOptions options);

        public Response<PredictionResponse> GetVersionPrediction(Guid appId, string version, string query);

        public Response<PredictionResponse> GetVersionPrediction(Guid appId, string version, string query, PredictionOptions options);
    }
}
