using Nehta.VendorLibrary.PCEHR;

namespace Nehta.VendorLibrary.MHR
{
    public interface ISoapClient
    {
        SoapMessages SoapMessages { get; }

        /// <summary>
        /// Close the client.
        /// </summary>
        void Close();
    }
}
