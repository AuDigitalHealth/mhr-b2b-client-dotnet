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
    /// Types of organizational setting of the clinical encounter during which the documented act occured.
    /// </summary>
    public enum HealthcareFacilityTypeCodes
    {
        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8601", ConceptName = "Aged Care Residential Services")]
        AgedCareResidentialServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8591", ConceptName = "Ambulance Services")]
        AmbulanceServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7294", ConceptName = "Call Centre Operation")]
        CallCentreOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7511", ConceptName = "Central Government Healthcare Administration")]
        CentralGovernmentHealthcareAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8710", ConceptName = "Child Care Services")]
        ChildCareServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8534", ConceptName = "Chiropractic and Osteopathic Services")]
        ChiropracticAndOsteopathicServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7000", ConceptName = "Computer System Design and Related Services")]
        ComputerSystemDesignAndRelatedServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6961", ConceptName = "Corporate Head Office Management Services")]
        CorporateHeadOfficeManagementServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5921", ConceptName = "Data Processing and Web Hosting Services")]
        DataProcessingAndWebHostingServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8531", ConceptName = "Dental Services")]
        DentalServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5922", ConceptName = "Electronic Information Storage Services")]
        ElectronicInformationStorageServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7561", ConceptName = "General Health Administration")]
        GeneralHealthAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8511", ConceptName = "General Practice")]
        GeneralPractice,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "9111", ConceptName = "Health and Fitness Centres and Gymnasia Operation")]
        HealthAndFitnessCentresAndGymnasiaOperation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6321", ConceptName = "Health Insurance")]
        HealthInsurance,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8102", ConceptName = "Higher Education")]
        HigherEducation,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8401", ConceptName = "Hospitals (except Psychiatric Hospitals)")]
        Hospitals,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "5910", ConceptName = "Internet Service Providers and Web Search Portals")]
        InternetServiceProvidersAndWebSearchPortals,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7531", ConceptName = "Local Government Healthcare Administration")]
        LocalGovernmentHealthcareAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8402", ConceptName = "Mental Health Hospitals")]
        MentalHealthHospitals,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7291", ConceptName = "Office Administrative Services")]
        OfficeAdministrativeServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8532", ConceptName = "Optometry and Optical Dispensing")]
        OptometryAndOpticalDispensing,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8539", ConceptName = "Other Allied Health Services")]
        OtherAlliedHealthServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8599", ConceptName = "Other Healthcare Services nec")]
        OtherHealthcareServicesNEC,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6999", ConceptName = "Other Professional, Scientific and Technical Services n.e.c.")]
        OtherProfessionalScientificAndTechnicalServicesNEC,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8609", ConceptName = "Other Residential Care Services")]
        OtherResidentialCareServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8790", ConceptName = "Other Social Assistance Services")]
        OtherSocialAssistanceServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8520", ConceptName = "Pathology and Diagnostic Imaging Services")]
        PathologyAndDiagnosticImagingServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8533", ConceptName = "Physiotherapy Services")]
        PhysiotherapyServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7562", ConceptName = "Provision and administration of public health program")]
        ProvisionAndAdministrationOfPublicHealthProgram,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "4271", ConceptName = "Retail Pharmacy")]
        RetailPharmacy,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "6910", ConceptName = "Scientific Research Services")]
        ScientificResearchServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "8512", ConceptName = "Specialist Medical Services")]
        SpecialistMedicalServices,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "7521", ConceptName = "State Government Healthcare Administration")]
        StateGovernmentHealthcareAdministration,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "4623", ConceptName = "Transport")]
        Transport,

        [CodedValueAttribute(CodingSystem = "ANZSIC", CodingSystemOID = "", ConceptCode = "XXXXDONOTUSEXXXX", ConceptName = "XXXXDONOTUSEXXXX")]
        XXXXDONOTUSEXXXX

    }
}
