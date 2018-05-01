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
using Nehta.VendorLibrary.PCEHR.Helper;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Client for the 'GainPCEHRAccess' service.
    /// </summary>
    public class GainPCEHRAccessClient
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
        public GainPCEHRAccessClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            profileClient = new PCEHRProfileClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public GainPCEHRAccessClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            profileClient = new PCEHRProfileClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Requests access to an individuals PCEHR. The IHI is specified within the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="accessPcehrRecord">Access record.</param>
        /// <param name="individual">Matching individual.</param>
        /// <returns>Response.</returns>
        public responseStatusType GainPCEHRAccess(CommonPcehrHeader pcehrHeader, gainPCEHRAccessPCEHRRecord accessPcehrRecord,
            out gainPCEHRAccessResponseIndividual individual)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);
            Validation.ValidateArgumentRequired("accessPcehrRecord", accessPcehrRecord);

            return profileClient.GainPCEHRAccess(pcehrHeader.GetHeader<PCEHRHeader>(), accessPcehrRecord, out individual);
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
