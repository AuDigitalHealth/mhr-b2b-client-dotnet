using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetRepresentativeList;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetRepresentativeListClient : ISoapClient
    {
        /// <summary>
        /// Get a representative list.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response.</returns>
        getRepresentativeListResponse GetRepresentativeList(CommonPcehrHeader pcehrHeader);

        /// <summary>
        /// Get a representative list.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <returns>Response.</returns>
        Task<getRepresentativeListResponse1> GetRepresentativeListAsync(CommonPcehrHeader pcehrHeader);
    }
}