/*
 * Copyright 2012 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using System.Xml;

namespace Nehta.VendorLibrary.PCEHR
{
    public class XdsMetadata
    {
        #region Private Constants

        // RegistryObject
        private const string XDS_REGISTRY_PACKAGE = "urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:RegistryPackage";
        private const string XDS_CLASSIFICATION = "urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:Classification";
        private const string XDS_EXTERNAL_IDENTIFIER = "urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:ExternalIdentifier";
        private const string XDS_ASSOCIATION = "urn:oasis:names:tc:ebxml-regrep:ObjectType:RegistryObject:Association";

        // XDSSubmissionSet
        private const string XDS_SUBMISSION_SET = "urn:uuid:a54d6aa5-d40d-43f9-88c5-b4633d873bdd";                        // 	 	ClassificationNode 	R/R 
        private const string XDS_SUBMISSION_SET_AUTHOR = "urn:uuid:a7058bb9-b4e4-4307-ba5b-e3f0ab85e12d";                 // 	 	External Classification Scheme 	R2/R 
        private const string XDS_SUBMISSION_SET_CONTENT_TYPE_CODE = "urn:uuid:aa543740-bdda-424e-8c96-df4873be8500";        // 	 	External Classification Scheme 	R/R 
        private const string XDS_SUBMISSION_SET_UNIQUE_ID = "urn:uuid:96fdda7c-d067-4183-912e-bf5ee74998a8";               // 	 	External Identifer 	R/R 
        private const string XDS_SUBMISSION_SET_SOURCE_ID = "urn:uuid:554ac39e-e3fe-47fe-b233-965d2a147832";               // 	 	External Identifer 	R/R 
        private const string XDS_SUBMISSION_SET_PATIENT_ID = "urn:uuid:6b5aea1a-874d-4603-a4bc-96a0a7b38446";              // 	 	External Identifier 	R/R 

        // XDSDocumentEntry
        private const string XDS_DOCUMENT_ENTRY = "urn:uuid:7edca82f-054d-47f2-a032-9b2a5b5186c1";                        // 	 	ClassificationNode 	R/R 
        private const string XDS_DOCUMENT_ENTRY_AUTHOR = "urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d";                 // 	 	External Classification Scheme 	R2/R 
        private const string XDS_DOCUMENT_ENTRY_CLASS_CODE = "urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a";              // 	 	External Classification Scheme 	R/R 
        private const string XDS_DOCUMENT_ENTRY_CONFIDENTIALITY_CODE = "urn:uuid:f4f85eac-e6cb-4883-b524-f2705394840f";    // 	 	External Classification Scheme 	R/P 
        private const string XDS_DOCUMENT_ENTRY_EVENT_CODE_LIST = "urn:uuid:2c6b8cb7-8b2a-4051-b291-b1ae6a575ef4";          // 	 	External Classification Scheme 	O/R 
        private const string XDS_DOCUMENT_ENTRY_FORMAT_CODE = "urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d";             // 	 	External Classification Scheme 	R/R 
        private const string XDS_DOCUMENT_ENTRY_HEALTHCARE_FACILITY_TYPE_CODE = "urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1"; // 	External Classification Scheme 	R/R 
        private const string XDS_DOCUMENT_ENTRY_PATIENT_ID = "urn:uuid:58a6f841-87b3-4a3e-92fd-a8ffeff98427";              // 	 	ExternalIdentifier 	R/R 
        private const string XDS_DOCUMENT_ENTRY_PRACTICE_SETTING_CODE = "urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead";    // 	 	External Classification Scheme 	R/R 
        private const string XDS_DOCUMENT_ENTRY_TYPE_CODE = "urn:uuid:f0306f51-975f-434e-a61c-c59651d33983";               // 	 	External Classification Scheme 	R/R 
        private const string XDS_DOCUMENT_ENTRY_UNIQUE_ID = "urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab";               // 	 	ExternalIdentifier 	R/R 

        // Association Type
        private const string XDS_ASSOCIATION_TYPE_HAS_MEMBER = "urn:oasis:names:tc:ebxml-regrep:AssociationType:HasMember";
        private const string XDS_ASSOCIATION_TYPE_APND = "urn:ihe:iti:2007:AssociationType:APND";
        private const string XDS_ASSOCIATION_TYPE_RPLC = "urn:ihe:iti:2007:AssociationType:RPLC";
        private const string XDS_ASSOCIATION_TYPE_XFRM = "urn:ihe:iti:2007:AssociationType:XFRM";
        private const string XDS_ASSOCIATION_TYPE_XFRM_RPLC = "urn:ihe:iti:2007:AssociationType:XFRM_RPLC";
        private const string XDS_ASSOCIATION_TYPE_SIGNS = "urn:ihe:iti:2007:AssociationType:signs";
        private const string XDS_ASSOCIATION_TYPE_UPDATE_AVAILABILITY_STATUS = "urn:ihe:iti:2010:AssociationType:UpdateAvailabilityStatus";

        #endregion

        private const string SubmissionSetId = "SUBSET_SYMBOLICID_01";
        private const string DocumentId = "DOCUMENT_SYMBOLICID_01";
        private const string DateTimeFormatString = "yyyyMMddHHmmss";
        private const string formatCodeCodingScheme = "PCEHR_FormatCodes";

        // Document (Class) Type;
        private string documentClassCode;
        private string documentTypeCodeSystem;
        private string documentClassCodeSystemName;
        private string documentClassDisplayName;
        private string documentClassCodeDisplayName;

        // Type Code - For Advance care type and other sub types
        private string documentTypeCode_cl07;
        private string documentTypeDisplayName_cl07;
        private string documentTypeCodeSystemName_cl07;

        // Get Data
        private string effectiveTime;
        private string serviceStartTime;
        private string serviceStopTime;
        private string languageCode;
        private string cdaTitle;
        private string documentTitle;
        private string documentId;
        private string documentIdExtension;
        private string cdaDocumentIdOid;

        // Template Id
        private string templateId;
        private string templateDesc;

        // Author org
        private string authorQualifiedOrgId;
        private string authorOrgName;

        // Author
        private string authorQualifiedId;
        private string authorQualifiedIdExtension;
        private string authorFamily;
        private string authorGiven;
        private string authorPrefix;
        private string authorSuffix;

        // Author specialty
        private string authorSpecialty;

        // HI numbers (for header)
        private string hpioNumber;
        private string pcehrOrganisationName;
        private string ihiNumber;

        // Formatted Ids
        private string cxFormattedPatientId;
        private string xcnFormattedAuthor;
        private string xonFormattedOrganisation;

        //private FormatCodes formatCode;

        private string formatCode;
        private string formatCodeName;

        private HealthcareFacilityTypeCodes healthcareFacilityTypeCode;
        private PracticeSettingTypes practiceSetting;
        private int size;
        private string hash;
        private string repositoryId;
        private bool isUpdateMetadata;
        private string uuidOfDocumentToReplace;

        private string Description;

        public XdsMetadata(XmlDocument cdaDocument,
                            string repositoryId,
                            string formatCode,
                            string formatCodeName,
                            HealthcareFacilityTypeCodes healthcareFacilityTypeCode,
                            PracticeSettingTypes practiceSetting,
                            int? size,
                            string hash,
                            bool isUpdateMetadata,
                            string uuidOfDocumentToReplace,
                            string documentSubTypeCode = "",
                            string documentSubTypeCodeSystem = "",
                            string documentSubTypeName = ""
                            )
        {
            this.formatCode = formatCode;
            this.formatCodeName = formatCodeName;

            this.healthcareFacilityTypeCode = healthcareFacilityTypeCode;
            this.practiceSetting = practiceSetting;
            this.isUpdateMetadata = isUpdateMetadata;

            this.uuidOfDocumentToReplace = uuidOfDocumentToReplace;

            if (isUpdateMetadata)
            {
                this.repositoryId = repositoryId;
                this.size = size.Value;
                this.hash = hash;
            }

            var xnm = new XmlNamespaceManager(cdaDocument.NameTable);
            xnm.AddNamespace("cda", "urn:hl7-org:v3");
            xnm.AddNamespace("ext", "http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0");

            // Document Class 
            documentClassCode = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:code/@code", xnm));
            var classCodeEnum = GetClassCodeEnum(documentClassCode);
            documentClassCodeSystemName = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:code/@codeSystemName", xnm));
            documentClassDisplayName = classCodeEnum.GetAttributeValue<CodedValueAttribute, string>(a => a.AlternateName);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // SUBTYPES:
            // 1) ACTS and ACSP documents have the Type code in the document, so need to update the class code to the overarching document Class
            // 2) Discharge Summary, Event Summary and Specialist Letter - the Subtype is passed into this function
            // 3) ACI - Subtypes are defined in the body - Advance Care Planning Document AND Goals of Care Document
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Type Codes
            documentTypeCode_cl07 = documentClassCode;
            documentTypeDisplayName_cl07 = documentClassDisplayName;
            documentTypeCodeSystemName_cl07 = documentClassCodeSystemName;

            // 1) For New documents that are sub typed where the code in the CDA doc is actually the typecode, need to update the class codes
            switch (documentClassCode)
            {
                // ACTS + ACSP - codeSystem names ALSO needs to be set correctly for either NCTIS or LOINC
                case "100.32044": documentClassCode = "18761-7"; classCodeEnum = GetClassCodeEnum(documentClassCode); documentClassCodeSystemName = "LOINC"; break;
                case "100.32046": documentClassCode = "80565-5"; classCodeEnum = GetClassCodeEnum(documentClassCode); documentClassCodeSystemName = "LOINC"; break;
                case "100.32049": documentClassCode = "100.32050"; classCodeEnum = GetClassCodeEnum(documentClassCode); documentClassCodeSystemName = "NCTIS Data Components"; break;
                case "100.32052": documentClassCode = "18776-5"; classCodeEnum = GetClassCodeEnum(documentClassCode); documentClassCodeSystemName = "LOINC"; break;
            }

            // 14/10 Updated Spec says we should use the AlternateName for both Type and Class Code
            documentClassCodeDisplayName = classCodeEnum.GetAttributeValue<CodedValueAttribute, string>(a => a.AlternateName);

            // 2) Discharge Summary, Event Summary and Specialist Letter can support subtypes.
            // But unlike ACTS and ACSP, these get passed into this function rather than existing in the document
            if (documentClassCode == "18842-5" || documentClassCode == "34133-9" || documentClassCode == "51852-2")
            {
                if (!string.IsNullOrWhiteSpace(documentSubTypeCode) &&
                    !string.IsNullOrWhiteSpace(documentSubTypeCodeSystem) && !string.IsNullOrWhiteSpace(documentSubTypeName))
                {
                    documentTypeCode_cl07 = documentSubTypeCode;
                    documentTypeDisplayName_cl07 = documentSubTypeName;
                    documentTypeCodeSystemName_cl07 = documentSubTypeCodeSystem;
                }
            }

            // 3) Subtypes: The exception is for Advance Care Information = which has Advance Care Planning Document/Goals of Care Document subtypes
            if (documentClassCode == "100.16975")
            {
                // Get document desc and code from the body
                documentTypeCode_cl07 = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component/cda:section/cda:entry/cda:act/cda:reference/cda:externalDocument/cda:code/@code", xnm));
                documentTypeDisplayName_cl07 = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component/cda:section/cda:entry/cda:act/cda:reference/cda:externalDocument/cda:code/@displayName", xnm));

                if (documentTypeCode_cl07 == "" || documentTypeDisplayName_cl07 == "")
                    throw new ArgumentException("The Advance Care Information does not reference an externalDocument.");
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// END OF SUBTYPES
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            
            // Get time stuff
            effectiveTime = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:effectiveTime/@value", xnm));
            effectiveTime = ConvertTimeToUtc(effectiveTime);

            // Set service start and stop time
            if (documentClassCode == "51852-2")
            {
                // If document is Specialist Letter
                serviceStartTime = effectiveTime;
                serviceStopTime = effectiveTime;
            }
            else if (documentClassCode == "100.16764")
            {
                // If document is PCEHR Prescription Record
                var authorTime = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:author/cda:time/@value", xnm));

                serviceStartTime = ConvertTimeToUtc(authorTime);
                serviceStopTime = serviceStartTime;
            } 
            else if (documentClassCode == "100.16765")
            {
                // If document is PCEHR Dispense Record
                var supplyTime = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:component/cda:structuredBody/" +
                    "cda:component/cda:section[cda:code/@code='102.16210' and cda:code/@codeSystem='1.2.36.1.2001.1001.101']/cda:entry/" +
                    "cda:substanceAdministration/cda:entryRelationship/cda:supply/cda:effectiveTime/@value", xnm));

                serviceStartTime = ConvertTimeToUtc(supplyTime);
                serviceStopTime = serviceStartTime;
            }
            else if (documentClassCode == "100.32001")
            {
                // R5: If document is Pathology Report - Could contain multiple records
                // Need to get LATEST Date - if multiple returned

                XmlNodeList nList = cdaDocument.SelectNodes("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component" +
                                                                                     "/cda:section[cda:code/@code='101.20018']/cda:component" +
                                                                                     "/cda:section[cda:code/@code='102.16144']/cda:entry/cda:observation" +
                                                                                     "/cda:entryRelationship/cda:observation[cda:code/@code='102.16156']" +
                                                                                     "/cda:effectiveTime/@value", xnm);
                var firstCollectionDateTime = "";
                var lastCollectionDateTime = "";
                if (nList.Count == 1)
                {
                    firstCollectionDateTime = nList[0].Value;
                    lastCollectionDateTime = nList[0].Value;
                }
                else if (nList.Count > 1)
                {
                    string[] sDates = new string[nList.Count];
                    for (int i = 0; i < nList.Count; i++)
			        {
                        sDates[i] = nList[i].Value;
			        }
                    Array.Sort(sDates);
                    //Get last entry
                    firstCollectionDateTime = sDates[0];
                    lastCollectionDateTime = sDates[nList.Count - 1];
                }

                serviceStartTime = ConvertTimeToUtc(firstCollectionDateTime);
                serviceStopTime = ConvertTimeToUtc(lastCollectionDateTime);
            }
            else if (documentClassCode == "100.16957")
            {
                // R5: If document is Diagnostic Imaging - Could contain multiple records
                // Need to get LATEST Date - if multiple returned
                XmlNodeList nList = cdaDocument.SelectNodes("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component" +
                                                                                  "/cda:section/cda:component/cda:section/cda:entry/cda:observation" +
                                                                                  "/cda:entryRelationship/cda:act[cda:code/@code='102.16511']/cda:entryRelationship" +
                                                                                  "/cda:observation/cda:effectiveTime/@value", xnm);
                var firstImagingDateTime = "";
                var lastImagingDateTime = "";
                if (nList.Count == 1)
                {
                    firstImagingDateTime = nList[0].Value;
                    lastImagingDateTime = nList[0].Value;
                }
                else if (nList.Count > 1)
                {
                    string[] sDates = new string[nList.Count];
                    for (int i = 0; i < nList.Count; i++)
                    {
                        sDates[i] = nList[i].Value;
                    }
                    Array.Sort(sDates);
                    //Get last entry
                    firstImagingDateTime = sDates[0];
                    lastImagingDateTime = sDates[nList.Count - 1];
                }

                serviceStartTime = ConvertTimeToUtc(firstImagingDateTime);
                serviceStopTime = ConvertTimeToUtc(lastImagingDateTime);
            }
            else if (documentClassCode == "100.16975") 
            {
                // Advance Care Planning Document
                var authorTime = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component/cda:section[cda:code/@code='101.16973']/cda:entry/cda:act/cda:author/cda:time/@value", xnm));
                serviceStartTime = ConvertTimeToUtc(authorTime);
                serviceStopTime = serviceStartTime;
            }
            else
            {
                // For other document types set service start and stop time to encompassingEncounter/effectiveTime if available
                string startTime1 = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:componentOf/cda:encompassingEncounter/cda:effectiveTime/@value", xnm));
                string startTime2 = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:componentOf/cda:encompassingEncounter/cda:effectiveTime/cda:low/@value", xnm));
                string stopTime = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:componentOf/cda:encompassingEncounter/cda:effectiveTime/cda:high/@value", xnm));

                serviceStartTime = !string.IsNullOrEmpty(startTime1)
                                       ? startTime1
                                       : !string.IsNullOrEmpty(startTime2)
                                             ? startTime2
                                             : effectiveTime;
                serviceStartTime = ConvertTimeToUtc(serviceStartTime);

                serviceStopTime = !string.IsNullOrEmpty(stopTime)
                                      ? stopTime
                                      : !string.IsNullOrEmpty(startTime1)
                                            ? startTime1
                                            : effectiveTime;

                serviceStopTime = ConvertTimeToUtc(serviceStopTime);
            }

            // As per DEXS-T123, serviceStartTime and serviceStopTime must be at least 8 chars long eg UTC formats: YYYYMMDD, YYYYMMDDhhmm, and YYYYMMDDhhmmss
            if (serviceStartTime.Length < 8)
                throw new ArgumentException("The serviceStartTime retrieved from the CDA document must be at least 8 digits long.");
            if (serviceStopTime.Length < 8)
                throw new ArgumentException("The serviceStopTime retrieved from the CDA document must be at least 8 digits long.");

            // Get Data
            languageCode = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:languageCode/@code", xnm));
            //Added as some documents do not include it. (4/2/15)
            if (String.IsNullOrEmpty(languageCode))
                languageCode = "en-AU";
            cdaTitle = CheckNullText(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:title", xnm));
            documentTitle = !string.IsNullOrEmpty(cdaTitle) ? cdaTitle : documentClassDisplayName;

            // Process document ID
            XdsMetadataHelper.IdType? idType;
            documentId = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:id/@root", xnm));
            documentIdExtension = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:id/@extension", xnm));
            cdaDocumentIdOid = XdsMetadataHelper.UuidToOid(documentId, out idType);
            if (idType == null)
                throw new ArgumentException("The CDA document must contain an ID which is either a UUID or an OID.");
            else if (idType == XdsMetadataHelper.IdType.Oid && !string.IsNullOrEmpty(documentIdExtension))
                cdaDocumentIdOid = cdaDocumentIdOid + "^" + documentIdExtension;

            XmlNode authorNode = null;

            if (cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:author/cda:assignedAuthor/cda:assignedPerson", xnm) != null)
                authorNode = cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:author/cda:assignedAuthor", xnm);
            
            // If document author doesn't exist, and document is pathology
            if (authorNode == null && documentClassCode == "100.32001")
            {
                authorNode = cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:component/cda:structuredBody/cda:component/cda:section[cda:code/@code='101.20018']/cda:author/cda:assignedAuthor", xnm);
            }

            bool authorDevice = false;
            if (cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:author/cda:assignedAuthor/cda:assignedAuthoringDevice", xnm) != null)
            {
                authorNode = authorNode = cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:author/cda:assignedAuthor", xnm);
                authorDevice = true;
            }

            if (authorNode == null && authorDevice == false)
                throw new ArgumentException("Unable to determine document author from CDA document");

            // Current Author format
            string authorOrgIdCurr = CheckNullValue(authorNode.SelectSingleNode("cda:assignedPerson/ext:asEmployment/ext:employerOrganization/cda:asOrganizationPartOf/cda:wholeOrganization/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='HPI-O']/@root", xnm));
            string authorOrgNameCurr = CheckNullText(authorNode.SelectSingleNode("cda:assignedPerson/ext:asEmployment/ext:employerOrganization/cda:asOrganizationPartOf/cda:wholeOrganization/cda:name", xnm));
            
            // Check Custodian if Author has no Org [Dont need code - as it would fail PCEHR validation for the NCAP 5 documents]
            string authorOrgIdCust = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:custodian/cda:assignedCustodian/cda:representedCustodianOrganization/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='HPI-O']/@root", xnm));
            // Check for Device (PAI-D)
            string authorOrgIdCustPaid = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:custodian/cda:assignedCustodian/cda:representedCustodianOrganization/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='PAI-O']/@root", xnm));
            string authorOrgNameCust = CheckNullText(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:custodian/cda:assignedCustodian/cda:representedCustodianOrganization/cda:name", xnm));

            // Check Health Care Facility in Component Of if Author has no Org
            string authorOrgIdHCF = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:componentOf/cda:encompassingEncounter/cda:location/cda:healthCareFacility/cda:serviceProviderOrganization/cda:asOrganizationPartOf/cda:wholeOrganization/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='HPI-O']/@root", xnm));
            string authorOrgNameHCF = CheckNullText(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:componentOf/cda:encompassingEncounter/cda:location/cda:healthCareFacility/cda:serviceProviderOrganization/cda:asOrganizationPartOf/cda:wholeOrganization/cda:name", xnm));

            authorQualifiedOrgId = (authorOrgIdCurr != "" ? authorOrgIdCurr : (authorOrgIdCust != "" ? authorOrgIdCust : (authorOrgIdHCF != "" ? authorOrgIdHCF : authorOrgIdCustPaid)));

            if (String.IsNullOrEmpty(authorOrgName))
            {
                authorOrgName = (authorOrgNameCurr != "" ? authorOrgNameCurr : (authorOrgNameCust != "" ? authorOrgNameCust : authorOrgNameHCF));
            }
                

            // AUTHOR
            authorQualifiedId = CheckNullValue(authorNode.SelectSingleNode("cda:assignedPerson/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='HPI-I']/@root", xnm));
            if (string.IsNullOrEmpty(authorQualifiedId))
                authorQualifiedId = CheckNullValue(authorNode.SelectSingleNode("cda:assignedAuthoringDevice/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='PAI-D']/@root", xnm));
            if (string.IsNullOrEmpty(authorQualifiedId))
                authorQualifiedId = CheckNullValue(cdaDocument.SelectSingleNode("cda:assignedPerson/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='PAI-D']/@root", xnm));
            if (string.IsNullOrEmpty(authorQualifiedId))
            {
                authorQualifiedId = CheckNullValue(authorNode.SelectSingleNode("cda:assignedPerson/ext:asEntityIdentifier[@classCode='IDENT']/ext:id/@root", xnm));
                authorQualifiedIdExtension = CheckNullValue(authorNode.SelectSingleNode("cda:assignedPerson/ext:asEntityIdentifier[@classCode='IDENT']/ext:id/@extension", xnm));
            }
            if (string.IsNullOrEmpty(authorQualifiedId))
                throw new ArgumentException("The CDA document must contain an author identifier.");
            if (string.IsNullOrEmpty(authorFamily))
                authorFamily = CheckNullText(authorNode.SelectSingleNode("cda:assignedPerson/cda:name/cda:family", xnm));
            authorGiven = CheckNullText(authorNode.SelectSingleNode("cda:assignedPerson/cda:name/cda:given", xnm));
            authorPrefix = CheckNullText(authorNode.SelectSingleNode("cda:assignedPerson/cda:name/cda:prefix", xnm));
            authorSuffix = CheckNullText(authorNode.SelectSingleNode("cda:assignedPerson/cda:name/cda:suffix", xnm));

            // See if the author is a device
            if (string.IsNullOrWhiteSpace(authorGiven))
            {
                authorGiven = CheckNullText(authorNode.SelectSingleNode("cda:assignedAuthoringDevice/cda:softwareName", xnm));
            }

            // Author specialty - check if original text exists
            authorSpecialty = CheckNullValue(authorNode.SelectSingleNode("cda:code/@displayName", xnm));
            if (string.IsNullOrWhiteSpace(authorSpecialty))
            {
                authorSpecialty = CheckNullText(authorNode.SelectSingleNode("cda:code/cda:originalText", xnm));
            }

            // HPIO number (for header) Either HPIO or PAI-O
            hpioNumber = authorQualifiedOrgId.Replace("1.2.36.1.2001.1003.0.", "");
            hpioNumber = authorQualifiedOrgId.Replace("1.2.36.1.2001.1007.0.", "");
            // IHI number (for header)
            ihiNumber = CheckNullValue(cdaDocument.SelectSingleNode("/cda:ClinicalDocument/cda:recordTarget/cda:patientRole/cda:patient/ext:asEntityIdentifier[@classCode='IDENT']/ext:id[@assigningAuthorityName='IHI']/@root", xnm)).Replace("1.2.36.1.2001.1003.0.", "");

            // Formatted Ids
            cxFormattedPatientId = ihiNumber + "^^^&1.2.36.1.2001.1003.0&ISO";

            // Correct format (3 hats before HPII)
            if (string.IsNullOrEmpty(authorQualifiedIdExtension))
                xcnFormattedAuthor = string.Format("^{0}^{1}^^^{2}^^^&{3}&ISO", authorFamily, authorGiven, authorPrefix, authorQualifiedId);
            else
                xcnFormattedAuthor = string.Format("^{0}^{1}^^^{2}^^^{3}&{4}&ISO", authorFamily, authorGiven, authorPrefix, authorQualifiedId, authorQualifiedIdExtension);

            xonFormattedOrganisation = string.Format("{0}^^^^^^^^^{1}", authorOrgName, authorQualifiedOrgId);
        }

        internal string ConvertTimeToUtc(string effectiveTime)
        {
            if (!string.IsNullOrEmpty(effectiveTime))
            {
                try
                {
                    return XdsMetadataHelper.GetUtcTime(effectiveTime);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error parsing effective time in CDA document: " + ex.Message);
                }
            }
            else
                return "";
        }

        public SubmitObjectsRequest CreateSubmitObjectsRequest()
        {
            var request = new SubmitObjectsRequest();

            var identifiables = new List<IdentifiableType>();

            // Create metadata for root
            var extrinsicObject = CreateExtrinsicObjectType();

            // Create metadata for submission set / package
            var registryPackage = CreateRegistryPackageType();

            // Create classification for submission set / package
            var classification = CreateClassificationType(
                "cl10",                
                XDS_SUBMISSION_SET,
                SubmissionSetId);

            var associations = new List<AssociationType1>();

            // Add associations of files to submission set
            var association = CreateAssociationType(
                "as01",         
                XDS_ASSOCIATION_TYPE_HAS_MEMBER,
                SubmissionSetId,
                DocumentId,
                new SlotType1[]
                    {
                        CreateSlotType("SubmissionSetStatus", "Original")
                    },
                null
                );
            associations.Add(association);

            // If replacement document UUID is set, then create replacement association;
            if (!string.IsNullOrEmpty(uuidOfDocumentToReplace))
            {
                association = CreateAssociationType(
                    "as02",                    
                    XDS_ASSOCIATION_TYPE_RPLC,
                    DocumentId,
                    uuidOfDocumentToReplace,
                    null,
                    "Replace Document"
                    );
                associations.Add(association);
            }

            request.RegistryObjectList = new RegistryObjectListType();
            request.RegistryObjectList.Association = associations.ToArray();
            request.RegistryObjectList.Classification = new ClassificationType[] { classification };
            request.RegistryObjectList.ExtrinsicObject = new ExtrinsicObjectType[] { extrinsicObject };
            request.RegistryObjectList.RegistryPackage = new RegistryPackageType[] { registryPackage };

            var requestXml = request.SerializeToXml();

            return request;
        }

        /// <summary>
        /// Creates a RegistryPackageType from a CDAPackage.
        /// </summary>
        /// <param name="package">The CDAPackage instance.</param>
        /// <returns>The constructed RegistryPackageType.</returns>
        internal RegistryPackageType CreateRegistryPackageType()
        {
            var registryPackage = new RegistryPackageType();
            var classifications = new List<ClassificationType>();
            var externalIdentifiers = new List<ExternalIdentifierType>();

            // Name
            registryPackage.Name = CreateInternationalStringType(documentClassDisplayName);

            // Type
            registryPackage.objectType = XDS_REGISTRY_PACKAGE;

            // ID
            registryPackage.id = SubmissionSetId;

            // SubmissionTime
            registryPackage.Slot = new SlotType1[1] { 
                CreateSlotType("submissionTime", DateTime.Now.ToUniversalTime().ToString(DateTimeFormatString)) 
            };

            //registryPackage.Name = CreateInternationalStringType(documentClassDisplayName);

            // Author - 4 components - Only populate author institution, author person (not role and specialty)
            var authorSlots = new List<SlotType1>();
            authorSlots.Add(CreateSlotType("authorInstitution", xonFormattedOrganisation));
            authorSlots.Add(CreateSlotType("authorPerson", xcnFormattedAuthor));
            // 24/01/23 Defect - should be in both authorSpecialty and authorRole
            if (!String.IsNullOrEmpty(authorSpecialty))
                authorSlots.Add(CreateSlotType("authorSpecialty", authorSpecialty));
            if (!String.IsNullOrEmpty(authorSpecialty))
                authorSlots.Add(CreateSlotType("authorRole", authorSpecialty));

            ClassificationType authorClassification = CreateClassificationType(
                "cl08",                
                XDS_SUBMISSION_SET_AUTHOR,
                registryPackage.id,
                "",
                authorSlots,
                null);
            classifications.Add(authorClassification);

            // Content type
            classifications.Add(
                CreateCodedValueClassification(
                    "cl09",
                    registryPackage.id,
                    documentTypeCode_cl07,
                    documentTypeDisplayName_cl07,
                    documentTypeCodeSystemName_cl07,
                    XDS_SUBMISSION_SET_CONTENT_TYPE_CODE
                )
            );

            // UniqueId
            externalIdentifiers.Add(
                CreateExternalIdentifierType(
                    "ei03",
                    XDS_SUBMISSION_SET_UNIQUE_ID,
                    cdaDocumentIdOid,
                    registryPackage.id,
                    "XDSSubmissionSet.uniqueId"
                    )
                );

            // SourceId
            externalIdentifiers.Add(
                CreateExternalIdentifierType(
                    "ei04",
                    XDS_SUBMISSION_SET_SOURCE_ID,
                    authorQualifiedOrgId,
                    registryPackage.id,
                    "XDSSubmissionSet.sourceId"
                    )
                );

            // PatientId
            externalIdentifiers.Add(
                CreateExternalIdentifierType(
                    "ei05", 
                    XDS_SUBMISSION_SET_PATIENT_ID,
                    cxFormattedPatientId,
                    registryPackage.id,
                    "XDSSubmissionSet.patientId"
                    )
                );

            registryPackage.Classification = classifications.ToArray();
            registryPackage.ExternalIdentifier = externalIdentifiers.ToArray();

            return registryPackage;
        }

        /// <summary>
        /// Creates an ExtrinsicObjectType from a CDAPackageFile.
        /// </summary>
        /// <param name="packageFile">The CDAPackageFile instance.</param>
        /// <returns>The constructed ExtrinsicObjectType.</returns>
        internal ExtrinsicObjectType CreateExtrinsicObjectType()
        {
            var extrinsicObject = new ExtrinsicObjectType();
            var classifications = new List<ClassificationType>();
            var externalIdentifiers = new List<ExternalIdentifierType>();

            extrinsicObject.id = DocumentId;
            extrinsicObject.mimeType = "application/zip";
            extrinsicObject.objectType = XDS_DOCUMENT_ENTRY;
            extrinsicObject.status = DocumentStatus.Approved.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);

            // General slots
            var generalSlots = new List<SlotType1>();
            generalSlots.Add(CreateSlotType("creationTime", effectiveTime));
            generalSlots.Add(CreateSlotType("languageCode", languageCode));
            if (!string.IsNullOrEmpty(serviceStartTime)) generalSlots.Add(CreateSlotType("serviceStartTime", serviceStartTime));
            if (!string.IsNullOrEmpty(serviceStopTime)) generalSlots.Add(CreateSlotType("serviceStopTime", serviceStopTime));
            generalSlots.Add(CreateSlotType("sourcePatientId", cxFormattedPatientId));

            if (isUpdateMetadata)
            {
                generalSlots.Add(CreateSlotType("hash", hash));
                generalSlots.Add(CreateSlotType("size", size.ToString()));
                generalSlots.Add(CreateSlotType("repositoryUniqueId", repositoryId));
            }

            // Name
            extrinsicObject.Name = CreateInternationalStringType(documentTitle);
            if (!String.IsNullOrEmpty(Description))
                extrinsicObject.Description = CreateInternationalStringType(Description);

            // Author - 4 components - Only populate author institution, author person (not role and specialty)
            var authorSlots = new List<SlotType1>();
            authorSlots.Add(CreateSlotType("authorInstitution", xonFormattedOrganisation));
            authorSlots.Add(CreateSlotType("authorPerson", xcnFormattedAuthor));
            // 26/07/24 Added in to align with submission set - should be in both authorSpecialty and authorRole
            if (!String.IsNullOrEmpty(authorSpecialty))
                authorSlots.Add(CreateSlotType("authorSpecialty", authorSpecialty));
            if (!String.IsNullOrEmpty(authorSpecialty))
                authorSlots.Add(CreateSlotType("authorRole", authorSpecialty));


            ClassificationType authorClassification = CreateClassificationType(
                "cl01",
                XDS_DOCUMENT_ENTRY_AUTHOR,
                extrinsicObject.id,
                "",
                authorSlots,
                null);
            classifications.Add(authorClassification);

            // ClassCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl02",
                    extrinsicObject.id,
                    documentClassCode,
                    documentClassCodeDisplayName,
                    documentClassCodeSystemName,     
                    XDS_DOCUMENT_ENTRY_CLASS_CODE
                    )
                );

            // ConfidentialityCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl03",
                    extrinsicObject.id,
                    "GENERAL",
                    "NA",
                    "PCEHR_DocAccessLevels",  
                    XDS_DOCUMENT_ENTRY_CONFIDENTIALITY_CODE
                    )
                );

            // FormatCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl04", 
                    extrinsicObject.id,
                    formatCode,
                    formatCodeName,
                    formatCodeCodingScheme,
                    XDS_DOCUMENT_ENTRY_FORMAT_CODE
                    )
                );

            // HealthcareFacilityTypeCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl05",
                    extrinsicObject.id,
                    healthcareFacilityTypeCode,
                    XDS_DOCUMENT_ENTRY_HEALTHCARE_FACILITY_TYPE_CODE
                    )
                );

            // PracticeSettingCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl06",
                    extrinsicObject.id,
                    practiceSetting,
                    XDS_DOCUMENT_ENTRY_PRACTICE_SETTING_CODE
                    )
                );

            // TypeCode
            classifications.Add(
                CreateCodedValueClassification(
                    "cl07",
                    extrinsicObject.id,
                    documentTypeCode_cl07,
                    documentTypeDisplayName_cl07,
                    documentTypeCodeSystemName_cl07,
                    XDS_DOCUMENT_ENTRY_TYPE_CODE
                    )
                );

            // PatientId
            externalIdentifiers.Add(
                CreateExternalIdentifierType(
                    "ei01",
                    XDS_DOCUMENT_ENTRY_PATIENT_ID,
                    cxFormattedPatientId,
                    extrinsicObject.id,
                    "XDSDocumentEntry.patientId"
                    )
                );

            // UniqueId
            externalIdentifiers.Add(
                CreateExternalIdentifierType(
                    "ei02",                
                    XDS_DOCUMENT_ENTRY_UNIQUE_ID,
                    cdaDocumentIdOid,
                    extrinsicObject.id,
                    "XDSDocumentEntry.uniqueId"
                    )
                );

            extrinsicObject.Slot = generalSlots.ToArray();
            extrinsicObject.Classification = classifications.ToArray();
            extrinsicObject.ExternalIdentifier = externalIdentifiers.ToArray();

            return extrinsicObject;
        }

        /// <summary>
        /// Creates a ClassificationType from a coded enum value.
        /// </summary>
        /// <param name="extrinsicObjectId">The Id of the ExtrinsicObject that this ClassificationType describes.</param>
        /// <param name="codedEnumValue">The coded enum value (applied using CodedValueAttribute)</param>
        /// <param name="xdsTypeUuid">The XDS classification UUID.</param>
        /// <returns>The constructed ClassificationType.</returns>
        internal ClassificationType CreateCodedValueClassification(
            string id,
            string extrinsicObjectId,
            Enum codedEnumValue,
            string xdsTypeUuid)
        {
            var slots = new List<SlotType1>();
            slots.Add(CreateSlotType("codingScheme", codedEnumValue.GetAttributeValue<CodedValueAttribute, string>(a => a.CodingSystem)));
            var classification = CreateClassificationType(
                id,                
                xdsTypeUuid,
                extrinsicObjectId,
                codedEnumValue.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode),
                slots,
                CreateInternationalStringType(codedEnumValue.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptName))
                );

            return classification;
        }

        /// <summary>
        /// Creates a ClassificationType from a coded enum value.
        /// </summary>
        /// <param name="extrinsicObjectId">The Id of the ExtrinsicObject that this ClassificationType describes.</param>
        /// <param name="codedEnumValue">The coded enum value (applied using CodedValueAttribute)</param>
        /// <param name="xdsTypeUUID">The XDS classification UUID.</param>
        /// <returns>The constructed ClassificationType.</returns>
        internal ClassificationType CreateCodedValueClassification(
            string id,
            string extrinsicObjectId,
            string code,
            string conceptName,
            string codingScheme,
            string xdsTypeUUID)
        {
            var slots = new List<SlotType1>();
            slots.Add(CreateSlotType("codingScheme", codingScheme));
            ClassificationType classification = CreateClassificationType(
                id,                
                xdsTypeUUID,
                extrinsicObjectId,
                code,
                slots,
                CreateInternationalStringType(conceptName)
                );

            return classification;
        }

        /// <summary>
        /// Creates a ClassificationType.
        /// </summary>
        /// <param name="classificationScheme">The classification scheme.</param>
        /// <param name="classifiedObject">The classified object UUID.</param>
        /// <param name="nodeRepresentation">The node representation.</param>
        /// <param name="slots">The SlotType1 entries in the classification.</param>
        /// <param name="name">The name of the classiciation.</param>
        /// <returns>ClassificationType.</returns>
        internal ClassificationType CreateClassificationType(
            string id,
            string classificationScheme,
            string classifiedObject,
            string nodeRepresentation,
            List<SlotType1> slots,
            InternationalStringType name)
        {
            var classification = new ClassificationType();
            classification.classificationScheme = classificationScheme;
            classification.classifiedObject = classifiedObject;
            classification.nodeRepresentation = nodeRepresentation;
            classification.objectType = XDS_CLASSIFICATION;
            classification.id = id;

            if (slots != null && slots.Count > 0)
            {
                classification.Slot = slots.ToArray();
            }

            if (name != null)
            {
                classification.Name = name;
            }

            return classification;
        }

        /// <summary>
        /// Creates a ClassificationType.
        /// </summary>
        /// <param name="classificationNode">The classification node.</param>
        /// <param name="classifiedObject">The classified object UUID.</param>
        /// <returns>ClassificationType.</returns>
        internal ClassificationType CreateClassificationType(
            string id,
            string classificationNode,
            string classifiedObject)
        {
            var classification = new ClassificationType();
            classification.classificationNode = classificationNode;
            classification.classifiedObject = classifiedObject;
            classification.objectType = XDS_CLASSIFICATION;
            classification.id = id;

            return classification;
        }

        /// <summary>
        /// Creates a Slot.
        /// </summary>
        /// <param name="name">The name of the Slot.</param>
        /// <param name="value">The value of the Slot.</param>
        /// <returns>SlotType1.</returns>
        internal SlotType1 CreateSlotType(string name, string value)
        {
            var slot = new SlotType1();
            slot.name = name;
            slot.ValueList = new ValueListType();
            slot.ValueList.Value = new string[] { value };
            return slot;
        }

        /// <summary>
        /// Creates an InternationalStringType.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns>InternationalStringType.</returns>
        internal InternationalStringType CreateInternationalStringType(string stringValue)
        {
            var stringType = new InternationalStringType();
            stringType.LocalizedString = new LocalizedStringType[] {
                new LocalizedStringType() {
                    value = stringValue
                }
            };
            return stringType;
        }

        /// <summary>
        /// Creates an ExternalIdentifierType.
        /// </summary>
        /// <param name="identificationScheme">The identification scheme.</param>
        /// <param name="value">The value.</param>
        /// <param name="registryObject">The registry object that is described.</param>
        /// <param name="name">The name of the ExternalIdentifier.</param>
        /// <returns>ExternalIdentifierType.</returns>
        internal ExternalIdentifierType CreateExternalIdentifierType(
            string id,
            string identificationScheme,
            string value,
            string registryObject,
            string name)
        {
            var identifier = new ExternalIdentifierType();
            identifier.identificationScheme = identificationScheme;
            identifier.value = value;
            identifier.objectType = XDS_EXTERNAL_IDENTIFIER;
            identifier.registryObject = registryObject;
            identifier.id = id;

            if (name != null)
                identifier.Name = CreateInternationalStringType(name);

            return identifier;
        }

        /// <summary>
        /// Creates an AssociationType1.
        /// </summary>
        /// <param name="associationType">Type of the association.</param>
        /// <param name="sourceObject">The source object UUID.</param>
        /// <param name="targetObject">The target object UUID.</param>
        /// <param name="slot">The slot to include.</param>
        /// <returns>AssociationType1.</returns>
        internal AssociationType1 CreateAssociationType(
            string id,
            string associationType,
            string sourceObject,
            string targetObject,
            SlotType1[] slots,
            string name)
        {
            var association = new AssociationType1();
            association.associationType = associationType;
            association.sourceObject = sourceObject;
            association.targetObject = targetObject;
            association.id = id;
            association.objectType = XDS_ASSOCIATION;

            if (slots != null && slots.Length > 0)
                association.Slot = slots;

            if (!string.IsNullOrEmpty(name))
                association.Name = CreateInternationalStringType(name);

            return association;
        }
        
        internal static string CheckNullText(XmlNode node)
        {
            return ((node != null ? node.InnerText : ""));
        }

        internal static string CheckNullValue(XmlNode node)
        {
            return ((node != null ? node.Value : ""));
        }

        internal static ClassCodes GetClassCodeEnum(string value)
        {
            var enums = Enum.GetValues(typeof (ClassCodes));

            foreach (ClassCodes e in enums)
            {
                var code = e.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);
                if (code == value)
                    return e;
            }

            throw new ArgumentException("Cannot find ClassCode with value of " + value);
        }
    }
}