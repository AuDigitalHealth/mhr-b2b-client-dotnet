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
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Ionic.Zip;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Nehta.VendorLibrary.Common;
using System.ServiceModel.Description;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// UploadDocumentClient.
    /// </summary>
    public class UploadDocumentClient : IUploadDocumentClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        private readonly DocumentRepositoryClient client;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return client.soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public UploadDocumentClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            client = new DocumentRepositoryClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public UploadDocumentClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback = null)
        {
            client = new DocumentRepositoryClient(endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public UploadDocumentClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            client = new DocumentRepositoryClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public UploadDocumentClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback = null)
        {
            client = new DocumentRepositoryClient(endpointUri, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            client.Close();
        }

        /// <summary>
        /// Upload a document to the PCEHR.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document and metadata.</param>
        /// <returns>Response.</returns>
        public RegistryResponseType UploadDocument(CommonPcehrHeader pcehrHeader, ProvideAndRegisterDocumentSetRequestType request)
        {
            // PCEHRHeaderValidator.Validate(pcehrHeader);
            var header = pcehrHeader.GetHeader<PCEHRHeader>();

            Validation.ValidateArgumentRequired("request", request);

            return client.UploadDocument(header, request);
        }

        /// <summary>
        /// Helper method to generate the request object when submitting a replacement document.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code display name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <param name="uuidOfDocumentToReplace">UUID of document to replace.</param>
        /// <returns>The populated request object.</returns>
        public ProvideAndRegisterDocumentSetRequestType CreateRequestForReplacement(
            byte[] cdaPackageContent,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace)
        {
            Validation.ValidateArgumentRequired("uuidOfDocumentToReplace", uuidOfDocumentToReplace);

            return CreateRequest(
                cdaPackageContent,
                formatCode,
                formatCodeName,
                healthcareFacilityTypeCode,
                practiceSetting,
                uuidOfDocumentToReplace
                );
        }

        /// <summary>
        /// Helper method to generate the request object when submitting a new document to the PCEHR.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code display name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <returns>The populated request object.</returns>
        public ProvideAndRegisterDocumentSetRequestType CreateRequestForNewDocument(
            byte[] cdaPackageContent,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting)
        {
            return CreateRequest(
                cdaPackageContent,
                formatCode,
                formatCodeName,
                healthcareFacilityTypeCode,
                practiceSetting,
                null
                );
        }

        /// <summary>
        /// Helper method to include Repository Unique ID, payload hash and payload size information in the XDS metadata.
        /// The Repository Unique ID must be specified, but the payload hash and payload size information is derived from the request object.
        /// This method must be invoked with a request created from CreateRequestForNewDocument or CreateRequestForReplacement.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="repositoryId">The unique repository ID.</param>
        public void AddRepositoryIdAndCalculateHashAndSize(ProvideAndRegisterDocumentSetRequestType request, string repositoryId)
        {
            try
            {
                var size = request.Document[0].Value.Length;
                var hash = UploadDocumentMetadataClient.CalculateSHA1(request.Document[0].Value);

                var extrinsicObject = request.SubmitObjectsRequest.RegistryObjectList.ExtrinsicObject[0];

                var slotList = extrinsicObject.Slot.ToList();

                slotList.Add(new SlotType1()
                {
                    name = "hash",
                    ValueList = new ValueListType() {Value = new string[] {hash}}
                });

                slotList.Add(new SlotType1()
                {
                    name = "size",
                    ValueList = new ValueListType() {Value = new string[] {size.ToString()}}
                });

                slotList.Add(new SlotType1()
                {
                    name = "repositoryUniqueId",
                    ValueList = new ValueListType() {Value = new string[] {repositoryId}}
                });

                extrinsicObject.Slot = slotList.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error with request object. This method must be invoked with a ProvideAndRegisterDocumentSetRequestType object created using " +
                    "CreateRequestForNewDocument or CreateRequestForReplacement.");
            }
        }

        /// <summary>
        /// Helper method to generate the request object.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <param name="uuidOfDocumentToReplace">UUID of document to replace. NULL for new documents.</param>
        /// <returns>The populated request object.</returns>
        internal ProvideAndRegisterDocumentSetRequestType CreateRequest(
            byte[] cdaPackageContent,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace)
        {
            Validation.ValidateArgumentRequired("cdaPackageContent", cdaPackageContent);
            Validation.ValidateArgumentRequired("formatCode", formatCodeName);
            Validation.ValidateArgumentRequired("formatCodeName", formatCodeName);

            var cdaFile = GetCdaDocument(cdaPackageContent);
            var cdaDoc = new XmlDocument();
            cdaDoc.Load(new MemoryStream(cdaFile));

            var metadata = new XdsMetadata(
                cdaDoc,
                null,
                formatCode,
                formatCodeName,
                healthcareFacilityTypeCode,
                practiceSetting,
                null,
                null,
                false,
                uuidOfDocumentToReplace
                );
            var sor = metadata.CreateSubmitObjectsRequest();

            var request = new ProvideAndRegisterDocumentSetRequestType();
            request.Document = new ProvideAndRegisterDocumentSetRequestTypeDocument[]
            {
                new ProvideAndRegisterDocumentSetRequestTypeDocument()
                {
                    id = "DOCUMENT_SYMBOLICID_01",
                    Value = cdaPackageContent
                }
            };
            request.SubmitObjectsRequest = sor;

            return request;
        }

        /// <summary>
        /// Obtain all the zip file entries and their content.
        /// </summary>
        /// <param name="zipFile">The zip file.</param>
        /// <returns>Zip file entries and their content.</returns>
        internal static byte[] GetCdaDocument(byte[] fileContent)
        {
            var zipFile = ZipFile.Read(new MemoryStream(fileContent));

            if (zipFile != null)
            {
                // Iterate through all entries and add their filename and contents.
                foreach (ZipEntry entry in zipFile.Entries)
                {
                    var readStream = new MemoryStream();

                    // Ony process files.
                    if (!entry.IsDirectory)
                    {
                        string filename = entry.FileName;

                        if (Regex.IsMatch(filename, @"[/\\]?[^/\\]+[/\\][^/\\]+[/\\]CDA_ROOT.XML", RegexOptions.IgnoreCase))
                        {
                            entry.Extract(readStream);
                            return readStream.ToArray();
                        }
                    }
                }
            }

            throw new ApplicationException("Failed to extract CDA_ROOT.XML from zip file.");
        }
    }
}
