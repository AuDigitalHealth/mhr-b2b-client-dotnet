using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.VendorLibrary.PCEHR;

namespace PCEHR.Sample
{
    public static class PcehrHeaderHelper
    {
        public static CommonPcehrHeader CreateHeader()
        {
            // Create the PCEHR header
            var pcehrHeader = new CommonPcehrHeader();

            // this is the patient's Individual Healthcare Identifier and is always 16 digits long
            pcehrHeader.IhiNumber = "IHI";

            //User ID should be a HPI-I if the user is HPI-I eligible (i.e. AHPRA registered)
            // If the user isn't HPI-I eligible (such as support staff and scientific staff) then set the user ID to a local ID and
            // Set pcehrHeader.UserIdType to PCEHRHeaderUserIDType.LocalSystemIdentifier;
            pcehrHeader.UserId = "user ID";
            pcehrHeader.UserIdType = CommonPcehrHeaderUserIDType.HPII;
            pcehrHeader.UserName = "user name";

            // "organisation name" and "organisation HPIO" can be found in the NASH PKI Test Kit
            // HPI-O is always 16 digits long
            pcehrHeader.OrganisationName = "organisation name";
            pcehrHeader.OrganisationId = "organisation HPIO";

            pcehrHeader.ClientSystemType = CommonPcehrHeaderClientSystemType.CIS;

            // Below information can be found in the My Health Record Vendor Product Details Form
            // that you filled out and submitted
            pcehrHeader.ProductPlatform = "platform";
            pcehrHeader.ProductName = "product name";
            pcehrHeader.ProductVersion = "product version";
            pcehrHeader.ProductVendor = "product vendor";

            return pcehrHeader;
        }
    }
}
