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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using System.ServiceModel.Dispatcher;
using Nehta.VendorLibrary.Common;
using System.Web;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Utility class to add a custom behavior to an endpoint which will capture the SOAP
    /// requests and responses, and also sign the outgoing messages.
    /// </summary>
    public class PCEHREndpointProcessor
    {
        /// <summary>
        /// Specify the endpoint on which the signing and inspector behavior is to be applied.
        /// </summary>
        /// <param name="endpoint">The service endpoint to process.</param>
        /// <param name="messageID">A UniqueId instance which specifies the ID of the request message.</param>
        /// <param name="signingCertificate">The X509Certificate2 instance which will be used to sign the request message.</param>
        /// <param name="soapMessages">A SoapMessages instance which will contain the soap request and response messages on the endpoint.</param>
        public static void ProcessEndpoint(
            ServiceEndpoint endpoint,
            X509Certificate2 signingCertificate,
            SoapMessages soapMessages)
        {
            // Add a behavior to remove reply-to and set the message id, to, from ws-address details
            InspectorBehavior newBehavior = new InspectorBehavior(soapMessages, signingCertificate);

            // Add the behavior
            endpoint.Behaviors.Add(newBehavior);
        }

        /// <summary>
        /// Implementation of a MessageInspector which populates a SoapMessages instance with the
        /// soap request and response messages. This also signs the request message with the signing certificate.
        /// </summary>
        internal class MessageInspector : IClientMessageInspector
        {
            SoapMessages soapMessages;
            X509Certificate2 signingCertificate;

            public MessageInspector(SoapMessages soapMessages, X509Certificate2 signingCertificate)
            {
                this.soapMessages = soapMessages;
                this.signingCertificate = signingCertificate;
            }

            // TODO: Put this somewhere
            private string GetSoapFromString(string message, out int start)
            {
                var match = Regex.Match(message, @"<([^>]+:)?Envelope.*?>[\S\s]*?</([^>]+:)?Envelope>");

                start = match.Index;

                return message.Substring(match.Index, match.Length);
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
            {
                string startInfo = "application/soap+xml";
                string boundary = string.Format("uuid:{0}+id=1", Guid.NewGuid());
                string startUri = "http://tempuri.org/0";

                // Set message ID
                UniqueId messageId = new UniqueId();
                soapMessages.SoapRequestMessageId = messageId.ToString();

                // Modify MessageId and From headers
                request.Headers.MessageId = messageId;
                request.Headers.From = new EndpointAddress("http://www.w3.org/2005/08/addressing/anonymous");
                request.Headers.To = new Uri("http://www.w3.org/2005/08/addressing/anonymous");

                // Create message buffer (Message needs to be recreated multiple times)
                MessageBuffer msgBuffer = request.CreateBufferedCopy(int.MaxValue);

                // Get assembled soap request and sign
                var messageToSign = msgBuffer.CreateMessage();
                var bigXml = ConvertMessageToString(messageToSign);
                var signedBigXml = Encoding.UTF8.GetString(SoapSignatureUtility.SignBodyAndAddressingHeaders(Encoding.UTF8.GetBytes(bigXml), signingCertificate));
                soapMessages.SoapRequest = signedBigXml;
                               
                // Encoding message into MTOM format using MTOM writer
                request = msgBuffer.CreateMessage();
                var initialMs = new MemoryStream();
                var initialWriter = XmlDictionaryWriter.CreateMtomWriter(initialMs, Encoding.UTF8, int.MaxValue, startInfo, boundary, startUri, true, true);
                request.WriteMessage(initialWriter);
                initialWriter.Flush();

                var originalMessageSize = (int) initialMs.Length;

                // Copy MTOM message into buffer
                byte[] bufferUnsigned = new byte[originalMessageSize];
                Array.Copy(initialMs.GetBuffer(), 0, bufferUnsigned, 0, (int) initialMs.Position);
                string mtomString = Encoding.UTF8.GetString(bufferUnsigned);

                // Get SOAP XML from MTOM message, with start and end index
                int startSoapIndex;
                var unsignedXml = GetSoapFromString(mtomString, out startSoapIndex);
                int endSoapIndex = startSoapIndex + Encoding.UTF8.GetBytes(unsignedXml).Length;
                //int endSoapIndex = startSoapIndex + unsignedXml.Length;

                // If binary MIME parts are found in MTOM message, then replace out base64 content with MTOM include statement
                string signedFinalXml = signedBigXml;
                var partHeaderMatches = Regex.Matches(mtomString, @"((Content-ID: <.*>\s+)|(Content-Transfer-Encoding: binary\s+)|(Content-Type: application/octet-stream\s+)){3}", RegexOptions.IgnoreCase);
                if (partHeaderMatches.Count > 0)
                {
                    for (int x = 0; x < partHeaderMatches.Count; x++)
                    {
                        var contentId = Regex.Match(partHeaderMatches[x].Value, "Content-ID: <(.*?)>");
                        var encodedContentId = HttpUtility.UrlEncode(contentId.Groups[1].Value).Replace(".", @"\.");
                        var unencodedContentId = contentId.Groups[1].Value.Replace(".", @"\.");
                        
                        // var xopIncludeMatch = Regex.Match(unsignedXml, "(<[^>]+?>)(<([^>]+?:)?Include href=\"cid:(" + encodedContentId + ")\"[^>]*?/>)(</[^>]+?>)", RegexOptions.IgnoreCase);

                        var xopIncludeMatch = Regex.Match(unsignedXml, "(<[^>]+?>)(<([^>]+?:)?Include href=\"cid:(" + encodedContentId + "|" + unencodedContentId + ")\"[^>]*?/>)(</[^>]+?>)", RegexOptions.IgnoreCase);

                        signedFinalXml = Regex.Replace(signedFinalXml,
                                                       xopIncludeMatch.Groups[1] + "[^<]*?" + xopIncludeMatch.Groups[5],
                                                       xopIncludeMatch.Groups[0].Value);
                    }
                }
                else
                {
                    signedFinalXml = signedBigXml;
                }

                // Get difference in message length between unsigned and signed SOAP messages
                int diff = Encoding.UTF8.GetBytes(signedFinalXml).Length - Encoding.UTF8.GetBytes(unsignedXml).Length;

                // Create buffer large enough to contain MTOM message with signed SOAP XML
                byte[] bufferSigned = new byte[bufferUnsigned.Length + diff]; 

                // Copy MIME start content (everything before SOAP XML) from unsigned buffer to signed buffer
                Array.Copy(bufferUnsigned, bufferSigned, startSoapIndex);

                // Copy signed SOAP XML into signed buffer after MIME start content
                var signedXmlArray = Encoding.UTF8.GetBytes(signedFinalXml);
                Array.Copy(signedXmlArray, 0, bufferSigned, startSoapIndex, signedXmlArray.Length);

                // Copy MIME end content to after signed SOAP XML
                Array.Copy(bufferUnsigned, endSoapIndex, bufferSigned, startSoapIndex + signedXmlArray.Length, bufferUnsigned.Length - (endSoapIndex + 1));

                var mimeContent = new ArraySegment<byte>(bufferSigned, 0, originalMessageSize + diff).Array;
                soapMessages.MtomRequest = mimeContent;

                // Recreate request (Message) using MTOM reader
                var outputReader = XmlDictionaryReader.CreateMtomReader(mimeContent, 0, mimeContent.Length, Encoding.UTF8, new XmlDictionaryReaderQuotas());
                request = Message.CreateMessage(outputReader, int.MaxValue, request.Version);

                // Dispose things
                msgBuffer.Close();

                return null;
            }

            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
                MessageBuffer msgBuffer = reply.CreateBufferedCopy(int.MaxValue);

                Message msg = msgBuffer.CreateMessage();
                soapMessages.SoapResponse = ConvertMessageToString(msg);

                reply = msgBuffer.CreateMessage();

                msgBuffer.Close();

                // Verify signature on soap response
                try
                {
                    soapMessages.SoapResponseSignatureStatus = SoapSignatureUtility.VerifyXML(soapMessages.SoapResponse);
                }
                catch (Exception ex)
                {
                }
            }

            /// <summary>
            /// Convert the message to a string.
            /// </summary>
            /// <param name="msg">Message to convert.</param>
            /// <returns>Message as a string.</returns>
            private string ConvertMessageToString(System.ServiceModel.Channels.Message msg)
            {
                var ms = new MemoryStream();
                var xw = XmlTextWriter.Create(ms, new XmlWriterSettings()
                {
                    Indent = false,
                    OmitXmlDeclaration = true
                });
                msg.WriteMessage(xw);
                xw.Close();
                ms.Position = 0;

                var sr = new StreamReader(ms);
                var convertedString = sr.ReadToEnd();

                sr.Close();
                ms.Close();

                return convertedString;
            }
        }

        /// <summary>
        /// Implementation of a behavior that instantiates a MessageInspector.
        /// </summary>
        class InspectorBehavior : IEndpointBehavior
        {
            SoapMessages soapMessages;
            X509Certificate2 signingCertificate;

            public InspectorBehavior(SoapMessages soapMessages, X509Certificate2 signingCertificate)
            {
                this.soapMessages = soapMessages;
                this.signingCertificate = signingCertificate;
            }

            public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
            {
            }

            public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
            {
                clientRuntime.MessageInspectors.Add(new MessageInspector(soapMessages, signingCertificate));
            }

            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
            {
            }

            public void Validate(ServiceEndpoint endpoint)
            {
            }
        }
    }
}
