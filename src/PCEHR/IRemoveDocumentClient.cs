using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.RemoveDocument;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IRemoveDocumentClient : ISoapClient
    {
        /// <summary>
        /// Removes a document with the document ID.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document unique ID and reason for removal.</param>
        /// <returns>Response.</returns>
        removeDocumentResponse RemoveDocument(CommonPcehrHeader pcehrHeader, removeDocument request);

        /// <summary>
        /// Removes a document with the document ID.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document unique ID and reason for removal.</param>
        /// <returns>Response.</returns>
        Task<removeDocumentResponse> RemoveDocumentAsync(CommonPcehrHeader pcehrHeader, removeDocument request);
    }
}