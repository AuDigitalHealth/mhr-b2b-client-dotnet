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

using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to process the  MHR_Document_Register.xml file
    /// </summary>
    /// 
    /// See the Record Access Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// 
    public class DocumentRegisterClientSample
    {
        public void Sample()
        {
            // Can download the latest file from here:
            // https://github.com/AuDigitalHealth/mhr-document-register
            string filename = @"MHR_Document_Register.xml";

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);

                Nehta.VendorLibrary.PCEHR.DocumentRegister.DocumentRegister documentRegister = new Nehta.VendorLibrary.PCEHR.DocumentRegister.DocumentRegister();
                documentRegister = (Nehta.VendorLibrary.PCEHR.DocumentRegister.DocumentRegister)DeserialiseElementToClass(xmlDoc.DocumentElement, documentRegister);

                // Informational tags
                foreach (var tag in documentRegister.tags)
                {
                    // Document Type tags
                }

                // Extract data to a database
                foreach (var docType in documentRegister.documentTypes)
                {
                    // Document Type (XDS:DocClass)
                    var documentCode = docType.code;
                    var documentCodeSystem = docType.codeSystem;
                    var documentCodeSystemName = docType.codeSystemName;

                    foreach (var subType in docType.documentSubTypes)
                    {
                        // Document Subtype (XDS:DocType)
                        var documentTypeCode = subType.code;
                        var documentTypeCodeSystem = subType.codeSystem;
                        var documentTypeCodeSystemName = subType.codeSystemName;
                    }
                }


            }
            catch (FaultException fex)
            {
                // Handle any errors
            }            
        }

        private object DeserialiseElementToClass(XmlElement element, object doctype)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.ImportNode(element, true));
            //doc.Save(@"d:\Element-FromMCA.xml");
            XmlReader read = doc.CreateNavigator().ReadSubtree();
            XmlRootAttribute rootAttr = new XmlRootAttribute(element.LocalName);
            rootAttr.Namespace = element.NamespaceURI;
            XmlSerializer xs = new XmlSerializer(doctype.GetType(), rootAttr);
            object rv = xs.Deserialize(read);
            return (rv);
        }

    }
}
