using Nehta.VendorLibrary.PCEHR;
using System.Threading.Tasks;

namespace Nehta.VendorLibrary.MHR
{
    public interface ISoapClient
    {
        SoapMessages SoapMessages { get; }

        /// <summary>
        /// Close the client.
        /// </summary>
        void Close();

        /// <summary>
        /// Close the client.
        /// </summary>
        Task CloseAsync();
    }
}
