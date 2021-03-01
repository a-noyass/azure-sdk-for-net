// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.DocumentTranslation.Models;
using Azure.Core.TestFramework;
using NUnit.Framework;

namespace Azure.AI.DocumentTranslation.Tests.Samples
{
    [LiveOnly]
    public partial class DocumentTranslationSamples : SamplesBase<DocumentTranslationTestEnvironment>
    {
        [Test]
        public void AllOperations()
        {
            string endpoint = TestEnvironment.Endpoint;
            string apiKey = TestEnvironment.ApiKey;

            var client = new DocumentTranslationClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            Pageable<OperationStatusDetail> operations = client.GetStatusesOfOperations();

            int operationsCount = 0;
            int totalDocs = 0;
            int docsCancelled = 0;
            int docsSucceeded = 0;
            int maxDocs = 0;
            string largestOperationId = "";

            foreach (OperationStatusDetail operation in operations)
            {
                operationsCount++;
                totalDocs += operation.TotalDocuments;
                docsCancelled += operation.DocumentsCancelled;
                docsSucceeded += operation.DocumentsSucceeded;
                if (totalDocs > maxDocs)
                {
                    maxDocs = totalDocs;
                    largestOperationId = operation.Id;
                }
            }

            Console.WriteLine($"# of operations: {operationsCount}\nTotal Documents: {totalDocs}\n"
                              + $"DocumentsSucceeded: {docsSucceeded}\n"
                              + $"Cancelled Documents: {docsCancelled}");

            Console.WriteLine($"Largest operation is {largestOperationId} and has the documents:");
            Pageable<DocumentStatusDetail> docs = client.GetStatusesOfDocuments(largestOperationId);

            foreach (DocumentStatusDetail docStatus in docs)
            {
                Console.WriteLine($"Document {docStatus.Url} has status {docStatus.Status}");
            }
        }
    }
}
