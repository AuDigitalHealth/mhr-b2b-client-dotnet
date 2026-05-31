using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetView;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetViewClient : ISoapClient
    {
        /// <summary>
        /// Gets a PCEHR view.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response.</returns>
        getViewResponse GetView(CommonPcehrHeader pcehrHeader, getView request);

        /// <summary>
        /// Gets a PCEHR view.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response.</returns>
        Task<getViewResponse> GetViewAsync(CommonPcehrHeader pcehrHeader, getView request);
    }
}