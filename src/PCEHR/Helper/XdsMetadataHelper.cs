/*
 * Copyright 2018 Australian Digital Health Agency
 *
 * Licensed under the Australian Digital Health Agency Open Source (Apache) License; 
 * you may not use this file except in compliance with the License. A copy of the 
 * License is in the 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nehta.VendorLibrary.PCEHR
{
    public static class XdsMetadataHelper
    {
        // XDS fixed GUIDs
        private const string XDS_DOCUMENT_ENTRY_AUTHOR = "urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d";                 // 	 	External Classification Scheme 
        private const string XDS_DOCUMENT_ENTRY_CLASS_CODE = "urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a";              // 	 	External Classification Scheme 
        private const string XDS_DOCUMENT_ENTRY_FORMAT_CODE = "urn:uuid:a09d5840-386c-46f2-b5ad-9c3699a4309d";             // 	 	External Classification Scheme 	
        private const string XDS_DOCUMENT_ENTRY_HEALTHCARE_FACILITY_TYPE_CODE = "urn:uuid:f33fb8ac-18af-42cc-ae0e-ed0b0bdb91e1"; // 	External Classification Scheme 	
        private const string XDS_DOCUMENT_ENTRY_PRACTICE_SETTING_CODE = "urn:uuid:cccf5598-8b07-4b77-a05e-ae952c785ead";    // 	 	External Classification Scheme 	
        private const string XDS_DOCUMENT_ENTRY_TYPE_CODE = "urn:uuid:f0306f51-975f-434e-a61c-c59651d33983";               // 	 	External Classification Scheme 
        private const string XDS_DOCUMENT_ENTRY_UNIQUE_ID = "urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab";               // 	 	ExternalIdentifier 

        public enum IdType
        {
            Uuid,
            Oid
        }

        public static string UuidToOid(string uuid)
        {
            IdType? idType;
            return UuidToOid(uuid, out idType);
        }       

        public static string GetUtcTime(string timestring)
        {
            var formatTemplate = "yyyyMMddHHmmss.ffff";

            var format = string.Empty;
            var timezoneFormat = string.Empty;

            int plusMinusIndex = timestring.IndexOf("+");
            if (plusMinusIndex < 0) plusMinusIndex = timestring.IndexOf("-");

            if (plusMinusIndex >= 10)
            {
                format = formatTemplate.Substring(0, plusMinusIndex);
                int timezoneLength = timestring.Substring(plusMinusIndex + 1).Length;

                if (timezoneLength != 2 && timezoneLength != 4)
                    throw new FormatException("Timezone must be specified with either 2 or 4 digits.");

                timezoneFormat = "zzzz".Substring(0, timezoneLength);
                var equivalent = DateTime.ParseExact(timestring, format + timezoneFormat, CultureInfo.InvariantCulture);

                // Get output format
                string outputFormat = format;
                if (timezoneLength == 4 && !timestring.EndsWith("00") && format.Length < 12)
                    outputFormat = formatTemplate.Substring(0, 12);
                if (outputFormat.Length > 14)
                    outputFormat = outputFormat.Substring(0, 14);

                return equivalent.ToUniversalTime().ToString(outputFormat);
            }
            else if (plusMinusIndex < 10 && plusMinusIndex > -1)
            {
                return timestring.Substring(0, plusMinusIndex);
            }
            else 
            {
                if (timestring.Length > 14)
                    return timestring.Substring(0, 14);
                else
                    return timestring;
            }
        }

        public static string UuidToOid(string uuid, out IdType? idType)
        {
            idType = null;

            // Example
            // UUID = a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1
            // OID = 2.25.N
            // Where N =  (2^96 * 0x a7b7c3b7) + (2^64 * 0x 463943a9) + (2^32 * 0x 8bb17cb8)) + 0x c91216c1 
            // Correct value  2.25.222935235211552455402395562399683974849

            string answer = uuid;

            // 0 start pos  01234567 9012 4567 9012 456789012345  for SubString
            //string uuid = "a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1";
            // Remove unwanted chars if they exist
            uuid = uuid.Replace("urn:uuid:", "");

            if (Regex.IsMatch(uuid, "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.IgnoreCase))
            {
                idType = IdType.Uuid;
                uuid = uuid.Replace("-", "");

                //Convert hex (16) to decimal
                //Add "0" to beginning to make sure positive number returned
                answer = "2.25." + System.Numerics.BigInteger.Parse("0" + uuid, NumberStyles.AllowHexSpecifier);
            }
            else if (Regex.IsMatch(uuid, "^[0-9]+(\\.[0-9]+)+$"))
            {
                idType = IdType.Oid;
            }

            return answer.ToString();
        }
        
        public static XdsRecord[] ProcessXdsMetadata(ExtrinsicObjectType[] data)
        {
            List<XdsRecord> xds = new List<XdsRecord>();
            int i = 0;

            foreach (var row in data)
            {
                XdsRecord record = new XdsRecord();
                i++;
                string log = "";

                try
                {
                    //Internal local record identifier
                    record.lid = row.lid;
                    record.status = row.status;
                    record.docWithdrawn = getTextFromSlot(row.Slot, "urn:nehta:xds:metadata:docWithdrawn") == "true";
                    record.removeReason = getTextFromSlot(row.Slot, "urn:pcehr:ihe:xds:metadata:removeReason");
                    record.creationTimeUTC = ConvertDateTime(getTextFromSlot(row.Slot, "creationTime"));
                    record.languageCode = getTextFromSlot(row.Slot, "languageCode");
                    record.serviceStartTimeUTC = ConvertDateTime(getTextFromSlot(row.Slot, "serviceStartTime"));
                    record.serviceStopTimeUTC = ConvertDateTime(getTextFromSlot(row.Slot, "serviceStopTime"));
                    record.ihiNumber = getTextFromSlot(row.Slot, "sourcePatientId").Substring(0, 16);
                    record.hash = getTextFromSlot(row.Slot, "hash");
                    record.size = getTextFromSlot(row.Slot, "size");
                    record.repositoryUniqueId = getTextFromSlot(row.Slot, "repositoryUniqueId");
                    // For MedicareView and MedicinesView this field seems to be omitted
                    record.recordVersion = (row.VersionInfo != null ? row.VersionInfo.versionName : "1");

                    // Variations to add
                    ClassificationType author = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_AUTHOR);
                    string[] authorInstitution = getTextFromSlot(author.Slot, "authorInstitution").Split('^');
                    record.authorInstitution = new XdsAuthorInstitution();
                    record.authorInstitution.institutionIdentifier = (authorInstitution.Length > 9 ? authorInstitution[9] : "");
                    record.authorInstitution.institutionName = (authorInstitution.Length > 0 ? authorInstitution[0] : "");
                    log = getTextFromSlot(author.Slot, "authorPerson");
                    string[] authorPerson = getTextFromSlot(author.Slot, "authorPerson").Split('^');
                    record.authorPerson = new XdsAuthorPerson();
                    record.authorPerson.authorPrefix = (authorPerson.Length > 5 ? authorPerson[5] : "");
                    record.authorPerson.authorGivenName = (authorPerson.Length > 2 ? authorPerson[2] : "");
                    record.authorPerson.authorFamilyName = (authorPerson.Length > 1 ? authorPerson[1] : "");

                    // There is an issue with the location of the identifier with position 7, 9 & 10 seem to have been used by different
                    // document types. The correct location is 9.
                    // 1  2      3     4          5      6      7      8            9              10
                    // Id^Family^Given^SecondName^Suffix^Prefix^Degree^Source Table^Name Type Code^Identifier Digit Scheme
                    // Just going to use last field
                    string[] authorId =  authorPerson.Last().Split('&') ;
                    if (authorId.Length > 1)
                    {
                        // if "&qid&ISO" then id is the qid.   If "qid&id&ISO" then id is the id.
                        record.authorPerson.authorQualifiedIdentifier = (authorId[0] != "" ? authorId[0] : authorId[1]);
                        record.authorPerson.authorQualifiedIdentifierExtension = (authorId[0] != "" ? authorId[1] : "");
                    }
                    record.authorSpeciality = getTextFromSlot(author.Slot, "authorSpecialty");

                    ClassificationType classCode = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_CLASS_CODE);
                    record.classCode = classCode.nodeRepresentation;
                    record.classCodeDisplayName = getValueFromName(classCode);

                    ClassificationType formatCode = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_FORMAT_CODE);
                    record.formatCode = formatCode.nodeRepresentation;
                    record.formatCodeDisplayName = getValueFromName(formatCode);

                    ClassificationType healthcareFacilityCode = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_HEALTHCARE_FACILITY_TYPE_CODE);
                    record.healthcareFacilityTypeCode = healthcareFacilityCode.nodeRepresentation;
                    record.healthcareFacilityTypeCodeDisplayName = getValueFromName(healthcareFacilityCode);

                    ClassificationType practiceCode = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_PRACTICE_SETTING_CODE);
                    record.practiceSettingCode = practiceCode.nodeRepresentation;
                    record.practiceSettingCodeDisplayName = getValueFromName(practiceCode);

                    ClassificationType typeCode = getClassification(row.Classification, XDS_DOCUMENT_ENTRY_TYPE_CODE);
                    record.typeCode = typeCode.nodeRepresentation;
                    record.typeCodeDisplayName = getValueFromName(typeCode);

                    record.documentId = getValueFromIdentifier(row.ExternalIdentifier, XDS_DOCUMENT_ENTRY_UNIQUE_ID);

                    xds.Add(record);

                }
                catch (Exception e)
                {
                    // To stop the app blowing up if any XDS data is dodgy
                    continue;
                }

                //For Debug purposes only
                if (false)
                {
                    //Header
                    if (i == 1)
                    {
                        foreach (var prop in record.GetType().GetProperties())
                        {
                            if (prop.Name == "authorInstitution")
                                File.AppendAllText(@"d:\author.txt", "institutionName\tinstitutionIdentifier\t");
                            else if (prop.Name == "authorPerson")
                                File.AppendAllText(@"d:\author.txt", "authorPrefix\tauthorGivenName\tauthorFamilyName\tauthorQualifiedIdentifier\tauthorQualifiedIdentifierExtension\t");
                            else
                                File.AppendAllText(@"d:\author.txt", prop.Name + "\t");
                        }
                        File.AppendAllText(@"d:\author.txt", Environment.NewLine);


                        // Data
                        foreach (var prop in record.GetType().GetProperties())
                        {
                            if (prop.Name == "authorInstitution")
                                File.AppendAllText(@"d:\author.txt", record.authorInstitution.institutionName + "\t" + record.authorInstitution.institutionIdentifier + "\t");
                            else if (prop.Name == "authorPerson")
                                File.AppendAllText(@"d:\author.txt", record.authorPerson.authorPrefix + "\t" + record.authorPerson.authorGivenName + "\t" + record.authorPerson.authorFamilyName + "\t" + record.authorPerson.authorQualifiedIdentifier + "\t" + record.authorPerson.authorQualifiedIdentifierExtension + "\t");
                            else if (prop.Name == "ihiNumber")
                                File.AppendAllText(@"d:\author.txt", "'" + prop.GetValue(record, null) + "\t");
                            else
                                File.AppendAllText(@"d:\author.txt", prop.GetValue(record, null) + "\t");
                        }
                        File.AppendAllText(@"d:\author.txt", Environment.NewLine);
                    }
                }
            }

            return xds.ToArray();
        }

        private static string getTextFromSlot(SlotType1[] slots, string slotName)
        {
            string rv = "";
            foreach (var slot in slots)
            {
                if (slot.name == slotName)
                {
                    rv = slot.ValueList.Value[0];
                    break;
                }
            }
            return rv;
        }

        private static string getValueFromName(ClassificationType ct)
        {
            string rv = "";
            if (ct.Name != null && ct.Name.LocalizedString.Length > 0)
                rv = ct.Name.LocalizedString[0].value;
            return (rv);
        }

        private static string getValueFromIdentifier(ExternalIdentifierType[] ctypes, string classScheme)
        {
            string rv = "";
            foreach (var ctype in ctypes)
            {
                if (ctype.identificationScheme == classScheme)
                {
                    rv = ctype.value;
                    break;
                }
            }
            return rv;
        }

        private static ClassificationType getClassification(ClassificationType[] ctypes, string classScheme)
        {
            ClassificationType rv = new ClassificationType();
            foreach (var ctype in ctypes)
            {
                if (ctype.classificationScheme == classScheme)
                {
                    rv = ctype;
                    break;
                }
            }
            return rv;
        }

        private static DateTime ConvertDateTime(string date1)
        {
            DateTime dt;
            switch (date1.Length)
            {
                case 4:
                    dt = DateTime.ParseExact(date1, "yyyy", CultureInfo.CurrentCulture);
                    break;
                case 6:
                    dt = DateTime.ParseExact(date1, "yyyyMM", CultureInfo.CurrentCulture);
                    break;
                case 8:
                    dt = DateTime.ParseExact(date1, "yyyyMMdd", CultureInfo.CurrentCulture);
                    break;
                case 12:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmm", CultureInfo.CurrentCulture);
                    break;
                case 14:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
                    break;
                case 17:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmzzzz", CultureInfo.CurrentCulture);
                    break;
                case 19:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmsszzzz", CultureInfo.CurrentCulture);
                    break;
                case 21:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.fzzzz", CultureInfo.CurrentCulture);
                    break;
                case 22:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.ffzzzz", CultureInfo.CurrentCulture);
                    break;
                case 23:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.fffzzzz", CultureInfo.CurrentCulture);
                    break;
                case 24:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.ffffzzzz", CultureInfo.CurrentCulture);
                    break;
                case 25:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.fffffzzzz", CultureInfo.CurrentCulture);
                    break;
                case 26:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.ffffffzzzz", CultureInfo.CurrentCulture);
                    break;
                case 27:
                    dt = DateTime.ParseExact(date1, "yyyyMMddHHmmss.fffffffzzzz", CultureInfo.CurrentCulture);
                    break;
                default:
                    dt = DateTime.ParseExact("18000101", "yyyyMMdd", CultureInfo.CurrentCulture);
                    break;
            }

            return dt;
        }


    }
}
