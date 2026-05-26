using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetDocumentClient : ISoapClient
    {
        /// <summary>
        /// Retrieve a document. 
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="requests">Request.</param>
        /// <returns>Response.</returns>
        RetrieveDocumentSetResponseType GetDocument(CommonPcehrHeader pcehrHeader, RetrieveDocumentSetRequestTypeDocumentRequest[] requests);

        /// <summary>
        /// Retrieve a document. 
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="requests">Request.</param>
        /// <returns>Response.</returns>    
        Task<DocumentRepository_RetrieveDocumentSetResponse> GetDocumentAsync(CommonPcehrHeader pcehrHeader, RetrieveDocumentSetRequestTypeDocumentRequest[] requests);
    }
}