using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetAuditView;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets the audit view for an individual.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="dates">The start and end dates</param>
        /// <returns>Response.</returns>
        Task<getAuditViewResponse1> GetAuditViewAsync(CommonPcehrHeader pcehrHeader, getAuditView dates);

	}
}