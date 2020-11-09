/*
 * Copyright 2012 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// GetDocumentClient
    /// </summary>
    public class GetDocumentClient : IGetDocumentClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        private readonly DocumentRepositoryClient client;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return client.soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetDocumentClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            client = new DocumentRepositoryClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetDocumentClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            client = new DocumentRepositoryClient(endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetDocumentClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            client = new DocumentRepositoryClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetDocumentClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            client = new DocumentRepositoryClient(endpointUri, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Retrieve a document. 
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="requests">Request.</param>
        /// <returns>Response.</returns>
        public RetrieveDocumentSetResponseType GetDocument(CommonPcehrHeader pcehrHeader, RetrieveDocumentSetRequestTypeDocumentRequest[] requests)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);
            Validation.ValidateArgumentRequired("requests", requests);

            return client.GetDocument(pcehrHeader.GetHeader<PCEHRHeader>(), requests);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            client.Close();
        }
    }
}
