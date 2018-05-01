using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nehta.VendorLibrary.PCEHR
{


    /// <summary>
    /// Class to contain the soap request and response messages.
    /// </summary>
    public class SoapMessages
    {
        public enum SignatureStatus
        {
            Valid,
            Invalid,
            NotPresent
        }

        /// <summary>
        /// Soap request XML.
        /// </summary>
        public string SoapRequest { get; set; }

        /// <summary>
        /// Soap response XML.
        /// </summary>
        public string SoapResponse { get; set; }

        /// <summary>
        /// MTOM request.
        /// </summary>
        public byte[] MtomRequest { get; set; }

        /// <summary>
        /// MTOM response.
        /// </summary>
        public byte[] MtomResponse { get; set; }

        /// <summary>
        /// Soap request Message ID.
        /// </summary>
        public string SoapRequestMessageId { get; set; }

        /// <summary>
        /// Indicates if the SOAP response signature is valid.
        /// </summary>
        public SignatureStatus SoapResponseSignatureStatus { get; internal set; }
    }
}
