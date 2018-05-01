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

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Custom attribute for enum values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class CodedValueAttribute : Attribute
    {
        /// <summary>
        /// Coding system name.
        /// </summary>
        public string CodingSystem { get; set; }

        /// <summary>
        /// Coding system OID.
        /// </summary>
        public string CodingSystemOID { get; set; }

        /// <summary>
        /// Concept code.
        /// </summary>
        public string ConceptCode { get; set; }

        /// <summary>
        /// Concept name.
        /// </summary>
        public string ConceptName { get; set; }

        /// <summary>
        /// Alternate name.
        /// </summary>
        public string AlternateName { get; set; }
    }
}
