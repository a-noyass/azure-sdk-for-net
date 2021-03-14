// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using NUnit.Framework;

namespace Azure.AI.DocumentTranslation.Tests
{
    public class TranslationOperationTests : DocumentTranslationClientLiveTestBase
    {
        public TranslationOperationTests(bool isAsync) : base(isAsync) { }

        private string GetContainerName()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            //long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            return testName.ToLower(); // + milliseconds.ToString();
        }
        private async Task<Uri> CreateTestContainerAsync()
        {
            string containerName = GetContainerName();
            var containerClient = new BlobContainerClient(TestEnvironment.StorageConnectionString, containerName.ToLower());
            await containerClient.CreateIfNotExistsAsync().ConfigureAwait(false);
            var expiresOn = DateTimeOffset.Now.AddHours(1);
            return containerClient.GenerateSasUri(BlobContainerSasPermissions.Write, expiresOn);
        }

        private async Task DeleteContainerAsync(string containerName)
        {
            var containerClient = new BlobContainerClient(TestEnvironment.StorageConnectionString, containerName.ToLower());
            await containerClient.DeleteIfExistsAsync().ConfigureAwait(false);
        }

        private Uri GetSasUrl(string containerName)
        {
            var containerClient = new BlobContainerClient(TestEnvironment.StorageConnectionString, containerName);
            var expiresOn = DateTimeOffset.Now.AddHours(1);
            return containerClient.GenerateSasUri(BlobContainerSasPermissions.List | BlobContainerSasPermissions.Read, expiresOn);
        }

        [Test]
        public async Task TranslationOperationTest()
        {
            Uri target = await CreateTestContainerAsync();
            Uri source = GetSasUrl(TestEnvironment.SourceContainerName);
            Uri endpoint = new Uri(TestEnvironment.Endpoint);
            AzureKeyCredential credential = new AzureKeyCredential(TestEnvironment.ApiKey);
            var client = new DocumentTranslationClient(endpoint, credential);

            var operation = await client.StartTranslationAsync(source, target, "ar");

            await operation.WaitForCompletionAsync();

            Assert.AreEqual(2, operation.DocumentsTotal);
            Assert.AreEqual(2, operation.DocumentsSucceeded);
            Assert.AreEqual(0, operation.DocumentsFailed);

            await DeleteContainerAsync(TestContext.CurrentContext.Test.Name);
        }
    }
}
