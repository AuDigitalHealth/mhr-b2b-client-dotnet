using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IUploadDocumentClient : ISoapClient
    {
        /// <summary>
        /// Upload a document to the PCEHR.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document and metadata.</param>
        /// <returns>Response.</returns>
        RegistryResponseType UploadDocument(CommonPcehrHeader pcehrHeader, ProvideAndRegisterDocumentSetRequestType request);

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
        ProvideAndRegisterDocumentSetRequestType CreateRequestForReplacement(
            byte[] cdaPackageContent,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting,
            string uuidOfDocumentToReplace);

        /// <summary>
        /// Helper method to generate the request object when submitting a new document to the PCEHR.
        /// </summary>
        /// <param name="cdaPackageContent">Byte content of the CDA package.</param>
        /// <param name="formatCode">Format code.</param>
        /// <param name="formatCodeName">Format code display name.</param>
        /// <param name="healthcareFacilityTypeCode">Healthcare facility type code.</param>
        /// <param name="practiceSetting">Practice setting code.</param>
        /// <returns>The populated request object.</returns>
        ProvideAndRegisterDocumentSetRequestType CreateRequestForNewDocument(
            byte[] cdaPackageContent,
            string formatCode,
            string formatCodeName,
            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
            PracticeSettingTypes practiceSetting);

        /// <summary>
        /// Helper method to include Repository Unique ID, payload hash and payload size information in the XDS metadata.
        /// The Repository Unique ID must be specified, but the payload hash and payload size information is derived from the request object.
        /// This method must be invoked with a request created from CreateRequestForNewDocument or CreateRequestForReplacement.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="repositoryId">The unique repository ID.</param>
        void AddRepositoryIdAndCalculateHashAndSize(ProvideAndRegisterDocumentSetRequestType request, string repositoryId);
    }
}