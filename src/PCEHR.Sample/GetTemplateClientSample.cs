﻿/*
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
using Nehta.VendorLibrary.PCEHR.Enums;
using Nehta.VendorLibrary.PCEHR.GetTemplate;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GetTemplateClient' client.
    /// </summary>
    public class GetTemplateClientSample
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
            // Interface requires this to be blank
            header.IhiNumber = null;

            // Create the client
            GetTemplateClient templateClient = new GetTemplateClient(new Uri("https://GetTemplateEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;     

            // Specify the objects that will hold the output
            getTemplateResponseTemplate responseTemplate;
            DateTime cacheExpiry = new DateTime();

            try
            {
                var request = new getTemplate()
                {
                    serviceRequestorOption = ServiceRequestorOption.FullPackage.ToString(),
                    templateID = "document unique ID"
                };

                // Invoke the service
                var responseStatusType = templateClient.GetTemplate(
                    header, 
                    request);

                // Get the soap request and response
                string soapRequest = templateClient.SoapMessages.SoapRequest;
                string soapResponse = templateClient.SoapMessages.SoapResponse;
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
