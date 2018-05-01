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
using System.Text;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Clinical specialties where the act that resulted in the document was performed.
    /// </summary>
    public enum PracticeSettingTypes
    {
        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-1", ConceptName = "Acupuncture service")]
        AcupunctureService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-1", ConceptName = "Adoption service")]
        AdoptionService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-2", ConceptName = "Adult day care centre operation")]
        AdultDayCareCentreOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8591-1", ConceptName = "Aerial ambulance service")]
        AerialAmbulanceService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-3", ConceptName = "Aged care assistance service")]
        AgedCareAssistanceService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-4", ConceptName = "Alcoholics anonymous operation")]
        AlcoholicsAnonymousOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-1", ConceptName = "Allergy specialist service")]
        AllergySpecialistService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8591-2", ConceptName = "Ambulance service")]
        AmbulanceService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-2", ConceptName = "Anaesthetist service")]
        AnaesthetistService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-1", ConceptName = "Application hosting")]
        ApplicationHosting,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-2", ConceptName = "Application service provision")]
        ApplicationServiceProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-2", ConceptName = "Aromatherapy service")]
        AromatherapyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-3", ConceptName = "Audio and visual media streaming service")]
        AudioAndVisualeMediaStreamingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-3", ConceptName = "Audiology service")]
        AudiologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-4", ConceptName = "Automated data processing service")]
        AutomatedDataProcessingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-1", ConceptName = "Before and/or after school care service")]
        BeforeAndOrAfterSchoolCareService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-1", ConceptName = "Billing and record-keeping service")]
        BillingAndRecordKeepingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-1", ConceptName = "Blood bank operation")]
        BloodBankOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-2", ConceptName = "Business administrative service")]
        BusinessAdministrativeService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-5", ConceptName = "Charitable hostels for the aged")]
        CharitableHostelsForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-2", ConceptName = "Child care service")]
        ChildCareService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-3", ConceptName = "Childminding service")]
        ChildmindingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-1", ConceptName = "Children's Hospital")]
        ChildrensHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-4", ConceptName = "Children's nursery operation (except preschool education)")]
        ChildrensNurseryOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-6", ConceptName = "Children's play programs")]
        ChildrensPlayPrograms,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8534-1", ConceptName = "Chiropractic")]
        Chiropractic,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-3", ConceptName = "Clerical service")]
        ClericalService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-4", ConceptName = "Clinical psychology service")]
        ClinicalPsychologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-1", ConceptName = "Colleges of education operation")]
        CollegesOfEducationOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511-5", ConceptName = "Community Health Care")]
        CommunityHealthCare,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-4", ConceptName = "Community Health Facility")]
        CommunityHealthFacility,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-8", ConceptName = "Community health facility - mental")]
        CommunityHealthFacilityMental,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-9", ConceptName = "Community health facility - other")]
        CommunityHealthFacilityOther,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-7", ConceptName = "Community health facility - substance abuse")]
        CommunityHealthFacilitySubstanceAbuse,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "4271-2", ConceptName = "Community Pharmacy")]
        CommunityPharmacy,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5922-1", ConceptName = "Computer data storage and retrieval service (except library service)")]
        ComputerDataStorageAndRetrievalService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-1", ConceptName = "Computer hardware consulting service")]
        ComputerHardwareConsultingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-5", ConceptName = "Computer input preparation service")]
        ComputerInputPreparationService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-2", ConceptName = "Computer programming service")]
        ComputerProgrammingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-3", ConceptName = "Computer software consulting service")]
        ComputerSoftwareConsultingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-6", ConceptName = "Computer time leasing or renting")]
        ComputerTimeLeasingOrRenting,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-7", ConceptName = "Computer time sharing service")]
        ComputerTimeSharingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-1", ConceptName = "Conservative dental service")]
        ConservativeDentalService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-1", ConceptName = "Contact lens dispensing")]
        ContactLensDispensing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6961-1", ConceptName = "Corporate head office management")]
        CorporateHeadOfficeManagement,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-8", ConceptName = "Data capture imaging service")]
        DataCaptureImagingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-9", ConceptName = "Data entry service (electronic)")]
        DataEntryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-10", ConceptName = "Data processing computer service")]
        DataProcessingComputerService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-2", ConceptName = "Day Hospital nec")]
        DayHospitalNec,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-18", ConceptName = "Defence Force Hospital")]
        DefenceForceHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-2", ConceptName = "Dental hospital (out-patient)")]
        DentalHospitalOutPatient,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-5", ConceptName = "Dental hygiene service")]
        DentalHygieneService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6321-1", ConceptName = "Dental insurance provision")]
        DentalInsuranceProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-3", ConceptName = "Dental practice service")]
        DentalPracticeService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-4", ConceptName = "Dental practitioner service")]
        DentalPractitionerService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-5", ConceptName = "Dental surgery service")]
        DentalSurgeryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-3", ConceptName = "Dermatology Service")]
        DermatologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8520-1", ConceptName = "Diagnostic imaging service")]
        DiagnosticImagingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-6", ConceptName = "Dietician service")]
        DieticianService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-5", ConceptName = "Disabilities assistance service")]
        DisabilitiesAssistanceService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-11", ConceptName = "Disk and diskette conversion and recertification service")]
        DiskAndDisketteConversionAndRecertificationService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7511-1", ConceptName = "Divisions of General Practice")]
        DivisionsOfGeneralPractice,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-3", ConceptName = "Ear, nose and throat hospital")]
        EarNoseAndThroatHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-4", ConceptName = "Ear, nose and throat specialist service")]
        EarNoseAndThroatSpecialistService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-12", ConceptName = "Electronic data processing service")]
        ElectronicDataProcessingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5922-2", ConceptName = "Electronic information storage and retrieval service (except library service)")]
        ElectronicInformationStorageAndRetrievalService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-19", ConceptName = "Emergency Department Services")]
        EmergencyDepartmentServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-6", ConceptName = "Endodontic service")]
        EndodonticService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-18", ConceptName = "Extended Allied Health services")]
        ExtendedAlliedHealthServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-4", ConceptName = "Eye Hospital")]
        EyeHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-2", ConceptName = "Eye testing (optometrist)")]
        EyeTestingOptometrist,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710-5", ConceptName = "Family day care service")]
        FamilyDayCareService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511-1", ConceptName = "Flying doctor service")]
        FlyingDoctorService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6321-2", ConceptName = "Funeral benefit provision")]
        FuneralBenefitProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7561-1", ConceptName = "General Health Administration")]
        GeneralHealthAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-5", ConceptName = "General Hospital")]
        GeneralHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511-2", ConceptName = "General medical practitioner service")]
        GeneralMedicalPractitionerService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511-3", ConceptName = "General practice medical clinic service")]
        GeneralPracticeMedicalClinicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-2", ConceptName = "Government nursing home for the aged")]
        GovernmentNursingHomeForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-3", ConceptName = "Government nursing home for young disabled")]
        GovernmentNursingHomeForYoungDisabled,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-5", ConceptName = "Gynaecology services")]
        GynaecologyServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-6", ConceptName = "Hair transplant service (by registered medical practitioner)")]
        HairTransplantServiceByRegisteredMedicalPractitioner,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "9111-1", ConceptName = "Health and Fitness Centres and Gymnasia Operation")]
        HealthAndFitnessCentresAndGymnasiaOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-2", ConceptName = "Health assessment service")]
        HealthAssessmentService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6321-3", ConceptName = "Health insurance provision")]
        HealthInsuranceProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-3", ConceptName = "Healthcare service nec")]
        HealthcareServiceNec,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-7", ConceptName = "Hearing aid dispensing")]
        HearingAidDispensing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-8", ConceptName = "Herbalist service")]
        HerbalistService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-9", ConceptName = "Homoeopathic service")]
        HomoeopathicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-6", ConceptName = "Hospital (except psychiatric or veterinary hospitals)")]
        HospitalExceptPsychiatricOrVeterinaryHospitals,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-10", ConceptName = "Hydropathic service")]
        HydropathicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-7", ConceptName = "Infectious diseases hospital (including human quarantine stations)")]
        InfectiousDiseasesHospitalIncludingHumanQuarantineStations,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-1", ConceptName = "Internet access provision")]
        InternetAccessProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-2", ConceptName = "Internet access service, on-line")]
        InternetAccessService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-4", ConceptName = "Internet and web design consulting service")]
        InternetAndWebDesignConsultingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-3", ConceptName = "Internet search portal operation")]
        InternetSearchPortalOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-4", ConceptName = "Internet search web site operation")]
        InternetSearchWebSiteOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-5", ConceptName = "Internet service provision (ISP)")]
        InternetServiceProvision,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6999-1", ConceptName = "Interpretation service")]
        InterpretationService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7531-1", ConceptName = "Local Government Healthcare Administration")]
        LocalGovernmentHealthcareAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-6", ConceptName = "Local government hostel for the aged")]
        LocalGovernmentHostelForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-6", ConceptName = "Marriage guidance service")]
        MarriageGuidanceService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-8", ConceptName = "Maternity Hospital")]
        MaternityHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8520-2", ConceptName = "Medical laboratory service")]
        MedicalLaboratoryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6910-1", ConceptName = "Medical research service")]
        MedicalResearchService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-13", ConceptName = "Microfiche or microfilm recording and imaging service")]
        MicroficheOrMicrofilmRecordingAndImagingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-11", ConceptName = "Midwifery service")]
        MidwiferyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-12", ConceptName = "Naturopathic service")]
        NaturopathicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-7", ConceptName = "Neurology service")]
        NeurologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-13", ConceptName = "Nursing service")]
        NursingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-8", ConceptName = "Obstetrics service")]
        ObstetricsService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-9", ConceptName = "Obstretic Hospital")]
        ObstreticHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-14", ConceptName = "Occupational therapy service")]
        OccupationalTherapyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-4", ConceptName = "Office administrative service n.e.c.")]
        OfficeAdministrativeServiceNEC,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-7", ConceptName = "Operation of soup kitchen (including mobile)")]
        OperationOfSoupKitchenIncludingMobile,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-9", ConceptName = "Ophthalmology service")]
        OphthalmologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-3", ConceptName = "Optical dispensing")]
        OpticalDispensing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-14", ConceptName = "Optical scanning service")]
        OpticalScanningService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-4", ConceptName = "Optician service")]
        OpticianService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-7", ConceptName = "Oral pathology service")]
        OralPathologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-8", ConceptName = "Oral surgery service")]
        OralSurgeryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-9", ConceptName = "Orthodontic service")]
        OrthodonticService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-10", ConceptName = "Orthopaedic service")]
        OrthopaedicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-5", ConceptName = "Orthoptic service")]
        OrthopticService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8534-2", ConceptName = "Osteopathic Services")]
        OsteopathicServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-4", ConceptName = "Other charitable hostel")]
        OtherCharitableHostel,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-19", ConceptName = "Other Commonwealth Hospital")]
        OtherCommonwealthHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-6", ConceptName = "Other Local government hostel")]
        OtherLocalGovernmentHostel,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-5", ConceptName = "Other State government hostel")]
        OtherStateGovernmentHostel,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-11", ConceptName = "Paediatric service")]
        PaediatricService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8520-3", ConceptName = "Pathology laboratory service")]
        PathologyLaboratoryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-5", ConceptName = "Payroll processing")]
        PayrollProcessing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-10", ConceptName = "Pedodontics service")]
        PedodonticsService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-11", ConceptName = "Periodontic service")]
        PeriodonticService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "4271-1", ConceptName = "Pharmacy, retail, operation")]
        PharmacyRetailOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8533-1", ConceptName = "Physiotherapy Services")]
        PhysiotherapyServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-15", ConceptName = "Podiatry service")]
        PodiatryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-6", ConceptName = "Portal web search operation")]
        PortalWebSearchOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-2", ConceptName = "Postgraduate school, university operation")]
        PostgraduateSchoolUniversityOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-6", ConceptName = "Private (non-profit) Community Health Centre")]
        PrivateNonProfitCommunityHealthCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-16", ConceptName = "Private acute care Hospital")]
        PrivateAcuteCareHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-8", ConceptName = "Private alcohol and drug treatment centre")]
        PrivateAlcoholAndDrugTreatmentCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-3", ConceptName = "Private charitable nursing home for the aged")]
        PrivateCharitableNursingHomeForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-1", ConceptName = "Private charitable nursing home for young disabled")]
        PrivateCharitableNursingHomeForYoungDisabled,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-13", ConceptName = "Private day centre/hospital.")]
        PrivateDayCentreOrHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-14", ConceptName = "Private freestanding day surgery centre.")]
        PrivateFreestandingDaySurgeryCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8402-2", ConceptName = "Private Mental Health Hospital")]
        PrivateMentalHealthHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-1", ConceptName = "Private profit nursing home for the aged")]
        PrivateProfitNursingHomeForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-2", ConceptName = "Private profit nursing home for young disabled")]
        PrivateProfitNursingHomeForYoungDisabled,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6999-2", ConceptName = "Professional, scientific and technical services n.e.c.")]
        ProfessionalScientificAndTechnicalServicesNEC,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531-12", ConceptName = "Prosthodontics service")]
        ProsthodonticsService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7562-1", ConceptName = "Provision and administration of public health program")]
        ProvisionAndAdministrationOfPublicHealthProgram,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-12", ConceptName = "Psychiatry service")]
        PsychiatryService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-15", ConceptName = "Public acute care Hospital")]
        PublicAcuteCareHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609-7", ConceptName = "Public alcohol and drug treatment centre")]
        PublicAlcoholAndDrugTreatmentCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599-5", ConceptName = "Public Community Health Centre")]
        PublicCommunityHealthCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-11", ConceptName = "Public day centre/hospital")]
        PublicDayCentreHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-12", ConceptName = "Public freestanding day surgery centre.")]
        PublicFreestandingDaySurgeryCentre,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8402-1", ConceptName = "Public Mental Health Hospital")]
        PublicMentalHealthHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291-6", ConceptName = "Reception service")]
        ReceptionService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-3", ConceptName = "Research school, university operation")]
        ResearchSchoolUniversityOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-13", ConceptName = "Rheumatology service")]
        RheumatologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511-4", ConceptName = "Rural general medical practice service")]
        RuralGeneralMedicalPracticeService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6910-2", ConceptName = "Social science research service")]
        SocialScienceResearchService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-5", ConceptName = "Software development (customised) service (except publishing)")]
        SoftwareDevelopmentServiceExceptPublishing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-6", ConceptName = "Software installation service")]
        SoftwareInstallationService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-4", ConceptName = "Specialist institute or college")]
        SpecialistInstituteOrCollege,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-14", ConceptName = "Specialist medical clinic service")]
        SpecialistMedicalClinicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-15", ConceptName = "Specialist medical practitioner service nec")]
        SpecialistMedicalPractitionerServiceNEC,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-16", ConceptName = "Specialist surgical service")]
        SpecialistSurgicalService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532-6", ConceptName = "Spectacles dispensing")]
        SpectaclesDispensing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-16", ConceptName = "Speech pathology service")]
        SpeechPathologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7521-1", ConceptName = "State Government Healthcare Administration")]
        StateGovernmentHealthcareAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601-4", ConceptName = "State government hostel for the aged")]
        StateGovernmentHostelForTheAged,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-20", ConceptName = "Subacute Hospitals")]
        SubacuteHospitals,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000-7", ConceptName = "Systems analysis service")]
        SystemsAnalysisService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-5", ConceptName = "Teachers' college operation")]
        TeachersCollegeOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7294-1", ConceptName = "Telephone answering service")]
        TelephoneAnsweringService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7294-2", ConceptName = "Telephone call centre operation")]
        TelephoneCallCentreOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539-17", ConceptName = "Therapeutic massage service")]
        TherapeuticMassageService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-17", ConceptName = "Thoracic specialist service")]
        ThoracicSpecialistService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6999-3", ConceptName = "Translation service")]
        TranslationService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "4623-1", ConceptName = "Transport")]
        Transport,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-6", ConceptName = "Undergraduate school, university operation")]
        UndergraduateSchoolUniversityOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102-7", ConceptName = "University operation")]
        UniversityOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512-18", ConceptName = "Urology service")]
        UrologyService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-17", ConceptName = "Veterans Affairs Hospital")]
        VeteransAffairsHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7294-3", ConceptName = "Voice mailbox service")]
        VoiceMailboxService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921-15", ConceptName = "Web hosting")]
        WebHosting,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910-7", ConceptName = "Web search portal operation")]
        WebSearchPortalOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-8", ConceptName = "Welfare counselling service")]
        WelfareCounsellingService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401-10", ConceptName = "Women's Hospital")]
        WomensHospital,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8520-4", ConceptName = "X-ray clinic service")]
        XRayClinicService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790-9", ConceptName = "Youth welfare service")]
        YouthWelfareService,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "TBD", ConceptName = "TBD")]
        TBD,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "XXXXDONOTUSEXXXX", ConceptName = "XXXXDONOTUSEXXXX")]
        XXXXDONOTUSEXXXX

    }
}
