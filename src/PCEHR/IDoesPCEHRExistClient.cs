using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IDoesPCEHRExistClient : ISoapClient
    {
        /// <summary>
        /// Checks if a PCEHR exists for an individual. The IHI of the individual is specified wihin the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response indicating the existence of a PCEHR.</returns>
        doesPCEHRExistResponse DoesPCEHRExist(CommonPcehrHeader pcehrHeader);
    }
}