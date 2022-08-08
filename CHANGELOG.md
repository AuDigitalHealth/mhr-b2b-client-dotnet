### Change Log/Revision History

1.4.2
------------------
- Fixed XdsMetadataHelper to pull out AuthorRole
- Updated SVT URL in sample code

1.4.1
------------------
- Minor update to Document Registry file adding field category

1.4.0
------------------
- Added support for uploading document subtypes (DS,ES,SL) with providing optional params on uploading
- Added support for downloading new document subtypes using SubTypeCode in the adhocQueryBuilder
- Updated Samples to show how to use optional params to override default behaviour
- Updated Samples to show how to use SubTypeCode in the adhocQueryBuilder
- Added Sample with how to process the Document Registry file - DocumentRegisterClientSample.cs

1.3.0
------------------
- Added support for NUNIT testing exposing interfaces for dummy responses to can test locally
- Re-mapped Author Role in XDS metadata from authorSpecialty to authorRole (though not used anywhere)
- Removed ACIR Code as AIR should be the only one needed
- Added support for non-ASCII Latin-1 chars in xdsmetadata

1.2.1.1
------------------
No version change 1.2.1 (.1)
Updated DotNetZip from 1.9.1.8 to 1.11.0 due to a security vulnerability (High severity). 

1.2.1
------------------
- Added support for multiple ACI subtype documents (ACP + GoC)

1.2.0
------------------
- Added ENUMs for PathologyOverview and DiagnosticImagingOverview
- Fixed support for uploading ACP documents
- As per DEXS-T-123, serviceStartTime and serviceStopTime must be at least 8 chars long

1.1.0
------------------
- Added PCML ENUM for support future release  
- Added  Helper function to process getDocumentList XDS data into simple class XDSRecord
- Updated PCML to PSML description

1.0.6
------------------
- Added new doc type Australian Immunisation Register
- Removed requirement for BouncyCastle.Crypto.dll
- Fixed getview example (function name wrong)
- added memory safe CachingXmlSerializerFactory for ConvertPcehrHeader function
- added a utility function to get Medicine View Document
- Updated example for GetIndividualDetailsView, setting request to "", not null
- Added comments to samples to help implementors
- Updated XdsMetadata.cs improving serviceStartTime and serviceStopTime times for doc types
- Added comments in samples
- Improved xdsmetadata values

1.0.5
------------------
* Updated the service date for path and di. Sort by date and take latest. Was just taking first found.
* Added ACP for getView
* Updated Enum description for ACP
* Updated Assembly versions

1.0.4
------------------
* When adding XDS metadata, default to en-AU if not supplied in CDA document
* Updated HRO response class which has some additional fields.

1.0.3
------------------
* Pathology Result Report's Reporting Pathologist is considered document author for populating XDS metadata.
* Added ENUMs for pathology and DI documents
* Updated descriptions for certain documents (ClassCodes.cs)
* ACI view removed - not required for R5
* Added specific xpaths to dates for service start and stop dates for Path and DI
* Updated the PCEHR_RegisterPCEHR Wsdl
* Change class code 'Advance Care Directive Custodian' to 'Advance Care Directive Custodian Record'


1.0.2
------------------
* Added the 4 new views for getView (Path,DI,ACI,HRO) - Draft versions at present (Mar 2014)


1.0.1
------------------
* Removed ExtrinsicObject/Name field from XDS metadata.


1.0.0
------------------
* Upgraded solution to VS2010.
* Fixed service start and stop time for CUP – UPLOAD.04 requirement for Specialist Letter


0.9.5 (DRAFT)
------------------
* Updated PCEHREndpointProcessor.cs to be able to handle "ContentID" which is not URL encoded (VS2012 and .NET 4 implementation).


0.9.4 (DRAFT)
------------------
* Added support for multiple MTOM parts when serializing outgoing SOAP request into MTOM.
* Fixed bug with AdhocQueryBuilder using $$ prefix instead of $ for XDSDocumentEntryServiceStartTimeFrom, XDSDocumentEntryServiceStartTimeTo, XDSDocumentEntryServiceStopTimeFrom and XDSDocumentEntryServiceStopTimeTo.
* Fixed ClassCode.cs file - some CodingSystem descriptions where incorrect - affecting document filtering.


