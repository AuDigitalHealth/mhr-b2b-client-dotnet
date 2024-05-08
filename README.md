# Introduction
The My Health Record system has a number of B2B SOAP web services, which allow vendors to incorporate My Health Record system functionality in their products. The My Health Record B2B Client Library provides vendors with a sample implementation of these services in both Java and .NET development environments.

The My Health Record B2B Client Library is based on the NEHTA Clinical Document Packaging Library to support the packaging requirements as part of the uploading of clinical documents.

# Content
The My Health Record B2B Client Library simplifies the development process by providing vendors with a sample implementation of how to interact with the My Health Record system. The library implements all B2B interfaces of My Health Record system, as listed below (these are the logical names):
- doesPCEHRExist
- gainPCEHRAccess
- uploadDocument
- retrieveDocument
- uploadDocumentMetadata
- findDocuments
- removeDocument
- getAuditView
- getChangeHistoryView
- getView - supporting 7 views:
    - healthCheckScheduleView
    - medicareOverview
    - observationView
    - prescriptionAndDispenseView
    - healthRecordOverview
    - diagnosticImagingReportView
    - pathologyReportView
- getTemplate
- searchTemplate
- getIndividualDetailsView
- getRepresentativeList
- registerPCEHR

Details of each interface are described and defined in the My Health Record LSS and TSS specifications, as per the references in the above list. The WSDL and schemas [WSDL] for all of the interfaces are also available on the same website.

The library demonstrates how to sign the SOAP header for both TEXT and MTOM encoding, as required by each interface, and how to map data into the XDS metadata components using the clinical document as the source of the information, thus removing the chance of invalidating the SOAP request. Examples are also included on how to invoke each interface.

# Project
This is a software library that provides an example implementation of how to connect up to the My Health Record Web Services client using .NET.

# Setup
- To build the distributable package, Visual Studio must be installed.
- Start up PCEHR.sln

# Solution
The solution consists of several projects, however the main project is the PCEHR project. 
This project contains the code to communicate with the my health record.

# Building and using the library
The solution can be built using 'F6'. 

# Library Usage
Documentation can be found in the sample project.

# Licensing
See [LICENSE](LICENSE.txt) file.
