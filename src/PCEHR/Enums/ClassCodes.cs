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

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Class codes.
    /// </summary>
    public enum ClassCodes
    {
        /// <summary>
        /// Shared health summary class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "60591-5", 
            ConceptName = "Patient Summary", AlternateName = "Shared Health Summary")]
        SharedHealthSummary,

        /// <summary>
        /// Ereferral class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "57133-1", 
            ConceptName = "Referral Note", AlternateName = "e-Referral")]
        eReferral,

        /// <summary>
        /// Specialist letter class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "51852-2",
            ConceptName = "Letter", AlternateName = "Specialist Letter")]
        SpecialistLetter,

        /// <summary>
        /// Discharge summary class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "18842-5",
            ConceptName = "Discharge Summarization Note", AlternateName = "Discharge Summary")]
        DischargeSummary,

        /// <summary>
        /// Event summary class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "34133-9",
            ConceptName = "Summarization of episode note", AlternateName = "Event Summary")]
        EventSummary,

        /// <summary>
        /// Pharmaceutical Benefits Report class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16650",
            ConceptName = "Pharmaceutical Benefits Report", AlternateName = "Pharmaceutical Benefits Report")]
        PharmaceuticalBenefitsReport,

        /// <summary>
        /// Australian Organ Donor Register class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16671",
            ConceptName = "Australian Organ Donor Register", AlternateName = "Australian Organ Donor Register")]
        AustralianOrganDonorRegister,

        /// <summary>
        /// Australian Childhood Immunisation Register class code.
        /// </summary>
        //[CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16659",
        //    ConceptName = "Australian Childhood Immunisation Register", AlternateName = "Australian Childhood Immunisation Register")]
        //AustralianChildhoodImmunisationRegister,

        /// <summary>
        /// Australian Immunisation Register class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.17042",
            ConceptName = "Australian Immunisation Register", AlternateName = "Australian Immunisation Register")]
        AustralianImmunisationRegister,

        /// <summary>
        /// Medicare/DVA Benefits Report class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16644",
            ConceptName = "Medicare/DVA Benefits Report", AlternateName = "Medicare/DVA Benefits Report")]
        MedicareDvaBenefitsReport,

        /// <summary>
        /// Prescription and Dispense View
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16789",
            ConceptName = "PCEHR Prescription and Dispense View", AlternateName = "Prescription and Dispense View")]
        PrescriptionAndDispenseView,

        /// <summary>
        /// Health Check Schedule View
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16940",
            ConceptName = "Health Check Schedule View", AlternateName = "Health Check Schedule View")]
        HealthCheckScheduleView,

        /// <summary>
        /// Observation Overview
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16872",
            ConceptName = "Observation View", AlternateName = "Observation View")]
        ObservationView,

        /// <summary>
        /// Medicare Overview
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16767",
            ConceptName = "Medicare Overview", AlternateName = "Medicare Overview")]
        MedicareOverview,

        /// <summary>
        /// Medicare Overview - all
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16767.1",
            ConceptName = "Medicare Overview - all", AlternateName = "Medicare Overview - all")]
        MedicareOverviewAll,

        /// <summary>
        /// Medicare Overview - past 12 months
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16767.2",
            ConceptName = "Medicare Overview - past 12 months", AlternateName = "Medicare Overview - past 12 months")]
        MedicareOverviewPast12Months,

        /// <summary>
        /// Consumer Entered Notes class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16681",
            ConceptName = "Consumer Entered Notes", AlternateName = "Personal Health Note")]
        ConsumerEnteredNotes,

        /// <summary>
        /// Consumer Entered Health Summary class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16685",
            ConceptName = "Consumer Entered Health Summary", AlternateName = "Personal Health Summary")]
        ConsumerEnteredHealthSummary,

        /// <summary>
        /// Advance Care Directive Custodian Record class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16696",
            ConceptName = "Advance Care Directive Custodian Record", AlternateName = "Advance Care Directive Custodian Record")]
        AdvanceCareDirectiveCustodianRecord,

        /// <summary>
        /// PCEHR prescription record class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16764",
            ConceptName = "PCEHR Prescription Record", AlternateName = "eHealth Prescription Record")]
        PcehrPrescriptionRecord,

        /// <summary>
        /// PCEHR dispense record class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16765",
            ConceptName = "PCEHR Dispense Record", AlternateName = "eHealth Dispense Record")]
        PcehrDispenseRecord,

        /// <summary>
        /// Diagnostic imaging report class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16957",
            ConceptName = "Diagnostic Imaging Report", AlternateName = "Diagnostic Imaging Report")]
        DiagnosticImagingReport,

        /// <summary>
        /// Pathology result report class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32001",
            ConceptName = "Pathology Report", AlternateName = "Pathology Report")]
        PathologyResultReport,

        /// <summary>
        /// Advance care information class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16975",
            ConceptName = "Advance Care Information", AlternateName = "Advance Care Information")]
        AdvanceCareInformation,

        /// <summary>
        /// AdvanceCareInformation Subtype
        /// Advance care planning type code (NOT CLASS CODE).
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16998",
            ConceptName = "Advance Care Planning Document", AlternateName = "Advance Care Planning Document")]
        AciTypeAdvanceCarePlanning,

        /// <summary>
        /// AdvanceCareInformation Subtype
        /// Goals Of Care Document type code (NOT CLASS CODE).
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32016",
            ConceptName = "Goals of Care Document", AlternateName = "Goals of Care Document")]
        AciTypeGoalsOfCare,


        // CeHR documents

        /// <summary>
        /// Health Check Assessment class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16920",
            ConceptName = "Health Check Assessment", AlternateName = "Health Check Assessment")]
        HealthCheckAssessment,

        /// <summary>
        /// Child Parent Questionnaire class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16919",
            ConceptName = "Child Parent Questionnaire", AlternateName = "Child Parent Questionnaire")]
        ChildParentQuestionnaire,

        /// <summary>
        /// Consumer Entered Achievements class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16812",
            ConceptName = "Consumer Entered Achievements", AlternateName = "Consumer Entered Achievements")]
        ConsumerEnteredAchievements,

        /// <summary>
        /// Consumer Entered Achievements class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16870",
            ConceptName = "Consumer Entered Measurements", AlternateName = "Consumer Entered Measurements")]
        ConsumerEnteredMeasurements,

        /// <summary>
        /// Birth Details class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16939",
            ConceptName = "Birth Details", AlternateName = "Birth Details")]
        BirthDetails,

        /// <summary>
        /// Medicines View
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32002",
            ConceptName = "Medicines View", AlternateName = "Medicines View")]
        MedicinesView,

        /// <summary>
        /// Pharmacist Shared Medicines List
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "56445-0",
            ConceptName = "Pharmacist Shared Medicines List", AlternateName = "Pharmacist Shared Medicines List")]
        PharmacistSharedMedicinesList,

        /// <summary>
        /// Pathology Overview
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32026",
            ConceptName = "Pathology Overview", AlternateName = "Pathology Overview")]
        PathologyOverview,

        /// <summary>
        /// Diagnostic Imaging Overview
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32025",
            ConceptName = "Diagnostic Imaging Overview", AlternateName = "Diagnostic Imaging Overview")]
        DiagnosticImagingOverview,

        /// <summary>
        /// Immunisation Consolidated View
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32034",
            ConceptName = "Immunisation Consolidated View", AlternateName = "Immunisation Consolidated View")]
        ImmunisationConsolidatedView,



        /// <summary>
        /// ACTS - Type - Residential Care Transfer Summary
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32049",
            ConceptName = "Residential Care Health Summary", AlternateName = "Residential Care Health Summary")]
        ActsTypeResidentialCareHealthSummary,

        /// <summary>
        /// ACTS - Class - Patient Health Summary
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32050",
            ConceptName = "Patient Health Summary", AlternateName = "Patient Health Summary")]
        ActsClassPatientHealthSummary,

        /// <summary>
        /// ACTS - Type - Residential Care Medication Chart
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32046",
            ConceptName = "Residential Care Medication Chart", AlternateName = "Residential Care Medication Chart")]
        ActsTypeResidentialCareMedicationChart,

        /// <summary>
        /// ACTS - Class - Medication Chart
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "80565-5",
            ConceptName = "Medication Chart", AlternateName = "Medication Chart")]
        ActsClassMedicationChart,

        /// <summary>
        /// ACTS - Type - Residential Care Transfer Reason
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32044",
            ConceptName = "Residential Care Transfer Reason", AlternateName = "Residential Care Transfer Reason")]
        ActsTypeResidentialCareTransferNote,

        /// <summary>
        /// ACTS - Class - Residential Care Transfer Note
        /// </summary>
        [CodedValueAttribute(CodingSystem = "LOINC", CodingSystemOID = "2.16.840.1.113883.6.1", ConceptCode = "18761-7",
            ConceptName = "Transfer Summary", AlternateName = "Transfer Summary")]
        ActsClassTransferSummary,

        /// ACTS - Type - Residential Care Transfer Overview (view)
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32047",
            ConceptName = "Residential Care Transfer Overview", AlternateName = "Residential Care Transfer Overview")]
        ActsTypeResidentialCareTransferOverview,


        /// <summary>
        /// MyMedicare Registered Practice Information
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.32048",
            ConceptName = "MyMedicare Registered Practice Information", AlternateName = "MyMedicare Registered Practice Information")]
        MyMedicareRegisteredPracticeInformation,


        /*
        /// <summary>
        /// Eprescription class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16100",
            ConceptName = "e-Prescription", AlternateName = "e-Prescription")]
        ePrescription,

        /// <summary>
        /// Dispense record class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16112",
            ConceptName = "Dispense Record", AlternateName = "Dispense Record")]
        DispenseRecord,

        /// <summary>
        /// Prescription request class code.
        /// </summary>
        [CodedValueAttribute(CodingSystem = "NCTIS Data Components", CodingSystemOID = "1.2.36.1.2001.1001.101", ConceptCode = "100.16285",
            ConceptName = "Prescription Request", AlternateName = "Prescription Request")]
        PrescriptionRequest
        */
    }
}
