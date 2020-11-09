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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR.GetAuditView;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Client for the 'GetAuditView' service.
    /// </summary>
    public class GetAuditViewClient : IGetAuditViewClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        private GetAuditViewPortTypeClient auditViewClient;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        private SoapMessages soapMessages;

        /// <summary>
        /// Gets the last invocations SOAP messages.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetAuditViewClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            Validation.ValidateArgumentRequired("endpointConfigurationName", endpointConfigurationName);

            InitialiseClient(null, endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetAuditViewClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            Validation.ValidateArgumentRequired("endpointConfigurationName", endpointConfigurationName);

            InitialiseClient(null, endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GetAuditViewClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            Validation.ValidateArgumentRequired("endpointUri", endpointUri);

            InitialiseClient(endpointUri.ToString(), null, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public GetAuditViewClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            Validation.ValidateArgumentRequired("endpointUri", endpointUri);

            InitialiseClient(endpointUri.ToString(), null, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Gets the audit view for an individual.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="dates">The start and end dates</param>
        /// <returns>Response.</returns>
        public getAuditViewResponse GetAuditView(CommonPcehrHeader pcehrHeader, getAuditView dates)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);

            var timestamp = new timestampType()
            {
                created = DateTime.Now
            };

            var signatureContainer = new signatureContainerType();

            return auditViewClient.getAuditView(timestamp, ref signatureContainer, pcehrHeader.GetHeader<PCEHRHeader>(), dates);
        }

        /// <summary>
        /// Initialises the client endpoint.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        private void InitialiseClient(string endpointUri, string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback = null)
        {
            Validation.ValidateArgumentRequired("tlsCert", tlsCert);

            soapMessages = new SoapMessages();

            GetAuditViewPortTypeClient client = null;

            if (!string.IsNullOrEmpty(endpointUri))
            {
                EndpointAddress address = new EndpointAddress(endpointUri);
                CustomBinding tlsBinding = BindingHelper.CreateTlsBinding();

                client = new GetAuditViewPortTypeClient(tlsBinding, address);
            }
            else if (!string.IsNullOrEmpty(endpointConfigurationName))
            {
                client = new GetAuditViewPortTypeClient(endpointConfigurationName);
            }

            if (client != null)
            {
                PCEHREndpointProcessor.ProcessEndpoint(client.Endpoint, signingCert, soapMessages);

                if (tlsCert != null)
                {
                    client.ClientCredentials.ClientCertificate.Certificate = tlsCert;
                }

                auditViewClient = client;

                initialisationCallback?.Invoke(client.Endpoint);
            }
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            auditViewClient.Close();
        }
    }
}
