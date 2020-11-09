using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetIndividualDetailsView;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetIndividualDetailsViewClient : ISoapClient
    {
        /// <summary>
        /// Gets details view for an individual.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">The request object.</param>
        /// <returns>Response.</returns>
        getIndividualDetailsViewResponse GetIndividualDetailsView(CommonPcehrHeader pcehrHeader, object request);
    }
}