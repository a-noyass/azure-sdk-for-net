﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.TestFramework;

namespace Azure.AI.DocumentTranslation.Tests
{
    public class DocumentTranslationTestEnvironment : TestEnvironment
    {
        public string Endpoint => GetRecordedVariable("DOCUMENT_TRANSLATION_ENDPOINT");
        public string ApiKey => GetRecordedVariable("DOCUMENT_TRANSLATION_API_KEY", options => options.IsSecret());
        public string SourceUrl => GetRecordedVariable("DOCUMENT_TRANSLATION_SOURCE_URL", options => options.IsSecret());
        public string SourceContainerName => GetRecordedVariable("DOCUMENT_TRANSLATION_SOURCE_CONTAINER", options => options.IsSecret());
        public string TargetUrl => GetRecordedVariable("DOCUMENT_TRANSLATION_TARGET_URL", options => options.IsSecret());
        public string GlossaryUrl => GetRecordedVariable("DOCUMENT_TRANSLATION_GLOSSARY_URL", options => options.IsSecret());
        public string StorageConnectionString => GetRecordedVariable("STORAGE_ACCOUNT_CONNECTION_STRING", options => options.IsSecret());
    }
}