0.9.3 (DRAFT)
------------------
+ Added utility function in UploadDocumentClient to include respository ID, hash and size information in the XDS metadata (for NPDR only, as the
repository is deficient in adding these additional fields when registering the document on the PCEHR).
* Corrected some document type descriptions in ClassCode.


0.9.2 (DRAFT)
------------------
* Fixed bug with CDA document title not being used in the metadata, even when it has been specified.
* Added serialization support for observationView class for use with the GetView service.


0.9.1 (DRAFT)
------------------
+ Added observationView class in to use with getView service.
+ Added metadata support for organisation details for Prescription and Dispense View.


0.9.0 (DRAFT)
------------------
+ Added class codes for PCEHR Prescription Record and PCEHR Dispense Record.
+ Added getView partial class (GeneratorCodeModifications/PCEHR_GetView10.cs) so that custom serialization attributes can be declared. This is so that the runtime knows the type of the view object during serialization.
* Updated GetViewClient to use MTOM binding by default.
* Updated GetViewClient to support the following views:
  1. PrescriptionAndDispenseView
  2. MedicareOverview
  3. HealthCheckScheduleView
* Updated XdsMetadata class to use "DateTime Prescription Written" as Service Start Time and Service End Time for PCEHR Prescription Records.
* Updated XdsMetadata class to use "DateTime of Dispense Event" as Service Start Time and Service End Time for PCEHR Dispense Records.


0.8.1 (DRAFT)
------------------
* Fixed serialisation problem when using prescriptionAndDispenseView, achievementDiaryView, observationView and medicareOverview with GetViewClient.
* Fixed incorrect XCN format. It has now been changed to:
  ^{family name}^{given name}^^^{prefix}^^^{assigning authority oid}&{id}&ISO
- Removed FormatCodes enum. PCEHR format codes and format code names are passed into related functions as strings.


0.8.0 (DRAFT)
------------------
- Remove ConsolidatedViewClient (this has been replaced by GetViewClient).
- Remove requirement (in terms of input validation) for IHI in PCEHRHeader.
+ Added schema classes for GetViewClient (PCEHR_AchievementDiaryView, PCEHR_ObservationView, PCEHR_PrescriptionAndDispenseView, PCEHR_MedicareOverview).


0.7.1 (DRAFT)
------------------
+ Added new format codes for PCEHR Prescription Record and PCEHR Dispense Record.
+ Added "authorSpecialty" into XDS metadata for both UploadDocumentClient and UploadDocumentMetadataClient.
* Updated class code for ePrescription and Dispense Record.


0.7.0 (DRAFT)
------------------
* Update to GetIndividualDetailsViewClient.
* GetRepresentativeListClient reverted to schema version 1.1.
* Update to RegisterPCEHRClient.
+ Added GetViewClient.


0.6.5 (DRAFT)
------------------
Change History:
* Upload document has been changed to support local identifiers in addition to HPI-Is.
* Updated schema generated code for RegisterPCEHR and sample code.
* Updated RegisterPCEHR client to use MTOM message encoding.


0.6.4 (DRAFT)
------------------
Known Limitations:
+ 'GetDocument' SOAP response from SVT does not comply with DEXS-T 109 of the PCEHR Document Exchange Service Technical Service Specification. This is in the process of being corrected.
+ 'UploadDocument' SOAP request is being incorrectly rejected by the SVT as having an invalid signature. This is in the process of being corrected.

Change History:
* MTOM operations have been changed to have the signature correctly applied on the entire SOAP message, and not only the MTOM SOAP envelope part.
* 'UploadDocument' SOAP request has been changed to correctly isolate the binary component as a separate MTOM part.
* The SoapMessages.IsSoapResponsesSignatureValid property has been changed to "SoapResponseSignatureStatus", an enumeration containing the values "Valid", "Invalid" and "NotPresent" to indicate the signature status of the SOAP response.
* SOAP response signature verification has been re-enabled on the "GetDocumentClient".
+ Added Close methods to all clients, to close the underlying WCF clients.


