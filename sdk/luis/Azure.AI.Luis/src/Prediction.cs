// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Azure.AI.Luis
{
    internal partial class Prediction
    {
#pragma warning disable AZC0014 // Avoid using banned types in public API
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CA1801 // Review unused parameters
        /// <summary>
        /// returns json array containing entities
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonDocument GetEntitiesByType(EntityTypeEnum type)
        {
            return null;
        }

        /// <summary>
        /// Maps occurences of the specified entity to the provided type and returns them as a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<T> DeserializeEntity<T>(string entityName)
        {
            return null;
        }
#pragma warning restore CA1801 // Review unused parameters
#pragma warning restore CA1822 // Mark members as static
#pragma warning restore AZC0014 // Avoid using banned types in public API
    }
}
