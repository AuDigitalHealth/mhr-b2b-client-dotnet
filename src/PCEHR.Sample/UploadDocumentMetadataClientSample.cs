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
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'UploadDocumentMetadataClient' client.
    /// This function is used to register metadata with the XDS in My Health Record (PCEHR)
    /// you don't need this if you only upload CDA packages to the My Health Record
    /// </summary>
    /// 
    /// See the Document Exchange Service Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// Ensure a record exists for the patient before attempting to gain access. (See DoesPCEHRExistClientSample.cs)
    /// Note: WARNING: This example is only for when you are operating a registered repository such as Medicare Australia.
    /// If you are looking to upload a document to My Health Record see  UploadDocumentClientSample.cs.     
    public class UploadDocumentMetadataClientSample
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
            UploadDocumentMetadataClient uploadDocumentMetadataClient = new UploadDocumentMetadataClient(
                new Uri("https://UploadDocumentEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;   

            byte[] packageBytes = File.ReadAllBytes("CdaPackage.zip"); // Create a package

            // Create a request from an existing package
            // Format codes and format code names are not fixed, and it is recommended for them to be configurable.
            SubmitObjectsRequest request = uploadDocumentMetadataClient.CreateRequestForNewDocument(
                packageBytes,
                "unique repository ID",   
                "formatCode",
                "formatCodeName",
                HealthcareFacilityTypeCodes.GeneralPractice,
                PracticeSettingTypes.GeneralPracticeMedicalClinicService
                );



            try
            {
                // Invoke the service
                RegistryResponseType registryResponse = uploadDocumentMetadataClient.UploadDocumentMetadata(header, request);

                // Get the soap request and response
                string soapRequest = uploadDocumentMetadataClient.SoapMessages.SoapRequest;
                string soapResponse = uploadDocumentMetadataClient.SoapMessages.SoapResponse;
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