0.6.3 (DRAFT)
------------------
Known Limitations:
+ 'GetDocument' SOAP response from SVT does not comply with DEXS-T 109 of the PCEHR Document Exchange Service Technical Service Specification.

Change History:
+ Added new enum value to 'DocumentStatus' => 'urn:ordreq:names:statusType:Deleted', as documented in the PCEHR Implementation Guide.


0.6.2 (DRAFT)
------------------
Known Limitations:
+ 'GetDocument' SOAP response from SVT does not comply with DEXS-T 109 of the PCEHR Document Exchange Service Technical Service Specification.

Change History:
* GetDocumentClient indicates the above limitation by returning 'null' for the SoapMessages.IsSoapResponseSignatureValid to indicate
that the soap response signature has not been verified.


0.6.1 (DRAFT)
-----------
* Service stop time is set to either "//encompassingEncounter/effectiveTime/@value" or "ClinicalDocument/effectiveTime/@value" in the
absence of "//encompassingEncounter/effectiveTime/high/@value".


0.6.0 (DRAFT)
-----------
* Signature change in GetRepresentativeListClient "GetRepresentativeList" function (removed unnecessary "object" parameter).
+ Added sample code for GetRepresentativeListClient.
+ Added sample code for RegisterPCEHRClient.


0.5.2 (DRAFT)
-----------
* Updated metadata generation for UploadDocumentClient and UploadDocumentMetadataClient
to use symbolic IDs instead of UUIDs. 


0.5.1 (DRAFT)
-----------
* Fixed bug with CDA date time conversion not being able to handle 24-hour time.


0.5 (DRAFT)
-----------
This is a draft release of sample code the PCEHR interfaces.
This release works against the current interfaces.

Known Issues:
- CDA document <id> root value must be an OID, for document uploads. Other
  operations may accept both in situations where the SVT environment contains
  valid documents with UUIDs.

Change History:
* UTC times supported for 'SubmitObjectsRequest' times. These values are extracted from the CDA root
  document and support the following formats defined in the 'DateParsePatterns' enum. Note that all
  UTC times will conform to the following format:
    YYYY[MM[DD[hh[mm[ss]]]]]
  This means that all timezones, if present will be applied in the conversion and any fractional second
  precision will be dropped from the date/time string.
* 'service[Start/Stop]Time' for 'SubmitObjectRequests' are now derived from the CDA root document,
  if present at the following locations:
    serviceStartTime = '/ClinicalDocument/componentOf/encompassingEncounter/effectiveTime/low/@value'
    serviceStopTime = '/ClinicalDocument/componentOf/encompassingEncounter/effectiveTime/high/@value'


0.4 (DRAFT)
-----------
This is a draft release of sample code the PCEHR interfaces.
This release works against the current interfaces.

Known Issues:
- CDA documents <id> root value must be an OID, for document uploads. Other
  operations may accept both. Note that some test data in the SVT environment
  may consist of valid documents with UUIDs.

Change History:
* Updated template FormatCodes to reflect the current values supported in
  the SVT environment.
+ Added an additional helper method for the 'UploadDocumentClient' to allow a document
  to be replaced.


0.3 (DRAFT)
-----------
This is a draft release of sample code the PCEHR interfaces.
This release works against the current interfaces.

Known Issues:
- CDA documents <id> root value must be an OID.
- UploadDocumentMetadata operations are not supported in this release.

Change History:
* Updated Template FormatCodes ID values to match current test IDs.


0.2 (DRAFT)
-----------
This is a draft release of the sample code for the PCEHR interfaces.
This version includes soap request signing.


0.1 (DRAFT)
-----------
This is a draft release of sample code the PCEHR interfaces.
This release works against the current interfaces

Known issues include:
1)	CDA documents must follow older IGs and the <id> root value must be an OID
2)	getAuditView has a deserialization issue
