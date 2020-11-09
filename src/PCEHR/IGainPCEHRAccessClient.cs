using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGainPCEHRAccessClient : ISoapClient
    {
        /// <summary>
        /// Requests access to an individuals PCEHR. The IHI is specified within the PCEHR header.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="accessPcehrRecord">Access record.</param>
        /// <param name="individual">Matching individual.</param>
        /// <returns>Response.</returns>
        responseStatusType GainPCEHRAccess(CommonPcehrHeader pcehrHeader, gainPCEHRAccessPCEHRRecord accessPcehrRecord,
            out gainPCEHRAccessResponseIndividual individual);
    }
}