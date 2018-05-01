### PCEHR_Schemas-20160218 v4.0.0-WithMod.zip - 18th Feb 2016
---
- Added the AdvanceCarePlanningView

### PCEHR_Schemas-20150429 v3.0.0-WithMod.zip - 29th April 2015
---

This zip file contains files that have been supplied by the National Infrastructure Partner (NIO) 
for the MHR.
These files are supplied under the "LICENSE.txt" file included in this zip.

### Changes

The Agency have made one modification to the "rim.xsd" file to support developers implementation tools.

<complexType name="RegistryObjectListType">
    
 <sequence>

  <!-- PW: HAS TO BE IN THIS ORDER AS THIS IS HOW IT COMES BACK FROM ORACLE INTERFACE -->
      
  <element maxOccurs="unbounded" minOccurs="0" ref="tns:ExtrinsicObject"/>

  <element maxOccurs="unbounded" minOccurs="0" ref="tns:RegistryPackage"/>
  <element maxOccurs="unbounded" minOccurs="0" ref="tns:Classification"/>

  <element maxOccurs="unbounded" minOccurs="0" ref="tns:Association"/>

  <!-- PW: END OF MODIFICATION -->
  <element maxOccurs="unbounded" minOccurs="0" ref="tns:Identifiable"/>

 </sequence>
</complexType>


### ZIP Structure
---

### Additional files provided by the Agency:


- \build.cmd - Compiles the wsdls/schemas into .net cs files
- \schema\external\view\BuildCS.cmd - Compiles the schemas into .net cs files for the getView operation
- \LICENCE.txt - Licence to go with these files
- \README.md - This File

Files provided by the NIO:
--------------------------

### \wsdl\external

B2B_DocumentRegistry.wsdl
B2B_DocumentRepository.wsdl
B2B_GetAuditView.wsdl
B2B_GetAuditViewInterface.wsdl
B2B_GetChangeHistoryView.wsdl
B2B_GetChangeHistoryViewInterface.wsdl
B2B_GetIndividualDetailsView.wsdl
B2B_GetIndividualDetailsViewInterface.wsdl
B2B_GetRepresentativeList.wsdl
B2B_GetRepresentativeListInterface.wsdl
B2B_GetTemplate.wsdl
B2B_GetTemplateInterface.wsdl
B2B_GetView.wsdl
B2B_GetViewInterface.wsdl
B2B_PCEHRProfile.wsdl
B2B_PCEHRProfileInterface.wsdl
B2B_RegisterPCEHR.wsdl
B2B_RegisterPCEHRInterface.wsdl
B2B_RemoveDocument.wsdl
B2B_RemoveDocumentInterface.wsdl
B2B_SearchTemplate.wsdl
B2B_SearchTemplateInterface.wsdl

### \schema\Common
PCEHR_CommonTypes.xsd
PCEHR_CommonTypes_Supplementary.xsd
PCEHR_TemplatesTypes.xsd
wsp-StandardError-2010.xsd
xmldsig-core-schema.xsd

### \schema\external
lcm.xsd
PCEHR_DoesPCEHRExist.xsd
PCEHR_GainPCEHRAccess.xsd
PCEHR_GetAuditView.xsd
PCEHR_GetChangeHistoryView.xsd
PCEHR_GetIndividualDetailsView.xsd
PCEHR_GetRepresentativeList.xsd
PCEHR_GetTemplate.xsd
PCEHR_GetView.xsd
PCEHR_RegisterPCEHR.xsd
PCEHR_RemoveDocument.xsd
PCEHR_SearchTemplate.xsd
query.xsd
rim.xsd
rs.xsd
XDS.b_DocumentRepository.xsd
xsd_1.xsd

### \schema\external\view
PCEHR_AdvanceCarePlanningView.xsd
PCEHR_AdvanceCarePlanningView_Response.xsd
PCEHR_DiagnosticImagingReportView.xsd
PCEHR_DiagnosticImagingReportView_Response.xsd
PCEHR_HealthCheckScheduleView.xsd
PCEHR_HealthRecordOverview.xsd
PCEHR_HealthRecordOverview_Response.xsd
PCEHR_MedicareOverview.xsd
PCEHR_ObservationView.xsd
PCEHR_PathologyReportView.xsd
PCEHR_PathologyReportView_Response.xsd
PCEHR_PrescriptionAndDispenseView.xsd
PCEHR_View_CommonType.xsd
