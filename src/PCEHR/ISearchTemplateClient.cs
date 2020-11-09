using Nehta.VendorLibrary.MHR;
using Nehta.VendorLibrary.PCEHR.SearchTemplate;

namespace Nehta.VendorLibrary.PCEHR
{
    public interface ISearchTemplateClient : ISoapClient
    {
        /// <summary>
        /// Searches for a template.
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <param name="request">Template ID and metadata of the search.</param>
        /// <returns>Response.</returns>
        searchTemplateResponse SearchTemplate(CommonPcehrHeader pcehrHeader, searchTemplate request);
    }
}