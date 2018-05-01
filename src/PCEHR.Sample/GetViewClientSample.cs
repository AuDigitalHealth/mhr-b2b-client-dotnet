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
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.GetView;
using Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView;
using Nehta.VendorLibrary.PCEHR.HealthRecordOverview;
using Nehta.VendorLibrary.PCEHR.PathologyReportView;
using Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView;
using Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;
using Nehta.VendorLibrary.PCEHR.DocumentRepository;
using Nehta.VendorLibrary.PCEHR.HealthCheckScheduleView;
using Nehta.VendorLibrary.PCEHR.MedicareOverview;
using Nehta.VendorLibrary.PCEHR.ObservationView;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GetAuditViewClient' client.
    /// </summary>
    /// See the View Service Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015

    /// Ensure GainAccess has already been called. (see GainPCEHRAccessClientSample.cs)
    /// The getView Service has two sets of Views:
    ///  CDA based Views. Which can be rendered using the CDA Stylesheet
    ///      - Prescription and Dispense View (Prescription and Dispense View stylesheet)
    ///      - Medicare Overview (Generic Stylesheet)
    ///      - Observation View (Generic Stylesheet)
    ///      - Health Check Schedule View (Generic Stylesheet)
    ///  XML based Views. Which must be parsed and rendered to a native or HTML view
    ///  -   Pathology Report View
    ///  -   Diagnostic Imaging Report View
    ///  -   Health Record Overview
    /// 
    public class GetViewClientSample
    {

        // Sample code for these CDA response getViews
        
        // Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView.prescriptionAndDispenseView
        // Nehta.VendorLibrary.PCEHR.MedicareOverview.medicareOverview
        // Nehta.VendorLibrary.PCEHR.HealthCheckScheduleView.healthCheckScheduleView
        // Nehta.VendorLibrary.PCEHR.ObservationView.observationView

        public void SampleForCdaDocumentResponses()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
            // production endpoint is "https://services.ehealth.gov.au/getView"
            GetViewClient getViewClient = new GetViewClient(new Uri("https://GetViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                getView request = new getView()
                {
                    // For PrescriptionAndDispenseView
                    view = new prescriptionAndDispenseView()
                    {
                        fromDate = DateTime.Now.AddDays(-10),
                        toDate = DateTime.Now,
                        // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                        // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                        // If the specification doesn't specify the version number then it is 1.0
                        versionNumber = "Version number here"
                    }

                    // For MedicareOverview
                    //view = new medicareOverview()
                    //{
                    //    fromDate = DateTime.Now.AddDays(-10),
                    //    toDate = DateTime.Now,
                    // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                    // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                    // If the specification doesn't specify the version number then it is 1.0
                    //    versionNumber = "Version number here"
                    //}

                    // For HealthCheckScheduleView
                    //view = new healthCheckScheduleView()
                    //{
                    //    jurisdiction = healthCheckScheduleViewJurisdiction.NSW,
                    // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                    // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                    // If the specification doesn't specify the version number then it is 1.0
                    //    versionNumber = "Version number here"
                    //}

                    // For ObservationView
                    //view = new observationView()
                    //{
                    //    fromDate = DateTime.Now.AddDays(-10),
                    //    toDate = DateTime.Now,
                    // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                    // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                    // If the specification doesn't specify the version number then it is 1.0
                    //    versionNumber = "Version number here"
                    //    documentSource = observationViewDocumentSource.ALL,
                    //    observationType = observationViewObservationType.WEIGHT,
                    //    referenceData = observationViewReferenceData.CDC
                    //}

                };

                var responseStatus = getViewClient.GetView(header, request);

                // Treat response like a getDocument - unzip package
                if (responseStatus.view != null)
                {
                    var zipfile = responseStatus.view.data;
                    // and Unzip
                }

                // Get the soap request and response
                string soapRequest = getViewClient.SoapMessages.SoapRequest;
                string soapResponse = getViewClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }
        }

        // Sample code for these Custom XML response getViews

        // Nehta.VendorLibrary.PCEHR.HealthRecordOverview.healthRecordOverview
        // Nehta.VendorLibrary.PCEHR.PathologyIndexView.pathologyIndexView
        // Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView.diagnosticImagingReportView
        // Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView.advanceCarePlanningView

        public void SampleForPathXmlResponses()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
            // production endpoint is "https://services.ehealth.gov.au/getView"
            GetViewClient getViewClient = new GetViewClient(new Uri("https://GetViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                getView request = new getView()
                {
                    // Creates a pathologyReportView
                    view = new pathologyReportView()
                    {
                        fromDate = DateTime.Now.AddDays(-10),
                        toDate = DateTime.Now,
                        // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                        // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                        // If the specification doesn't specify the version number then it is 1.0
                        versionNumber = "Version number here"
                    }

                };

                var responseStatus = getViewClient.GetView(header, request);

                // Convert XML response into Class for pathologyReportView
                XmlDocument xml = new XmlDocument();
                xml.PreserveWhitespace = true;
                xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
                pathologyReportViewResponse data = new pathologyReportViewResponse();
                data = (pathologyReportViewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

                // Get the soap request and response
                string soapRequest = getViewClient.SoapMessages.SoapRequest;
                string soapResponse = getViewClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }
        }

        public void SampleForDIXmlResponses()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
            // production endpoint is "https://services.ehealth.gov.au/getView"
            GetViewClient getViewClient = new GetViewClient(new Uri("https://GetViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                getView request = new getView()
                {
                    // Creates a diagnosticImagingReportView
                    view = new diagnosticImagingReportView()
                    {
                        fromDate = DateTime.Now.AddDays(-10),
                        toDate = DateTime.Now,
                        // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                        // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                        // If the specification doesn't specify the version number then it is 1.0
                        versionNumber = "Version number here"
                    }

                };

                var responseStatus = getViewClient.GetView(header, request);

                // Convert XML response into Class for diagnosticImagingReportView
                XmlDocument xml = new XmlDocument();
                xml.PreserveWhitespace = true;
                xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
                diagnosticImagingReportViewResponse data = new diagnosticImagingReportViewResponse();
                data = (diagnosticImagingReportViewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

                // Get the soap request and response
                string soapRequest = getViewClient.SoapMessages.SoapRequest;
                string soapResponse = getViewClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }
        }

        public void SampleForAcpResponse()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
            // production endpoint is "https://services.ehealth.gov.au/getView"
            GetViewClient getViewClient = new GetViewClient(new Uri("https://GetViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                getView request = new getView()
                {
                    // Creates a advanceCarePlanningView
                    view = new advanceCarePlanningView()
                    {
                        // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                        // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                        // If the specification doesn't specify the version number then it is 1.0
                        versionNumber = "Version number here"
                    }
                };

                var responseStatus = getViewClient.GetView(header, request);

                // Convert XML response into Class for advanceCarePlanningView
                XmlDocument xml = new XmlDocument();
                xml.PreserveWhitespace = true;
                xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
                advanceCarePlanningViewResponse data = new advanceCarePlanningViewResponse();
                data = (advanceCarePlanningViewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

                // Get the soap request and response
                string soapRequest = getViewClient.SoapMessages.SoapRequest;
                string soapResponse = getViewClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }
        }

        public void SampleForHroResponse()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getView"
            // production endpoint is "https://services.ehealth.gov.au/getView"
            GetViewClient getViewClient = new GetViewClient(new Uri("https://GetViewEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            try
            {
                getView request = new getView()
                {
                    // Creates a healthRecordOverView
                    view = new healthRecordOverView()
                    {
                        clinicalSynopsisLength = 200,
                        // versionNumber can be found in the "PCEHR View Service - Technical Service Specification" via
                        // https://digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
                        // If the specification doesn't specify the version number then it is 1.0
                        versionNumber = "Version number here"
                    }
                };

                var responseStatus = getViewClient.GetView(header, request);

                // Convert XML response into Class for healthRecordOverview
                XmlDocument xml = new XmlDocument();
                xml.PreserveWhitespace = true;
                xml.LoadXml(Encoding.Default.GetString(responseStatus.view.data));
                healthRecordOverviewResponse data = new healthRecordOverviewResponse();
                data = (healthRecordOverviewResponse)DeserialiseElementToClass(xml.DocumentElement, data);

                // Get the soap request and response
                string soapRequest = getViewClient.SoapMessages.SoapRequest;
                string soapResponse = getViewClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }
        }


        // Sample code for getting Medicine View CDA document
        // Until getView returns Medicine View, two calls need to be made,
        // 1) getDocumentList to retrieve the doc id of the Mediciew View CDA document
        // 2) getDocument

        public void SampleForMedicineViewResponse()
        {
            string documentId = "";
            string repositoryId = "";

            GetDocumentId("IHI Number", out documentId, out repositoryId);

            if (repositoryId != "" && documentId != "")
            {
                RetrieveDocumentSetResponseType response = GetDocument(documentId, repositoryId);

                // response
                if (response.DocumentResponse != null && response.DocumentResponse.Length == 1)
                {
                    //Zip file
                    byte[] zipFile = response.DocumentResponse[0].Document;
                    // Proceed to unzip and render using generic style sheet

                }
            }
        }

        private bool GetDocumentId(string IHI, out string documentId, out string repositoryId)
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Instantiate the client
            GetDocumentListClient documentListClient = new GetDocumentListClient(new Uri("https://GetDocumentListEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            // Create a query 
            AdhocQueryBuilder adhocQueryBuilder = new AdhocQueryBuilder(IHI, new[] { DocumentStatus.Approved });

            // Reduce documents returned to just SHS from yesterday onwards (Medicines View will always be returned in this query)
            // as cannot filter on Medicines View as a document
            adhocQueryBuilder.ClassCode = new List<ClassCodes>();
            adhocQueryBuilder.ClassCode.Add(ClassCodes.SharedHealthSummary);
            adhocQueryBuilder.ServiceStopTimeFrom = new ISO8601DateTime(Convert.ToDateTime(DateTime.Now).AddDays(-1));

            // Create the request using the query
            AdhocQueryRequest queryRequest = adhocQueryBuilder.BuildRequest();

            // Initialise
            repositoryId = "";
            documentId = "";

            try
            {
                // Invoke the service
                AdhocQueryResponse queryResponse = documentListClient.GetDocumentList(header, queryRequest);

                if (queryResponse != null && 
                    queryResponse.status == "urn:oasis:names:tc:ebxml-regrep:ResponseStatusType:Success" &&
                    queryResponse.RegistryObjectList != null)
                {
                    const string XDS_DOCUMENT_ENTRY_CLASS_CODE = "urn:uuid:41a5887f-8865-4c09-adf7-e362475b143a";
                    const string XDS_DOCUMENT_ENTRY_UNIQUE_ID = "urn:uuid:2e82c1f6-a085-4c72-9da3-8640a32e42ab";
                    const string NCTIS_CLASS_CODE_MEDS_VIEW = "100.32002";

                    //Loop through responses
                    foreach (var entry in queryResponse.RegistryObjectList.ExtrinsicObject)
                    {
                        var classification = entry.Classification.FirstOrDefault(o => o.classificationScheme == XDS_DOCUMENT_ENTRY_CLASS_CODE);
                        if (classification != null && classification.nodeRepresentation == NCTIS_CLASS_CODE_MEDS_VIEW)
                        {
                            // Get Values
                            var repid = entry.Slot.FirstOrDefault(o => o.name == "repositoryUniqueId");
                            var docId = entry.ExternalIdentifier.FirstOrDefault(o => o.identificationScheme == XDS_DOCUMENT_ENTRY_UNIQUE_ID);

                            // Set Values
                            repositoryId = (repid != null ? repid.ValueList.Value[0] : "");
                            documentId = (docId != null ? docId.value : "");
                            break;
                        }
                    }
                }
                return true;

            }
            catch (FaultException e)
            {
                // Handle any errors
                return false;
            }
        }

        public RetrieveDocumentSetResponseType GetDocument(string documentId, string repositoryId)
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "Serial Number",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();

            // Create the client
            GetDocumentClient getDocumentClient = new GetDocumentClient(new Uri("https://GetDocumentEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            // Create a request
            List<RetrieveDocumentSetRequestTypeDocumentRequest> request = new List<RetrieveDocumentSetRequestTypeDocumentRequest>();

            // Set the details of the document to retrieve
            request.Add(new RetrieveDocumentSetRequestTypeDocumentRequest()
            {
                DocumentUniqueId = documentId,
                RepositoryUniqueId = repositoryId
            });

            try
            {
                // Invoke the service
                RetrieveDocumentSetResponseType response = getDocumentClient.GetDocument(header, request.ToArray());
                return response;
            }
            catch (FaultException e)
            {
                // Handle any errors
                return null;
            }
        }

        private bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Checks can be done here to validate the service certificate.
            // If the service certificate contains any problems or is invalid, return false. Otherwise, return true.
            // This example returns true to indicate that the service certificate is valid.
            return true;
        }

        private static object DeserialiseElementToClass(XmlElement element, object doctype)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.ImportNode(element, true));
            XmlReader read = doc.CreateNavigator().ReadSubtree();
            XmlRootAttribute rootAttr = new XmlRootAttribute(element.LocalName);
            rootAttr.Namespace = element.NamespaceURI;
            XmlSerializer xs = new XmlSerializer(doctype.GetType(), rootAttr);
            object rv = xs.Deserialize(read);
            return (rv);
        }


    }
}
