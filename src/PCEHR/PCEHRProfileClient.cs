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
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// PCEHRProfileClient.
    /// </summary>
    internal class PCEHRProfileClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        internal PCEHRProfilePortTypeClient pcehrProfileClient;

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
        internal PCEHRProfileClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
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
        internal PCEHRProfileClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            Validation.ValidateArgumentRequired("endpointUri", endpointUri);

            InitialiseClient(endpointUri.ToString(), null, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        internal PCEHRProfileClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
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
        internal PCEHRProfileClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            Validation.ValidateArgumentRequired("endpointConfigurationName", endpointConfigurationName);

            InitialiseClient(null, endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        internal void Close()
        {
            pcehrProfileClient.Close();
        }

        /// <summary>
        /// Check if PCEHR exists for an individual.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Query response.</returns>
        internal doesPCEHRExistResponse DoesPCEHRExist(PCEHRHeader pcehrHeader)
        {
            var timestamp = new timestampType()
            {
                created = DateTime.Now
            };

            var signatureContainer = new signatureContainerType();

            return pcehrProfileClient.doesPCEHRExist(timestamp, ref signatureContainer, pcehrHeader, "");
        }

        /// <summary>
        /// Gain access to an individual's PCEHR.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Query response.</returns>
        internal responseStatusType GainPCEHRAccess(PCEHRHeader pcehrHeader, gainPCEHRAccessPCEHRRecord accessPcehrRecord, out gainPCEHRAccessResponseIndividual individual)
        {
            var timestamp = new timestampType()
            {
                created = DateTime.Now
            };

            var signatureContainer = new signatureContainerType();

            return pcehrProfileClient.gainPCEHRAccess(timestamp, ref signatureContainer, pcehrHeader, accessPcehrRecord, out individual);
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

            PCEHRProfilePortTypeClient client = null;

            if (!string.IsNullOrEmpty(endpointUri))
            {
                EndpointAddress address = new EndpointAddress(endpointUri);
                CustomBinding tlsBinding = BindingHelper.CreateTlsBinding();

                client = new PCEHRProfilePortTypeClient(tlsBinding, address);
            }
            else if (!string.IsNullOrEmpty(endpointConfigurationName))
            {
                client = new PCEHRProfilePortTypeClient(endpointConfigurationName);
            }

            if (client != null)
            {
                PCEHREndpointProcessor.ProcessEndpoint(client.Endpoint, signingCert, soapMessages);

                if (tlsCert != null)
                {
                    client.ClientCredentials.ClientCertificate.Certificate = tlsCert;
                }

                pcehrProfileClient = client;

                initialisationCallback?.Invoke(client.Endpoint);
            }
        }

    }
}
