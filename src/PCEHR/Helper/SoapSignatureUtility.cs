using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.PCEHR
{
    public class SoapSignatureUtility
    {
        private const string UserNamespace = "http://ns.electronichealth.net.au/pcehr/xsd/common/CommonCoreElements/1.0";

        public static Message SignBodyAndAddressingHeaders(Message message, X509Certificate2 signingCertificate)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriter xmlWriter = XmlWriter.Create(ms);
            message.WriteMessage(xmlWriter);
            xmlWriter.Flush();
            xmlWriter.Close();
            var outputBytes = SignBodyAndAddressingHeaders(ms.ToArray(), signingCertificate);
            ms.Close();

            var outputStream = new MemoryStream(outputBytes);
            var outputReader = XmlReader.Create(outputStream);
            var newMessage = Message.CreateMessage(outputReader, int.MaxValue, message.Version);
            outputReader.Close();
            outputStream.Close();

            return newMessage;
        }

        public static byte[] SignBodyAndAddressingHeaders(byte[] xmlBytes, X509Certificate2 signingCertificate)
        {
            var ms = new MemoryStream(xmlBytes);
            XmlReader reader = XmlReader.Create(ms);
            XmlDocument messageDoc = new XmlDocument();
            messageDoc.Load(reader);
            reader.Close();
            ms.Close();

            XmlElement root = messageDoc.DocumentElement;

            if (signingCertificate != null)
            {
                string soapNs = messageDoc.DocumentElement.NamespaceURI;
                XmlNamespaceManager nm = new XmlNamespaceManager(reader.NameTable);
                nm.AddNamespace("s", soapNs);
                nm.AddNamespace("a", "http://www.w3.org/2005/08/addressing");
                nm.AddNamespace("h", UserNamespace);

                // Create references
                List<string> references = new List<string>();

                // Get the body element and assign ID
                XmlElement bodyElement = (XmlElement)root.SelectSingleNode("//s:Body", nm);
                string bodyId = "body-" + Guid.NewGuid().ToString();
                bodyElement.SetAttribute("xml:id", bodyId);
                references.Add(bodyId);

                // Get the header element and assign ID
                XmlElement headerElement = (XmlElement)root.SelectSingleNode("//s:Header/h:PCEHRHeader", nm);
                string headerId = "header-" + Guid.NewGuid().ToString();
                headerElement.SetAttribute("xml:id", headerId);
                references.Add(headerId);

                // Get the timestamp element and assign ID
                XmlElement productElement = (XmlElement)root.SelectSingleNode("//s:Header/h:timestamp", nm);
                string timestampId = "timestamp-" + Guid.NewGuid().ToString();
                productElement.SetAttribute("xml:id", timestampId);
                references.Add(timestampId);

                if (references.Count > 0)
                {
                    // Create signature
                    XmlElement signatureElement = Sign(root, signingCertificate, references);

                    // Get the SOAP header element
                    XmlElement headerSignatureElement =
                        (XmlElement)root.SelectSingleNode("//s:Header/h:signature", nm);

                    // Import and append the created signature
                    XmlNode signatureNode = messageDoc.ImportNode(signatureElement, true);
                    headerSignatureElement.AppendChild(signatureNode);
                }
            }

            var memoryStream = new MemoryStream();
            var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings()
            {
                Encoding = new UTF8Encoding(false),
                OmitXmlDeclaration = true
            });
            messageDoc.WriteTo(xmlWriter);
            xmlWriter.Flush();

            var outputBytes = memoryStream.ToArray();

            xmlWriter.Close();
            memoryStream.Close();

            return outputBytes;
        }

        static XmlElement Sign(XmlElement element, X509Certificate2 signingCertificate, List<string> references)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(element.OuterXml);

            // Create the signature object
            NehtaSignedXml signedXml = new NehtaSignedXml(xmlDoc);
            signedXml.SigningKey = signingCertificate.PrivateKey;

            // Specify the canonicalization method
            signedXml.Signature.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            // Specify the signature method
            signedXml.Signature.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA1Url;

            // Add all the signing references
            foreach (string signReferenceId in references)
            {
                Reference reference = new Reference();
                reference.Uri = "#" + signReferenceId;
                reference.DigestMethod = SignedXml.XmlDsigSHA1Url;

                // Add the transform
                XmlDsigExcC14NTransform transform = new XmlDsigExcC14NTransform();
                reference.AddTransform(transform);

                // Add the reference
                signedXml.AddReference(reference);
            }

            // Calculate the signature
            signedXml.ComputeSignature();

            // Add the key information to the signature 
            signedXml.KeyInfo = new KeyInfo();
            signedXml.KeyInfo.AddClause(new KeyInfoX509Data(signingCertificate));

            // Return the signature
            return signedXml.GetXml();
        }

        public static SoapMessages.SignatureStatus VerifyXML(string xml)
        {
            XmlDocument env = new XmlDocument();
            env.PreserveWhitespace = true;
            env.LoadXml(xml);

            string soapNs = env.DocumentElement.NamespaceURI;
            XmlNamespaceManager xmlnm = new XmlNamespaceManager(env.NameTable);
            xmlnm.AddNamespace("s", soapNs);
            xmlnm.AddNamespace("def", "http://www.w3.org/2000/09/xmldsig#");
            xmlnm.AddNamespace("h", UserNamespace);

            XmlElement ele = (XmlElement)env.SelectSingleNode("/s:Envelope/s:Header/h:signature/def:Signature", xmlnm);
            if (ele == null)
                return SoapMessages.SignatureStatus.NotPresent;

            NehtaSignedXml signedXml = new NehtaSignedXml(env);
            signedXml.LoadXml(ele);
            bool answer = signedXml.CheckSignature();

            return answer ? SoapMessages.SignatureStatus.Valid : SoapMessages.SignatureStatus.Invalid;
        }
    }
}
