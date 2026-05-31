using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetChangeHistoryView;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetChangeHistoryViewClient : ISoapClient
    {
        /// <summary>
        /// Gets the change history view for a document.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document unique ID.</param>
        /// <returns>Change history for the document.</returns>
        getChangeHistoryViewResponse GetChangeHistoryView(CommonPcehrHeader pcehrHeader, getChangeHistoryView request);

        /// <summary>
        /// Gets the change history view for a document.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Document unique ID.</param>
        /// <returns>Change history for the document.</returns>
        Task<getChangeHistoryViewResponse> GetChangeHistoryViewAsync(CommonPcehrHeader pcehrHeader, getChangeHistoryView request);
    }
}