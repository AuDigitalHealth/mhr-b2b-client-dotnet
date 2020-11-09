using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.GetTemplate;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface IGetTemplateClient : ISoapClient
    {
        /// <summary>
        /// Gets a template.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Template parameters.</param>
        /// <returns>Response.</returns>
        getTemplateResponse GetTemplate(CommonPcehrHeader pcehrHeader, getTemplate request);
    }
}