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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using System.ServiceModel;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// DocumentRepositoryClient.
    /// </summary>
    internal class DocumentRepositoryClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        internal DocumentRepository_PortTypeClient repositoryClient;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        internal SoapMessages soapMessages;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        internal DocumentRepositoryClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            Validation.ValidateArgumentRequired("endpointUri", endpointUri);

            InitialiseClient(endpointUri.ToString(), null, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        internal DocumentRepositoryClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            Validation.ValidateArgumentRequired("endpointConfigurationName", endpointConfigurationName);

            InitialiseClient(null, endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        internal void Close()
        {
            repositoryClient.Close();
        }

        /// <summary>
        /// Retrieve a document.
        /// </summary>
        /// <param name="header">PCEHR header.</param>
        /// <param name="requests">Request.</param>
        /// <returns>Response.</returns>
        internal RetrieveDocumentSetResponseType GetDocument(PCEHRHeader header, RetrieveDocumentSetRequestTypeDocumentRequest[] requests)
        {
            var timestamp = new timestampType()
            {
                created = DateTime.Now
            };

            var signatureContainer = new signatureContainerType();

            var result = repositoryClient.DocumentRepository_RetrieveDocumentSet(timestamp, ref signatureContainer, header, requests);

            return result;
        }

        /// <summary>
        /// Upload a document.
        /// </summary>
        /// <param name="header">PCEHR header.</param>
        /// <param name="request">Request.</param>
        /// <returns>Response.</returns>
        internal RegistryResponseType UploadDocument(PCEHRHeader header, ProvideAndRegisterDocumentSetRequestType request)
        {
            var timestamp = new timestampType()
            {
                created = DateTime.Now
            };

            var signatureContainer = new signatureContainerType();

            return repositoryClient.DocumentRepository_ProvideAndRegisterDocumentSetb(timestamp, ref signatureContainer, header, request);
        }

        /// <summary>
        /// Initialises the client endpoint.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        private void InitialiseClient(string endpointUri, string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            // Validation.ValidateArgumentRequired("signingCert", signingCert);
            Validation.ValidateArgumentRequired("tlsCert", tlsCert);

            soapMessages = new SoapMessages();

            DocumentRepository_PortTypeClient client = null;

            if (!string.IsNullOrEmpty(endpointUri))
            {
                var address = new EndpointAddress(endpointUri);

                var mtomBinding = BindingHelper.CreateMtomTlsBinding();

                client = new DocumentRepository_PortTypeClient(mtomBinding, address);
            }
            else if (!string.IsNullOrEmpty(endpointConfigurationName))
            {
                client = new DocumentRepository_PortTypeClient(endpointConfigurationName);
            }

            if (client != null)
            {
                PCEHREndpointProcessor.ProcessEndpoint(client.Endpoint, signingCert, soapMessages);

                if (tlsCert != null)
                {
                    client.ClientCredentials.ClientCertificate.Certificate = tlsCert;
                }

                repositoryClient = client;
            }
        }
    }
}
