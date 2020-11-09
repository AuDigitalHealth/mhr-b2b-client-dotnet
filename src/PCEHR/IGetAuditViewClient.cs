using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetAuditView;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetAuditViewClient : ISoapClient
    {
        /// <summary>
        /// Gets the audit view for an individual.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="dates">The start and end dates</param>
        /// <returns>Response.</returns>
        getAuditViewResponse GetAuditView(CommonPcehrHeader pcehrHeader, getAuditView dates);
    }
}