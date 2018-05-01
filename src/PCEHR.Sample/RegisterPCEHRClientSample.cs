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
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.RegisterPCEHR;
using System.IO;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'RegisterPCEHRClient'.
    /// </summary>
    /// To be used when a patient does not have a My Health Record, and you want to register them for one.
    ///
    public class RegisterPCEHRClientSample
    {
        public void Sample()
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
            header.IhiNumber = "IHI"; // This value is required in the absence of demographic information.


            // Create the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/registerPCEHR"
            // production endpoint is "https://services.ehealth.gov.au/registerPCEHR"
            RegisterPCEHRClient registerClient = new RegisterPCEHRClient(
                new Uri("https://RegisterPCEHREndpoint"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            // Create a request
            registerPCEHR request = new registerPCEHR();

            // Set required assertion values
            request.assertions = new registerPCEHRAssertions()
            {
                acceptedTermsAndConditions = true,
                documentConsent = new registerPCEHRAssertionsDocument[]
                {
                    new registerPCEHRAssertionsDocument()
                    {
                        // status is either ConsentGiven or ConsentNotGiven
                        status = registerPCEHRAssertionsDocumentStatus.ConsentGiven,
                        // Types of document include:
                        // ACIR (Australian Childhood Immunisation Register)
                        // AODR (Australian Organ Donor Register)
                        // MBS (Medicare Benefits Report)
                        // PBS (Pharmaceutical Benefits Report)
                        // PBSPastAssimilation, - last 2 years worth
                        // MBSPastAssimilation, - last 2 years worth 
                        type = registerPCEHRAssertionsDocumentType.AODR
                    },
                    // For multiple document types  
                    new registerPCEHRAssertionsDocument()
                    {
                        status = registerPCEHRAssertionsDocumentStatus.ConsentNotGiven,
                        type = registerPCEHRAssertionsDocumentType.ACIR
                    }
                },
                identity = new registerPCEHRAssertionsIdentity()
                {
                    evidenceOfIdentity = new registerPCEHRAssertionsIdentityEvidenceOfIdentity()
                    {
                        // type can be one of IdentityVerificationMethod1 to IdentityVerificationMethod10
                        // the details of each method can be found in Part A of the My Health Record Identification Framework (link below)
                        // https://myhealthrecord.gov.au/internet/mhr/publishing.nsf/Content/healthcare-providers/$file/My-Health-Record-Assisted-Registration-Identification-Framework.pdf
                        type = registerPCEHRAssertionsIdentityEvidenceOfIdentityType.IdentityVerificationMethod5
                    },
                    // indigenousStatus can be one of: Item1, Item2, Item3, Item4 or Item9 the meaning of each of these
                    // items can be found in the Value domain attributes section of http://meteor.aihw.gov.au/content/index.phtml/itemId/291036
                    indigenousStatus = registerPCEHRAssertionsIdentityIndigenousStatus.Item3,
                    // the consent form can be downloaded from:
                    // https://myhealthrecord.gov.au/internet/mhr/publishing.nsf/Content/healthcare-providers/$file/My-Health-Record-Assisted-Registration-Form.pdf for adult
                    // https://myhealthrecord.gov.au/internet/mhr/publishing.nsf/Content/healthcare-providers/$file/My-Health-Record-Assisted-Registration-Form-Child.pdf for child
                    signedConsentForm = File.ReadAllBytes("signedConsentForm.pdf")
                },
                ivcCorrespondence = new registerPCEHRAssertionsIvcCorrespondence()
                {
                    channel = registerPCEHRAssertionsIvcCorrespondenceChannel.email,
                    contactDetails = new contactDetailsType()
                    {
                        emailAddress = "patient@emails.com",
                        mobilePhoneNumber = "patient phone number"
                    }
                },
                representativeDeclaration = false,
                representativeDeclarationSpecified = true
            };

            // Populate a new individual
            request.individual = new registerPCEHRIndividual()
            {
                demographics = new registerPCEHRIndividualDemographics()
                {
                    name = new nameTypeSupp()
                    {
                        givenName = new string[] { "First Name" },
                        familyName = "Family Name",                        
                    },
                    sex = sex.M,
                    dateOfBirth = DateTime.Parse("1 Jan 1978")
                }
            };

            try
            {
                // Invoke the service
                registerPCEHRResponse response = registerClient.RegisterPCEHR(header, request);
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
