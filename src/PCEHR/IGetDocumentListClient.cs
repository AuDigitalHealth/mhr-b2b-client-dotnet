using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetDocumentListClient : ISoapClient
    {
        /// <summary>
        /// Gets a list of documents based on the query criteria.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="adhocQueryRequest">Query request.</param>
        /// <returns>Query response.</returns>
        AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader, AdhocQueryRequest adhocQueryRequest);

        /// <summary>
        /// Gets a list of documents based on the query criteria. The IHI of the individual is specified within the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="documentStatus">Status of the documents.</param>
        /// <returns>Query response.</returns>
        AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader, DocumentStatus documentStatus);

        /// <summary>
        /// Gets a list of documents based on the query criteria. The IHI of the individual is specified within the PCEHR header. The
        /// document status is set to 'Approved'.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Query request</returns>
        AdhocQueryResponse GetDocumentList(CommonPcehrHeader pcehrHeader);
    }
}