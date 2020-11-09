using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IUploadDocumentMetadataClient : ISoapClient
    {
        /// <summary>
        /// Upload metadata for a document.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="submitObjectsRequest">Metadata.</param>
        /// <returns>Response.</returns>
        RegistryResponseType UploadDocumentMetadata(CommonPcehrHeader pcehrHeader, SubmitObjectsRequest submitObjectsRequest);

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
        SubmitObjectsRequest CreateRequestForReplacement(
            byte[] cdaPackageContent,
            string uniqueRepositoryId,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace);

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
        SubmitObjectsRequest CreateRequestForNewDocument(
            byte[] cdaPackageContent,
            string uniqueRepositoryId,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting);
    }
}