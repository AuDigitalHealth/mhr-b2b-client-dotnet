using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using Nehta.VendorLibrary.Common;
using System.Xml;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Class to encapsulate data used for PCEHRHeader.
    /// </summary>
    public class CommonPcehrHeader
    {
        public string IhiNumber { get; set; }
        public string UserId { get; set; }
        public CommonPcehrHeaderUserIDType UserIdType { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public bool UserUseRoleForAudit { get; set; }

        public string OrganisationName { get; set; }
        public string OrganisationId { get; set; }
        public string AlternateOrganisationName { get; set; }

        public CommonPcehrHeaderClientSystemType ClientSystemType { get; set; }

        public string ProductPlatform { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string ProductVendor { get; set; }

        /// <summary>
        /// Converts CommonPcehrHeader into the a specific PCEHRHeader instance.
        /// </summary>
        /// <typeparam name="T">The PCEHRHeader generated type (from WSDL) to be converted to.</typeparam>
        /// <returns>PCEHRHeader instance.</returns>
        public T GetHeader<T>()
        {
            ValidateHeader();
            var xml = Serialize();

            return xml.Deserialize<T>();
        }

        /// <summary>
        /// Serializes CommmonPcehrHeader as a PCEHRHeader.
        /// </summary>
        /// <returns>Serialized PCEHRHeader.</returns>
        public XmlDocument Serialize()
        {
            var pcehrHeader = new PCEHRHeader();

            pcehrHeader.ihiNumber = IhiNumber;

            pcehrHeader.User = new PCEHRHeaderUser();
            pcehrHeader.User.ID = UserId;
            pcehrHeader.User.IDType = (PCEHRHeaderUserIDType)Enum.Parse(typeof(PCEHRHeaderUserIDType), UserIdType.ToString());
            pcehrHeader.User.userName = UserName;
            pcehrHeader.User.role = UserRole;
            pcehrHeader.User.useRoleForAudit = UserUseRoleForAudit;

            pcehrHeader.accessingOrganisation = new PCEHRHeaderAccessingOrganisation();
            pcehrHeader.accessingOrganisation.organisationName = OrganisationName;
            pcehrHeader.accessingOrganisation.organisationID = OrganisationId;
            pcehrHeader.accessingOrganisation.alternateOrganisationName = AlternateOrganisationName;

            pcehrHeader.clientSystemType = (PCEHRHeaderClientSystemType)Enum.Parse(typeof(PCEHRHeaderClientSystemType), ClientSystemType.ToString());

            pcehrHeader.productType = new PCEHRHeaderProductType();
            pcehrHeader.productType.platform = ProductPlatform;
            pcehrHeader.productType.productName = ProductName;
            pcehrHeader.productType.productVersion = ProductVersion;
            pcehrHeader.productType.vendor = ProductVendor;

            return pcehrHeader.SerializeToXml();
        }

        /// <summary>
        /// Validates the instance of CommonPcehrHeader.
        /// </summary>
        public void ValidateHeader()
        {
            Validation.ValidateArgumentRequired("(PCEHR Header)UserId", UserId);
            Validation.ValidateArgumentRequired("(PCEHR Header)UserName", UserName);

            if (UserIdType == CommonPcehrHeaderUserIDType.HPII)
            {
                Validation.ValidateStringLength("(PCEHR Header)UserId", UserId, 16, true);
            }

            Validation.ValidateArgumentRequired("(PCEHR Header)ProductPlatform", ProductPlatform);
            Validation.ValidateArgumentRequired("(PCEHR Header)ProductName", ProductName);
            Validation.ValidateArgumentRequired("(PCEHR Header)ProductVersion", ProductVersion);
            Validation.ValidateArgumentRequired("(PCEHR Header)ProductVendor", ProductVendor);
        }
    }

    /// <summary>
    /// User ID type.
    /// </summary>
    public enum CommonPcehrHeaderUserIDType
    {
        HPII,
        PortalUserIdentifier,
        LocalSystemIdentifier
    }

    /// <summary>
    /// Client system type.
    /// </summary>
    public enum CommonPcehrHeaderClientSystemType
    {
        CIS,
        CSP,
        CRP,
        HI,
        Medicare,
        CPP,
        CCP,
        Other
    }
}
