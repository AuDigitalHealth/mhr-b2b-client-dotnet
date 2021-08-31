using System;

namespace Nehta.VendorLibrary.PCEHR
{
    public class XdsRecord
    {
        public string lid { get; set; }
        public string status { get; set; }
        public bool docWithdrawn { get; set; }
        public string removeReason { get; set; }
        public DateTime creationTimeUTC { get; set; } //used as Document Date in the Document List
        public string languageCode { get; set; }
        public DateTime serviceStartTimeUTC { get; set; }
        public DateTime serviceStopTimeUTC { get; set; } //used as Service Date in the Document List
        public string ihiNumber { get; set; }
        public string hash { get; set; }
        public string size { get; set; }
        public string repositoryUniqueId { get; set; } //required to retrieve this document
        public string recordVersion { get; set; }
        public XdsAuthorInstitution authorInstitution { get; set; }
        public XdsAuthorPerson authorPerson { get; set; }
        public string authorSpeciality { get; set; }
        public string classCode { get; set; }
        public string classCodeDisplayName { get; set; } //used as Document Type in the Document List
        public string formatCode { get; set; }
        public string formatCodeDisplayName { get; set; }
        public string healthcareFacilityTypeCode { get; set; }
        public string healthcareFacilityTypeCodeDisplayName { get; set; }//used as Organisation Type in the Document List
        public string practiceSettingCode { get; set; }
        public string practiceSettingCodeDisplayName { get; set; }
        public string typeCode { get; set; }
        public string typeCodeDisplayName { get; set; }
        public string documentId { get; set; } //required to retrieve this document
    }

    public class XdsAuthorInstitution
    {
        public string institutionName { get; set; }//used as Organisation in the Document List
        public string institutionIdentifier { get; set; }
    }

    public class XdsAuthorPerson
    {
        public string authorPrefix { get; set; }
        public string authorGivenName { get; set; }
        public string authorFamilyName { get; set; }
        public string authorQualifiedIdentifier { get; set; }
        public string authorQualifiedIdentifierExtension { get; set; }
    }

}
