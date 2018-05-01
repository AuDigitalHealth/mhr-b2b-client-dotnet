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
using System.Globalization;
using System.Text.RegularExpressions;


namespace Nehta.VendorLibrary.PCEHR
{
    public static class XdsMetadataHelper
    {
        public enum IdType
        {
            Uuid,
            Oid
        }

        public static string UuidToOid(string uuid)
        {
            IdType? idType;
            return UuidToOid(uuid, out idType);
        }       

        public static string GetUtcTime(string timestring)
        {
            var formatTemplate = "yyyyMMddHHmmss.ffff";

            var format = string.Empty;
            var timezoneFormat = string.Empty;

            int plusMinusIndex = timestring.IndexOf("+");
            if (plusMinusIndex < 0) plusMinusIndex = timestring.IndexOf("-");

            if (plusMinusIndex >= 10)
            {
                format = formatTemplate.Substring(0, plusMinusIndex);
                int timezoneLength = timestring.Substring(plusMinusIndex + 1).Length;

                if (timezoneLength != 2 && timezoneLength != 4)
                    throw new FormatException("Timezone must be specified with either 2 or 4 digits.");

                timezoneFormat = "zzzz".Substring(0, timezoneLength);
                var equivalent = DateTime.ParseExact(timestring, format + timezoneFormat, CultureInfo.InvariantCulture);

                // Get output format
                string outputFormat = format;
                if (timezoneLength == 4 && !timestring.EndsWith("00") && format.Length < 12)
                    outputFormat = formatTemplate.Substring(0, 12);
                if (outputFormat.Length > 14)
                    outputFormat = outputFormat.Substring(0, 14);

                return equivalent.ToUniversalTime().ToString(outputFormat);
            }
            else if (plusMinusIndex < 10 && plusMinusIndex > -1)
            {
                return timestring.Substring(0, plusMinusIndex);
            }
            else 
            {
                if (timestring.Length > 14)
                    return timestring.Substring(0, 14);
                else
                    return timestring;
            }
        }

        /* Dont need to use Org.BouncyCastle any more
          
        public static string UuidToOid(string uuid, out IdType? idType)
        {
            idType = null;

            // Example
            // UUID = a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1
            // OID = 2.25.N
            // Where N =  (2^96 * 0x a7b7c3b7) + (2^64 * 0x 463943a9) + (2^32 * 0x 8bb17cb8)) + 0x c91216c1 
            // Correct value  2.25.222935235211552455402395562399683974849

            string answer = uuid;

            // 0 start pos  01234567 9012 4567 9012 456789012345  for SubString
            //string uuid = "a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1";
            // Remove unwanted chars if they exist
            uuid = uuid.Replace("urn:uuid:", "");

            if (Regex.IsMatch(uuid, "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.IgnoreCase))
            {
                idType = IdType.Uuid;
                uuid = uuid.Replace("-", "");

                //Convert hex (16) to decimal
                var num1 = new Org.BouncyCastle.Math.BigInteger(uuid.Substring(0, 8), 16);
                var num2 = new Org.BouncyCastle.Math.BigInteger(uuid.Substring(8, 8), 16);
                var num3 = new Org.BouncyCastle.Math.BigInteger(uuid.Substring(16, 8), 16);
                var num4 = new Org.BouncyCastle.Math.BigInteger(uuid.Substring(24, 8), 16);

                //Multiply by powers
                var num5 = new Org.BouncyCastle.Math.BigInteger("2").Pow(96).Multiply(num1);
                var num6 = new Org.BouncyCastle.Math.BigInteger("2").Pow(64).Multiply(num2);
                var num7 = new Org.BouncyCastle.Math.BigInteger("2").Pow(32).Multiply(num3);

                //Add them up to get answer
                answer = "2.25." + num4.Add(num5).Add(num6).Add(num7).ToString();
            }
            else if (Regex.IsMatch(uuid, "^[0-9]+(\\.[0-9]+)+$"))
            {
                idType = IdType.Oid;
            }

            return answer.ToString();
        }
        */

        public static string UuidToOid(string uuid, out IdType? idType)
        {
            idType = null;

            // Example
            // UUID = a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1
            // OID = 2.25.N
            // Where N =  (2^96 * 0x a7b7c3b7) + (2^64 * 0x 463943a9) + (2^32 * 0x 8bb17cb8)) + 0x c91216c1 
            // Correct value  2.25.222935235211552455402395562399683974849

            string answer = uuid;

            // 0 start pos  01234567 9012 4567 9012 456789012345  for SubString
            //string uuid = "a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1";
            // Remove unwanted chars if they exist
            uuid = uuid.Replace("urn:uuid:", "");

            if (Regex.IsMatch(uuid, "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.IgnoreCase))
            {
                idType = IdType.Uuid;
                uuid = uuid.Replace("-", "");

                //Convert hex (16) to decimal
                //Add "0" to beginning to make sure positive number returned
                answer = "2.25." + System.Numerics.BigInteger.Parse("0" + uuid, NumberStyles.AllowHexSpecifier);
            }
            else if (Regex.IsMatch(uuid, "^[0-9]+(\\.[0-9]+)+$"))
            {
                idType = IdType.Oid;
            }

            return answer.ToString();
        }

    }
}
