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
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Client for the 'DoesPCEHRExist' service.
    /// </summary>
    public class DoesPCEHRExistClient
    {
        /// <summary>
        /// Profile client.
        /// </summary>
        private readonly PCEHRProfileClient profileClient;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return profileClient.soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public DoesPCEHRExistClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            profileClient = new PCEHRProfileClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public DoesPCEHRExistClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            profileClient = new PCEHRProfileClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Checks if a PCEHR exists for an individual. The IHI of the individual is specified wihin the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response indicating the existence of a PCEHR.</returns>
        public doesPCEHRExistResponse DoesPCEHRExist(CommonPcehrHeader pcehrHeader)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);

            return profileClient.DoesPCEHRExist(pcehrHeader.GetHeader<PCEHRHeader>());
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            profileClient.Close();
        }
    }
}
