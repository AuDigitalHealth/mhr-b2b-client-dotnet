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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GetDocumentList' client.
    /// </summary>
    /// See the Document Exchange Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// 
    public class GetDocumentListClientSample
    {

        public void Sample()
        {
            // NASH certificate should be used here, NOT the HI certificate the NASH certificate can be found in the NASH PKI Test Kit
            // certificate needs to be installed in the right place
            // The "Issue To" field of a NASH certificate looks like general (or something different)."HPI-O".electronichealth.net.au
            // "Serial Number" can be found in the details once the certificate is installed.e.g. in Windows, certificates can be found in Certs.msc

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
            // Override this value to the current patient's IHI.
            header.IhiNumber = "IHI";

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/getDocumentList"
            // production endpoint is "https://services.ehealth.gov.au/getDocumentList"
            GetDocumentListClient documentListClient = new GetDocumentListClient(new Uri("https://GetDocumentListEndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            // Create a query 
            AdhocQueryBuilder adhocQueryBuilder = new AdhocQueryBuilder("patient IHI", new[] { DocumentStatus.Approved });

            // To further filter documents, build on the adhocQueryBuilder helper functions
            // For example, filtering on document type
            // adhocQueryBuilder.ClassCode = new List<ClassCodes>() {ClassCodes.DischargeSummary};
            // See Table 3 XDSDocumentEntry Document Type and Class Code value set from 
            // the Document Exchange Service Technical Service Specification

            // New option to filter document subtypes as strings that are not defined in the ENUM ClassCodes
            // Tuple<string, string> subtype = new Tuple<string, string>("100.32016", "NCTIS Data Components");
            // adhocQueryBuilder.SubTypeCode = new List<Tuple<string, string>>();
            // adhocQueryBuilder.SubTypeCode.Add(subtype);

            // Create the request using the query
            AdhocQueryRequest queryRequest = adhocQueryBuilder.BuildRequest();
            
            
            try
            {
                // Invoke the service
                AdhocQueryResponse queryResponse = documentListClient.GetDocumentList(header, queryRequest);

                /////////////////////////////////////////////////////////////////////////////////////////

                // If you want to handle Document Views different to documents in your UI, use the following code to split them out

                // XDS.b Constants
                var authorInstitution = "My Health Record^^^^^^^^^1.2.36.1.2001.1007.1.8003640001000036";
                var authorClassification = "urn:uuid:93606bcf-9494-43ec-9b4e-a7748d1a838d";

                // Documents Only (Filter our Document Views (ie Medicare Overviews, Medicines View, Pathology Overview, Diagnostic Imaging Overview, Immunisation Consolidated View))
                var listOfDocuments = queryResponse.RegistryObjectList.ExtrinsicObject.Where(r => r.Classification.Any(c =>
                        c.classificationScheme == authorClassification && c.Slot.Any(s => s.name == "authorInstitution" && !s.ValueList.Value[0].Equals(authorInstitution)))).ToArray();

                // Document Views only (Filter out documents)
                var listOfDocumentViews = queryResponse.RegistryObjectList.ExtrinsicObject.Where(r => r.Classification.Any(c =>
                        c.classificationScheme == authorClassification && c.Slot.Any(s => s.name == "authorInstitution" && s.ValueList.Value[0].Equals(authorInstitution)))).ToArray();

                // For other views, see GetViewClient.cs

                /////////////////////////////////////////////////////////////////////////////////////////

                // Process data into a more simple model - use alternative objects if handling views
                XdsRecord[] data = XdsMetadataHelper.ProcessXdsMetadata(queryResponse.RegistryObjectList.ExtrinsicObject);
                //XdsRecord[] dataForDocsOnly = XdsMetadataHelper.ProcessXdsMetadata(listOfDocuments);
                //XdsRecord[] dataForDocViews = XdsMetadataHelper.ProcessXdsMetadata(listOfDocumentViews);

                // For displaying the data in a list
                foreach (var row in data)
                {
                    // Convert dates from UTC to local time
                    //row.creationTimeUTC.ToLocalTime();
                    //row.serviceStopTimeUTC.ToLocalTime();

                    // Document name
                    //row.classCodeDisplayName 

                    // Organisation
                    //row.authorInstitution.institutionName 

                    // Organisation Type
                    //row.healthcareFacilityTypeCodeDisplayName

                    // Identifiers to retrieve the document
                    //row.repositoryUniqueId  
                    //row.documentId 
                }


                // Get the soap request and response
                string soapRequest = documentListClient.SoapMessages.SoapRequest;
                string soapResponse = documentListClient.SoapMessages.SoapResponse;
            }
            catch (FaultException e)
            {
                // Handle any errors
            }
        }

        private bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Checks can be done here to validate the service certificate.
            // If the service certificate contains any problems or is invalid, return false. Otherwise, return true.
            // This example returns true to indicate that the service certificate is valid.
            return true;
        }
    }
}
