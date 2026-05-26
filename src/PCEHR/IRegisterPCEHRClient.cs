using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.RegisterPCEHR;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IRegisterPCEHRClient : ISoapClient
    {
        /// <summary>
        /// Register a person for PCEHR.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">The request object.</param>
        /// <returns>Response.</returns>
        registerPCEHRResponse RegisterPCEHR(CommonPcehrHeader pcehrHeader, registerPCEHR request);

        /// <summary>
        /// Register a person for PCEHR.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">The request object.</param>
        /// <returns>Response.</returns>
        Task<registerPCEHRResponse1> RegisterPCEHRAsync(CommonPcehrHeader pcehrHeader, registerPCEHR request);
    }
}