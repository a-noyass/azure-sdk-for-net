// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.Luis.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.Luis
{
    /// <summary>
    /// The client to use for interacting with luis.
    /// </summary>
    public class LuisPredictionClient
    {
        internal readonly PredictionRestClient _serviceRestClient;
        internal readonly ClientDiagnostics _clientDiagnostics;
        internal readonly LuisPredictionClientOptions _options;

        private const string AuthorizationHeader = "Ocp-Apim-Subscription-Key";
        private readonly string DefaultCognitiveScope = "https://cognitiveservices.azure.com/.default";

        /// <summary>
        /// Protected constructor to allow mocking.
        /// </summary>
        protected LuisPredictionClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LuisPredictionClient"/>
        /// </summary>
        /// <param name="endpoint">A <see cref="Uri"/> to the service the client
        /// sends requests to.  Endpoint can be found in the Azure portal.</param>
        /// <param name="credential">A <see cref="TokenCredential"/> used to
        /// authenticate requests to the service, such as DefaultAzureCredential.</param>
        /// <param name="options"><see cref="LuisPredictionClientOptions"/> that allow
        /// callers to configure how requests are sent to the service.</param>
        public LuisPredictionClient(Uri endpoint, TokenCredential credential, LuisPredictionClientOptions options)
        {
            Argument.AssertNotNull(endpoint, nameof(endpoint));
            Argument.AssertNotNull(credential, nameof(credential));
            Argument.AssertNotNull(options, nameof(options));

            _options = options;
            _clientDiagnostics = new ClientDiagnostics(options);

            var pipeline = HttpPipelineBuilder.Build(options, new BearerTokenAuthenticationPolicy(credential, DefaultCognitiveScope));
            _serviceRestClient = new PredictionRestClient(_clientDiagnostics, pipeline, endpoint.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LuisPredictionClient"/>
        /// </summary>
        /// <param name="endpoint">A <see cref="Uri"/> to the service the client
        /// sends requests to.  Endpoint can be found in the Azure portal.</param>
        /// <param name="credential">A <see cref="TokenCredential"/> used to
        /// authenticate requests to the service, such as DefaultAzureCredential.</param>
        public LuisPredictionClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new LuisPredictionClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LuisPredictionClient"/>
        /// </summary>
        /// <param name="endpoint">A <see cref="Uri"/> to the service the client
        /// sends requests to.  Endpoint can be found in the Azure portal.</param>
        /// <param name="credential">The API key used to access
        /// the service. This will allow you to update the API key
        /// without creating a new client.</param>
        /// <param name="options"><see cref="LuisPredictionClientOptions"/> that allow
        /// callers to configure how requests are sent to the service.</param>
        public LuisPredictionClient(Uri endpoint, AzureKeyCredential credential, LuisPredictionClientOptions options)
        {
            Argument.AssertNotNull(endpoint, nameof(endpoint));
            Argument.AssertNotNull(credential, nameof(credential));
            Argument.AssertNotNull(options, nameof(options));

            _options = options;
            _clientDiagnostics = new ClientDiagnostics(options);

            var pipeline = HttpPipelineBuilder.Build(options, new AzureKeyCredentialPolicy(credential, AuthorizationHeader));
            _serviceRestClient = new PredictionRestClient(_clientDiagnostics, pipeline, endpoint.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LuisPredictionClient"/>
        /// </summary>
        /// <param name="endpoint">A <see cref="Uri"/> to the service the client
        /// sends requests to.  Endpoint can be found in the Azure portal.</param>
        /// <param name="credential">The API key used to access
        /// the service. This will allow you to update the API key
        /// without creating a new client.</param>
        public LuisPredictionClient(Uri endpoint, AzureKeyCredential credential)
            : this(endpoint, credential, new LuisPredictionClientOptions())
        {
        }
    }
}
