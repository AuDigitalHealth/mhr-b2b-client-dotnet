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
using System.Linq;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Class responsible for building up an adhoc query for use with the 'GetDocumentList' service.
    /// </summary>
    public class AdhocQueryBuilder
    {
        /// <summary>
        /// UUID of the XDS find document list operation.
        /// </summary>
        private const string XdsFindDocumentList = "urn:uuid:14d4debf-8f97-4251-9a74-a90016b0af0d";

        private const string XdsGetSingleDocumentList = "urn:uuid:5c4f972b-d56b-40ac-a5fc-c8ca9b40b9d4";

        public string Ihi { get; private set; }

        public IList<DocumentStatus> Status { get; private set; }

        public IList<ClassCodes> ClassCode { get; set; }

        public IList<ClassCodes> TypeCode { get; set; }

        // Use own sub type code as opposed to ENUMs
        public IList<Tuple<string, string>> SubTypeCode { get; set; }

        public IList<PracticeSettingTypes> PracticeSettingCode { get; set; }

        public IList<HealthcareFacilityTypeCodes> HealthcareFacilityTypeCode { get; set; }

        public IList<Author> AuthorPerson { get; set; }

        public IList<PcehrFormatCode> FormatCode { get; set; }

        public ISO8601DateTime CreationTimeFrom { get; set; }

        public ISO8601DateTime CreationTimeTo { get; set; }

        public ISO8601DateTime ServiceStartTimeFrom { get; set; }

        public ISO8601DateTime ServiceStartTimeTo { get; set; }

        public ISO8601DateTime ServiceStopTimeFrom { get; set; }

        public ISO8601DateTime ServiceStopTimeTo { get; set; }

        public string DocumentUniqueId { get; set; }

        /// <summary>
        /// Constructor that takes an IHI and sets the document status to approved.
        /// </summary>
        /// <param name="ihi">IHI.</param>
        public AdhocQueryBuilder(string ihi)
        {
            Validation.ValidateStringLength("ihi", ihi, 16, true);

            Ihi = ihi;
            Status = new List<DocumentStatus> { DocumentStatus.Approved };
        }

        /// <summary>
        /// Constructor that takes an IHI and a document status.
        /// </summary>
        /// <param name="ihi">IHI.</param>
        /// <param name="status">Document status.</param>
        public AdhocQueryBuilder(string ihi, IList<DocumentStatus> status)
        {
            Validation.ValidateStringLength("ihi", ihi, 16, true);
            Validation.ValidateArgumentRequired("status", status);
            
            Ihi = ihi;
            Status = status;
        }

        /// <summary>
        /// Builds an adhoc query.
        /// </summary>
        /// <returns>Adhoc query.</returns>
        public AdhocQueryType Build()
        {
            // Document entry unique ID
            if (!string.IsNullOrEmpty(DocumentUniqueId))
            {
                return new AdhocQueryType()
                {
                    id = XdsGetSingleDocumentList,
                    Slot = new SlotType1[]
                    {
                        CreateDocumentEntryUniqueIdSlot(DocumentUniqueId)
                    }
                };
            }
            else
            {
                IList<SlotType1> slots = new List<SlotType1>();

                // Ihi
                slots.Add(CreateIhiSlot());

                // Status
                slots.Add(CreateStatusSlot());

                // Class code
                if (ClassCode != null && ClassCode.Count > 0)
                {
                    slots.Add(CreateClassCodeSlot(ClassCode));
                }

                // Type code + (Sub)Type code
                if (TypeCode != null || SubTypeCode != null)
                {
                    slots.Add(CreateTypeCodeSlot(TypeCode, SubTypeCode));
                }

                // Practice setting
                if (PracticeSettingCode != null && PracticeSettingCode.Count > 0)
                {
                    slots.Add(CreatePracticeSettingCodeSlot(PracticeSettingCode));
                }

                // Health care facility
                if (HealthcareFacilityTypeCode != null && HealthcareFacilityTypeCode.Count > 0)
                {
                    slots.Add(CreateHealthcareFacilityTypeCodeSlot(HealthcareFacilityTypeCode));
                }

                // Creation time from
                if (CreationTimeFrom != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryCreationTimeFrom", new[] {CreationTimeFrom.ToString()}));
                }

                // Creation time to
                if (CreationTimeTo != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryCreationTimeTo", new[] {CreationTimeTo.ToString()}));
                }

                // Service start time from
                if (ServiceStartTimeFrom != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryServiceStartTimeFrom",
                                         new[] {ServiceStartTimeFrom.ToString()}));
                }

                // Service start time to
                if (ServiceStartTimeTo != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryServiceStartTimeTo", new[] {ServiceStartTimeTo.ToString()}));
                }

                // Service stop time from
                if (ServiceStopTimeFrom != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryServiceStopTimeFrom", new[] {ServiceStopTimeFrom.ToString()}));
                }

                // Service stop time to
                if (ServiceStopTimeTo != null)
                {
                    slots.Add(CreateSlot("$XDSDocumentEntryServiceStopTimeTo", new[] {ServiceStopTimeTo.ToString()}));
                }

                // Format code
                if (FormatCode != null && FormatCode.Count > 0)
                {
                    slots.Add(CreateFormatCodeSlot(FormatCode));
                }

                // Author person
                if (AuthorPerson != null && AuthorPerson.Count > 0)
                {
                    slots.Add(CreateAuthorPersonSlot());
                }

                // Document entry unique ID
                if (!string.IsNullOrEmpty(DocumentUniqueId))
                {
                    slots.Add(CreateDocumentEntryUniqueIdSlot(DocumentUniqueId));
                }

                AdhocQueryType adhocQuery = new AdhocQueryType();
                adhocQuery.Slot = slots.ToArray();
                adhocQuery.id = XdsFindDocumentList;

                return adhocQuery;
            }
        }

        /// <summary>
        /// Builds an adhoc query request.
        /// </summary>
        /// <returns>Adhoc query request.</returns>
        public AdhocQueryRequest BuildRequest()
        {
            AdhocQueryRequest queryRequest = new AdhocQueryRequest();
            queryRequest.ResponseOption = new ResponseOptionType();
            queryRequest.ResponseOption.returnComposedObjects = true;
            queryRequest.ResponseOption.returnType = ResponseOptionTypeReturnType.LeafClass;
            queryRequest.AdhocQuery = Build();

            return queryRequest;
        }

        private SlotType1 CreateClassCodeSlot(IList<ClassCodes> classCode)
        {
            IList<string> items = new List<string>();
            foreach (ClassCodes codes in classCode)
            {
                string code = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);
                string codeSystem = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.CodingSystem);
                items.Add(CreateCodeText(code, codeSystem));
            }

            return CreateSlot("$XDSDocumentEntryClassCode", items.ToArray());
        }

        private SlotType1 CreateTypeCodeSlot(IList<ClassCodes> typeCode, IList<Tuple<string, string>> subTypeCode)
        {
            IList<string> items = new List<string>();
            // Enum Subtypes
            if (typeCode != null)
            {
                foreach (ClassCodes codes in typeCode)
                {
                    string code = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);
                    string codeSystem = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.CodingSystem);
                    items.Add(CreateCodeText(code, codeSystem));
                }
            }

            if (subTypeCode != null)
            {
                // Custom Subtypes
                foreach (var codes in subTypeCode)
                {
                    string code = codes.Item1;
                    string codeSystem = codes.Item2;
                    items.Add(CreateCodeText(code, codeSystem));
                }
            }

            return CreateSlot("$XDSDocumentEntryTypeCode", items.ToArray());
        }

        private SlotType1 CreatePracticeSettingCodeSlot(IList<PracticeSettingTypes> practiceSettingCode)
        {
            IList<string> items = new List<string>();
            foreach (PracticeSettingTypes codes in practiceSettingCode)
            {
                string code = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);
                string codeSystem = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.CodingSystem);
                items.Add(CreateCodeText(code, codeSystem));
            }

            return CreateSlot("$XDSDocumentEntryPracticeSettingCode", items.ToArray());
        }

        private SlotType1 CreateHealthcareFacilityTypeCodeSlot(IList<HealthcareFacilityTypeCodes> healthCareFacilityTypeCode)
        {
            IList<string> items = new List<string>();
            foreach (HealthcareFacilityTypeCodes codes in healthCareFacilityTypeCode)
            {
                string code = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.ConceptCode);
                string codeSystem = codes.GetAttributeValue<CodedValueAttribute, string>(a => a.CodingSystem);
                items.Add(CreateCodeText(code, codeSystem));
            }

            return CreateSlot("$XDSDocumentEntryHealthcareFacilityTypeCode", items.ToArray());
        }

        private SlotType1 CreateFormatCodeSlot(IList<PcehrFormatCode> formatCodes)
        {
            IList<string> items = new List<string>();
            foreach (var formatCode in formatCodes)
            {
                string code = formatCode.ConceptCode;
                string codeSystem = "PCEHR_FormatCodes";
                items.Add(CreateCodeText(code, codeSystem));
            }

            return CreateSlot("$XDSDocumentEntryFormatCode", items.ToArray());
        }

        private SlotType1 CreateAuthorPersonSlot()
        {
            IList<string> items = new List<string>();
            foreach (Author author in AuthorPerson)
            {
                items.Add("('^" + author.Family + "^" + author.Given + "^^^" + author.Prefix + "^^^&1.2.36.1.2001.1003.0." + author.Hpii + "&ISO')");
            }

            return CreateSlot("$XDSDocumentEntryAuthorPerson", items.ToArray());
        }

        private SlotType1 CreateStatusSlot()
        {
            IList<string> statusItems = new List<string>();
            foreach (DocumentStatus status in Status)
            {
                statusItems.Add("('" + status.GetAttributeValue<CodedValueAttribute,string>(a => a.ConceptCode) + "')");
            }

            return CreateSlot("$XDSDocumentEntryStatus", statusItems.ToArray());            
        }

        private SlotType1 CreateIhiSlot()
        {
            return CreateSlot("$XDSDocumentEntryPatientId", new[] {"'" + Ihi + "^^^&1.2.36.1.2001.1003.0&ISO'"});
        }

        private static string CreateCodeText(string code, string codeSystem)
        {
            return "('" + code + "^^" + codeSystem + "')";
        }

        private SlotType1 CreateDocumentEntryUniqueIdSlot(string uniqueId)
        {
            return CreateSlot("$XDSDocumentEntryUniqueId", new[] { "('" + uniqueId + "')" });
        }

        private static SlotType1 CreateSlot(string name, string[] value)
        {
            SlotType1 slot = new SlotType1();
            slot.name = name;

            slot.ValueList = new ValueListType();
            if (value.Length > 0)
            {
                slot.ValueList.Value = value;
            }
            return slot;
        }

        public class PcehrFormatCode
        {
            public string ConceptCode { get; set; }
            public string ConceptName { get; set; }
        }
    }
}

