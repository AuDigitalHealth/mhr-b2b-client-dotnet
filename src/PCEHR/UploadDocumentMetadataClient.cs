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
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using Ionic.Zip;
using System.IO;
using System.Text.RegularExpressions;
using Nehta.VendorLibrary.Common;
using System.Xml;
using System.Security.Cryptography;
using System.ServiceModel.Description;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// UploadDocumentMetadataClient.
    /// </summary>
    public class UploadDocumentMetadataClient : IUploadDocumentMetadataClient
    {
        /// <summary>
        /// Generated client.
        /// </summary>
        private readonly DocumentRegistryClient documentRegistryClient;

        /// <summary>
        /// Contains the request and response SOAP messages after an invocation is made.
        /// </summary>
        public SoapMessages SoapMessages
        {
            get { return documentRegistryClient.soapMessages; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public UploadDocumentMetadataClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointConfigurationName, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointConfigurationName">Configuration name.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public UploadDocumentMetadataClient(string endpointConfigurationName, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointConfigurationName, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        public UploadDocumentMetadataClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointUri, signingCert, tlsCert);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpointUri">Service endpoint.</param>
        /// <param name="signingCert">Header signing certificate.</param>
        /// <param name="tlsCert">TLS client certificate.</param>
        /// <param name="initialisationCallback">Callback for additional configuration after creation.</param>
        public UploadDocumentMetadataClient(Uri endpointUri, X509Certificate2 signingCert, X509Certificate2 tlsCert, Action<ServiceEndpoint> initialisationCallback)
        {
            documentRegistryClient = new DocumentRegistryClient(endpointUri, signingCert, tlsCert, initialisationCallback);
        }

        /// <summary>
        /// Upload metadata for a document.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="submitObjectsRequest">Metadata.</param>
        /// <returns>Response.</returns>
        public RegistryResponseType UploadDocumentMetadata(CommonPcehrHeader pcehrHeader, SubmitObjectsRequest submitObjectsRequest)
        {
            return documentRegistryClient.UploadDocumentMetadata(pcehrHeader.GetHeader<PCEHRHeader>(), submitObjectsRequest);
        }

        /// <summary>
        /// Helper method to generate the request object when registering a replacement document on the PCEHR.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="uniqueRepositoryId">Repository ID.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code display name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <param name="uuidOfDocumentToReplace">UUID of document to replace.</param>
        /// <returns>The populated request object.</returns>
        public SubmitObjectsRequest CreateRequestForReplacement(
            byte[] cdaPackageContent,
            string uniqueRepositoryId,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace)
        {
            Validation.ValidateArgumentRequired("uuidOfDocumentToReplace", uuidOfDocumentToReplace);

            return CreateRequest(
               cdaPackageContent,
               uniqueRepositoryId,
               formatCode,
               formatCodeName,
               healthcareFacilityTypeCode,
               practiceSetting,
               uuidOfDocumentToReplace
               );
        }

        /// <summary>
        /// Helper method to generate the request object when registering a new document on the PCEHR.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="uniqueRepositoryId">Repository ID.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code display name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <returns>The populated request object.</returns>
        public SubmitObjectsRequest CreateRequestForNewDocument(
            byte[] cdaPackageContent,
            string uniqueRepositoryId,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting)
        {
            return CreateRequest(
                cdaPackageContent,
                uniqueRepositoryId,
                formatCode,
                formatCodeName,
                healthcareFacilityTypeCode,
                practiceSetting,
                null
                );
        }

        /// <summary>
        /// Helper method to generate the request object.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// /// <param name="uniqueRepositoryId">Repository ID.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <param name="uuidOfDocumentToReplace">UUID of document to replace. NULL for new documents.</param>
        /// <returns>The populated request object.</returns>
        internal SubmitObjectsRequest CreateRequest(
            byte[] cdaPackageContent,
            string uniqueRepositoryId,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace)
        {
            Validation.ValidateArgumentRequired("cdaPackageContent", cdaPackageContent);
            Validation.ValidateArgumentRequired("uniqueRepositoryId", uniqueRepositoryId);
            Validation.ValidateArgumentRequired("formatCode", formatCodeName);
            Validation.ValidateArgumentRequired("formatCodeName", formatCodeName);

            var cdaFile = GetCdaDocument(cdaPackageContent);
            var cdaDoc = new XmlDocument();
            cdaDoc.Load(new MemoryStream(cdaFile));

            var metadata = new XdsMetadata(
                cdaDoc,
                uniqueRepositoryId,
                formatCode,
                formatCodeName,
                healthcareFacilityTypeCode,
                practiceSetting,
                cdaPackageContent.Length,
                CalculateSHA1(cdaPackageContent),
                true,
                uuidOfDocumentToReplace
                );

            var sor = metadata.CreateSubmitObjectsRequest();
            var serializedSor = sor.SerializeToXml();

            return serializedSor.Deserialize<SubmitObjectsRequest>();
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

        /// <summary>
        /// Generates a hash value for a byte array using SHA1.
        /// </summary>
        /// <param name="content">The byte array to generate the hash from.</param>
        /// <returns>Generated hash value.</returns>
        internal static string CalculateSHA1(byte[] content)
        {
            var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] bytes = cryptoTransformSHA1.ComputeHash(content);
            string hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return (hash);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            documentRegistryClient.Close();
        }
    }
}
