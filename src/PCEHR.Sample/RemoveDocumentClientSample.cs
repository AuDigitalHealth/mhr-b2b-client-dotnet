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
using Nehta.VendorLibrary.PCEHR.RemoveDocument;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'RemoveDocumentClient' client.
    /// </summary>
    /// See the Document Exchange Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// Ensure GainAccess has already been called. (see GainPCEHRAccessClientSample.cs)
    public class RemoveDocumentClientSample
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

            // Create the client
            // SVT endpoint is "https://services.svt.gw.myhealthrecord.gov.au/removeDocument"
            // production endpoint is "https://services.ehealth.gov.au:443/removeDocument"
            RemoveDocumentClient removeDocumentClient = new RemoveDocumentClient(
                new Uri("https://RemoveDocumentEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;     

            try
            {
                var request = new removeDocument()
                {
                    // this should be the value of the ExternalIdentifier "XDSDocumentEntry.uniqueId" in the GetDocumentList response
                    documentID = "document ID",
                    // reasonForRemoval should be one of:
                    // removeDocumentReasonForRemoval.IncorrectIdentity
                    // removeDocumentReasonForRemoval.ElectToRemove
                    // removeDocumentReasonForRemoval.Withdrawn
                    reasonForRemoval = removeDocumentReasonForRemoval.Withdrawn
                };

                // Invoke the service
                var responseStatus = removeDocumentClient.RemoveDocument(header, request);

                // Get the soap request and response
                string soapRequest = removeDocumentClient.SoapMessages.SoapRequest;
                string soapResponse = removeDocumentClient.SoapMessages.SoapResponse;
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
