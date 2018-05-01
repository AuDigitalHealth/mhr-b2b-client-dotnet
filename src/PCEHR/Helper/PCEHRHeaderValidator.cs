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
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR.DocumentRegistry;

namespace Nehta.VendorLibrary.PCEHR.Helper
{
    /// <summary>
    /// Validates the PCEHR header.
    /// </summary>
    internal static class PCEHRHeaderValidator
    {
        /// <summary>
        /// Validates the passed header.
        /// </summary>
        /// <typeparam name="T">PCEHR header type.</typeparam>
        /// <param name="pcehrHeader">PCEHR header.</param>
        internal static void Validate<T>(T pcehrHeader)
        {
            Validation.ValidateArgumentRequired("pcehrHeader", pcehrHeader);

            ValidatePcehrHeader(ConvertPcehrHeader(pcehrHeader));
        }

        /// <summary>
        /// Validates the header. 
        /// </summary>
        /// <param name="pcehrHeader">PCEHR header.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        private static void ValidatePcehrHeader(PCEHRHeader pcehrHeader)
        {
            Validation.ValidateArgumentRequired("User", pcehrHeader.User);
            Validation.ValidateArgumentRequired("User.ID", pcehrHeader.User.ID);
            Validation.ValidateArgumentRequired("User.userName", pcehrHeader.User.userName);

            if (pcehrHeader.User.IDType == PCEHRHeaderUserIDType.HPII)
            {
                Validation.ValidateStringLength("pcehrHeader.User.ID", pcehrHeader.User.ID, 16, true);
            }

            Validation.ValidateArgumentRequired("productType", pcehrHeader.productType);
            Validation.ValidateArgumentRequired("productType.platform", pcehrHeader.productType.platform);
            Validation.ValidateArgumentRequired("productType.productName", pcehrHeader.productType.productName);
            Validation.ValidateArgumentRequired("productType.productVersion", pcehrHeader.productType.productVersion);
            Validation.ValidateArgumentRequired("productType.vendor", pcehrHeader.productType.vendor);

            if (pcehrHeader.accessingOrganisation != null)
            {
                Validation.ValidateArgumentRequired("accessingOrganisation.organisationID", pcehrHeader.accessingOrganisation.organisationID);
                Validation.ValidateArgumentRequired("accessingOrganisation.organisationName", pcehrHeader.accessingOrganisation.organisationName);

                Validation.ValidateStringLength("accessingOrganisation.organisationID", pcehrHeader.accessingOrganisation.organisationID, 16, true);
            }

            if (pcehrHeader.ihiNumber != null)
            {
                Validation.ValidateStringLength("ihiNumber", pcehrHeader.ihiNumber, 16, true);
            }
        }

        /// <summary>
        /// Converts a PCEHR header into a common PCEHR header that can be validated.
        /// </summary>
        /// <typeparam name="T">PCEHR header type.</typeparam>
        /// <param name="header">PCEHR header.</param>
        /// <returns>Converted header.</returns>
        private static PCEHRHeader ConvertPcehrHeader<T>(T header)
        {
            Debug.Assert(typeof(T).IsClass);
            Debug.Assert(typeof(T).Name == "PCEHRHeader");

            // Serialize
            MemoryStream memoryStream = new MemoryStream();
            //Updated to use memory safe version
            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializer serializer = CachingXmlSerializerFactory.Create(typeof(T));

            serializer.Serialize(memoryStream, header);
            memoryStream.Position = 0;

            // Deserialize
            //Updated to use memory safe version
            //XmlSerializer deserializer = new XmlSerializer(typeof(PCEHRHeader));
            XmlSerializer deserializer = CachingXmlSerializerFactory.Create(typeof(PCEHRHeader));
            PCEHRHeader pcehrHeader = (PCEHRHeader)deserializer.Deserialize(memoryStream);
            
            return pcehrHeader;
        }

    }
}
