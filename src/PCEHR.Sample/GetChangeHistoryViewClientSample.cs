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
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.GetChangeHistoryView;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GetChangeHistoryView' client.
    /// </summary>
    /// See the View Service Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// Ensure GainAccess has already been called. (see GainPCEHRAccessClientSample.cs)
    /// 
    public class GetChangeHistoryViewClientSample
    {
        public void Sample()
        {
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

            // Instantiate the client
            // SVT endpoint is "https://services.svt.gw.myhealthrecord.gov.au/getChangeHistoryView"
            // production endpoint is "https://services.ehealth.gov.au/getChangeHistoryView"
            GetChangeHistoryViewClient changeHistoryViewClient = new GetChangeHistoryViewClient(new Uri("https://GetChangeHistoryViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;      

            try
            {
                // Invoke the service
                // if you run GetDocumentList first, you will get a list of document ids in the response
                // the "value" attribute in the <ns3:ExternalIdentifier> element provides the unique document id
                var changeHistoryView = changeHistoryViewClient.GetChangeHistoryView(
                    header, new getChangeHistoryView() { documentID = "document unique id" }
                    );

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
