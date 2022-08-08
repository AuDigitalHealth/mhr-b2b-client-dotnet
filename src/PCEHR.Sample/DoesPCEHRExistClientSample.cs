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
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;
using System.Net;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'DoesPCEHRExist' client.
    /// </summary>
    /// 
    /// See the Record Access Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// 
    public class DoesPCEHRExistClientSample
    {
        public void Sample()
        {
            // NASH certificate should be used here, NOT the HI/Medicare certificates
            // The NASH certificate can be found in the NASH PKI Test Kit
            // To receive the Test Kit fill the application to request a National Authentication Service 
            // for Health(NASH) Public Key Infrastructure(PKI) test certificate kit form
            // https://myhealthrecorddeveloper.digitalhealth.gov.au/resources/prepare/my-health-record-system-pre-requisites#step-2
            // Insure the certificate is installed in  Current User -> Personal -> Certificates
            // (run MMC and add the certificates add - on to view)
            // The "Issue To" field of a NASH certificate looks like general(or something different)."HPI-O".electronichealth.net.au
            // "Serial Number" can be found in the details once the certificate is installed.
            // Type the Serial number, don't copy and paste it, remove the spaces and use uppercase.

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
            // SVT endpoint is "https://services.svt.gw.myhealthrecord.gov.au/doesPCEHRExist"
            // Production endpoint is "https://services.ehealth.gov.au/doesPCEHRExist"
            DoesPCEHRExistClient doesPcehrExistClient = new DoesPCEHRExistClient(new Uri("https://DoesPCEHRExistEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                // Invoke the service
                doesPCEHRExistResponse response = doesPcehrExistClient.DoesPCEHRExist(header);

                // Get the soap request and response
                string soapRequest = doesPcehrExistClient.SoapMessages.SoapRequest;
                string soapResponse = doesPcehrExistClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
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
