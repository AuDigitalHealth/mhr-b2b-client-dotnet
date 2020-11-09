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
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// GetDocumentListClient.
    /// </summary>
    public class GetDocumentListClient : IGetDocumentListClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        private readonly DocumentRegistryClient documentRegistryClient;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return documentRegistryClient.soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetDocumentListClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetDocumentListClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetDocumentListClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetDocumentListClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointUri, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Gets a list of documents based on the query criteria.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="adhocQueryRequest">Query request.</param>
        /// <returns>Query response.</returns>
        public AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader, AdhocQueryRequest adhocQueryRequest)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);
            Validation.ValidateArgumentRequired("adhocQueryRequest", adhocQueryRequest);

            return documentRegistryClient.GetDocumentList(pcehrHeader.GetHeader<PCEHRHeader>(), adhocQueryRequest);
        }

        /// <summary>
        /// Gets a list of documents based on the query criteria. The IHI of the individual is specified within the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="documentStatus">Status of the documents.</param>
        /// <returns>Query response.</returns>
        public AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader, DocumentStatus documentStatus)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);
            Validation.ValidateArgumentRequired("ihiNumber", pcehrHeader.IhiNumber);

            AdhocQueryBuilder adhocQueryBuilder = new AdhocQueryBuilder(pcehrHeader.IhiNumber, new[] { documentStatus });

            return documentRegistryClient.GetDocumentList(pcehrHeader.GetHeader<PCEHRHeader>(), adhocQueryBuilder.BuildRequest());
        }

        /// <summary>
        /// Gets a list of documents based on the query criteria. The IHI of the individual is specified within the PCEHR header. The
        /// document status is set to 'Approved'.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Query request</returns>
        public AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);

            AdhocQueryBuilder adhocQueryBuilder = new AdhocQueryBuilder(pcehrHeader.IhiNumber, new[] { DocumentStatus.Approved });

            return documentRegistryClient.GetDocumentList(pcehrHeader.GetHeader<PCEHRHeader>(), adhocQueryBuilder.BuildRequest());
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            documentRegistryClient.Close();
        }
    }
}
