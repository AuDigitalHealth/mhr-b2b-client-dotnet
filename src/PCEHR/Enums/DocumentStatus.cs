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
    /// Status values for CDA documents in the PCEHR.
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// Approved status.
        /// </summary>
        [CodedValueAttribute(ConceptCode = "urn:oasis:names:tc:ebxml-regrep:StatusType:Approved", ConceptName = "Approved")]
        Approved,

        /// <summary>
        /// Submitted status.
        /// </summary>
        [CodedValueAttribute(ConceptCode = "urn:oasis:names:tc:ebxml-regrep:StatusType:Submitted", ConceptName = "Submitted")]
        Submitted,

        /// <summary>
        /// Deprecated status.
        /// </summary>
        [CodedValueAttribute(ConceptCode = "urn:oasis:names:tc:ebxml-regrep:StatusType:Deprecated", ConceptName = "Deprecated")]
        Deprecated,

        /// <summary>
        /// Deleted status.
        /// </summary>
        [CodedValueAttribute(ConceptCode = "urn:orcl.reg:names:StatusType:Deleted", ConceptName = "Deleted")]
        Deleted
    
    }
}
