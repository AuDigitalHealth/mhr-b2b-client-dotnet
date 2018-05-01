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

using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Author search criteria.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Family name.
        /// </summary>
        public string Family { get; private set; }

        /// <summary>
        /// Given name.
        /// </summary>
        public string Given { get; private set; }

        /// <summary>
        /// Name prefix.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// Unqualified Hpii.
        /// </summary>
        public string Hpii { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="family">Family name.</param>
        /// <param name="given">Given name.</param>
        /// <param name="prefix">Name prefix.</param>
        /// <param name="hpii">Unqualified Hpii.</param>
        public Author(string family, string given, string prefix, string hpii)
        {
            Validation.ValidateArgumentRequired("family", family);
            Validation.ValidateArgumentRequired("given", given);
            Validation.ValidateArgumentRequired("prefix", prefix);
            Validation.ValidateStringLength("hpii", hpii, 16, true);

            Family = family;
            Given = given;
            Prefix = prefix;
            Hpii = hpii;
        }
    }
    
}
