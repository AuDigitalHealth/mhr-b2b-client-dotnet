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
using Nehta.VendorLibrary.PCEHR.DocumentRepository;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'UploadDocumentClient' client.
    /// </summary>
    /// See the Document Exchange Service Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// @JP Ensure a record exists for the patient before attempting to gain access. (See DoesPCEHRExistClientSample.cs)
    /// Note: This is typically the example to follow when uploading a document to the My Health Record.UploadDocumentMetadataClientSample.cs
    /// is only to be used for registered repositories such as Medicare Australia.
    /// The uploadDocumentClient is also used for superseding a document.
    /// 
    public class UploadDocumentClientSample
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
            // SVT endpoint is https://b2b.ehealthvendortest.health.gov.au/uploadDocument
            // production endpoint is https://services.ehealth.gov.au/uploadDocument
            UploadDocumentClient uploadDocumentClient = new UploadDocumentClient(
                new Uri("https://UploadDocumentEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;     

            byte[] packageBytes = File.ReadAllBytes("CdaPackage.zip"); // Create a package

            // Create a request to register a new document on the PCEHR.
            // Create a request to register a new document on the PCEHR.
            // Format codes and format code names are not fixed, and it is recommended for them to be configurable.
            // formatCode is the Template Package ID for each clinical document, formatCodeName is the Document type
            // please find specific details for each clinical document type on https://digitalhealth.gov.au/implementation-resources/clinical-documents
            // formatCodeName can be read in Table 3 of the Document Exchange Service Technical Service Specification
            // For example (formateCodeName - formatCode):
            // "eHealth Dispense Record" - 1.2.36.1.2001.1006.1.171.5
            // "Pathology Report" - 1.2.36.1.2001.1006.1.220.4
            // "Diagnostic Imaging Report" - 1.2.36.1.2001.1006.1.222.4
            ProvideAndRegisterDocumentSetRequestType request = uploadDocumentClient.CreateRequestForNewDocument(
                packageBytes,
                "formatCode",       
                "formatCodeName",
                HealthcareFacilityTypeCodes.GeneralPractice,                // Update to relevant code
                PracticeSettingTypes.GeneralPracticeMedicalClinicService    // Update to relevant code
                );

            // To supercede / amend an existing document, the same UploadDocument call is used. However, the request is 
            // prepared using the CreateRequestForReplacement function.

            // Note that the new document must have a different UUID/GUID to the one it is replacing.
            // the uuidOfDocumentToReplace must be converted to OID format and include the repository OID. 
            // (i.e. a document being replaced in the My Health Record repository is)

            // ProvideAndRegisterDocumentSetRequestType request = uploadDocumentClient.CreateRequestForReplacement(
            //    packageBytes,
            //    "formatCode",
            //    "formatCodeName",
            //    HealthcareFacilityTypeCodes.GeneralPractice,
            //    PracticeSettingTypes.GeneralPracticeMedicalClinicService,
            //    "uuidOfDocumentToReplace"
            //    );

            // When uploading to the NPDR where the repository unique ID, document size and hash may need to be included
            // in the metadata, use the utility function below.

            // uploadDocumentClient.AddRepositoryIdAndCalculateHashAndSize(request, "REPOSITORY_UNIQUE_ID");

            try
            {
                // Invoke the service
                RegistryResponseType registryResponse = uploadDocumentClient.UploadDocument(header, request);

                // Get the soap request and response
                string soapRequest = uploadDocumentClient.SoapMessages.SoapRequest;
                string soapResponse = uploadDocumentClient.SoapMessages.SoapResponse;
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
