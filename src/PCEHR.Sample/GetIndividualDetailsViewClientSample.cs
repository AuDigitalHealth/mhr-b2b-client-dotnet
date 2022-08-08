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
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using System.IO;
using Nehta.VendorLibrary.PCEHR.GetIndividualDetailsView;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GetIndividualDetailsViewClient'.
    /// </summary>
    /// See the View Service Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// Ensure GainAccess has already been called. (see GainPCEHRAccessClientSample.cs)
    public class GetIndividualDetailsViewClientSample
    {
        public void Sample()
        {
            // NASH certificate should be used here, NOT the HI certificate
            // the NASH certificate can be found in the NASH PKI Test Kit
            // certificate needs to be installed in the right place
            // the "Issue To" field of a NASH certificate looks like general(or something different)."HPI-O".electronichealth.net.au
            // "Serial Number" can be found in the details once the certificate is installed. e.g. in Windows, certificates can be found in Certs.msc

            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();
            // Override this value to the current patient's IHI.
            header.IhiNumber = "IHI";

            // Create the client
            // SVT endpoint is "https://services.svt.gw.myhealthrecord.gov.au/getRepresentativeList"
            // production endpoint is "https://services.ehealth.gov.au/getRepresentativeList"
            GetIndividualDetailsViewClient getIndividualDetailsClient = new GetIndividualDetailsViewClient(
                new Uri("https://GetIndividualDetailsViewClientEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            object request = (string)"";

            try
            {
                // Invoke the service
                getIndividualDetailsViewResponse response = getIndividualDetailsClient.GetIndividualDetailsView(header, request);
            }
            catch (FaultException e)
            {
                // Handle any errors
            }
        }

        private bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Checks can be done here to validate the service certificate.
            // If the service certificate contains any problems or is invalid, return false. Otherwise, return true.
            // This example returns true to indicate that the service certificate is valid.
            return true;
        }
    }
}
