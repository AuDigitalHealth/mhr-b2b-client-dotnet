using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;

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
    }
}