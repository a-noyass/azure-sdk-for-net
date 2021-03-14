﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.AI.DocumentTranslation.Models;
using Azure.Core.TestFramework;
using NUnit.Framework;

namespace Azure.AI.DocumentTranslation.Tests.Samples
{
    [LiveOnly]
    public partial class DocumentTranslationSamples : SamplesBase<DocumentTranslationTestEnvironment>
    {
        [Test]
        public async Task DocumentStatusAsync()
        {
            string endpoint = TestEnvironment.Endpoint;
            string apiKey = TestEnvironment.ApiKey;
            Uri sourceUrl = new Uri(TestEnvironment.SourceUrl);
            Uri targetUrl = new Uri(TestEnvironment.TargetUrl);

            var client = new DocumentTranslationClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            DocumentTranslationOperation operation = await client.StartTranslationAsync(sourceUrl, targetUrl, "it");

            var documentscompleted = new HashSet<string>();

            while (!operation.HasCompleted)
            {
                await operation.UpdateStatusAsync();

                AsyncPageable<DocumentStatusDetail> documentsStatus = operation.GetAllDocumentsStatusAsync();
                await foreach (DocumentStatusDetail docStatus in documentsStatus)
                {
                    if (documentscompleted.Contains(docStatus.DocumentId))
                        continue;
                    if (docStatus.Status == TranslationStatus.Succeeded || docStatus.Status == TranslationStatus.Failed)
                    {
                        documentscompleted.Add(docStatus.DocumentId);
                        Console.WriteLine($"Document {docStatus.LocationUri} completed with status ${docStatus.Status}");
                    }
                }
            }
        }
    }
}
