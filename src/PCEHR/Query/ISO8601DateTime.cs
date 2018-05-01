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

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Specify a DateTime which will be formatted in ISO8601 compliant string.
    /// Eg: To express "5 Jan 2009, 5:25PM (UTC-04:30)"
    /// var time = new ISO8601DateTime(DateTime.Parse(
    ///     "5 Jan 2009 5:25 PM"),
    ///     Precision.Minute,
    ///     -new TimeSpan(4, 30, 0));
    /// </summary>
    public class ISO8601DateTime
    {
        /// <summary>
        /// Precision of the date/time.
        /// </summary>
        public enum Precision
        {
            /// <summary>
            /// Year e.g. 1992
            /// </summary>
            Year,

            /// <summary>
            /// Month e.g. 199204
            /// </summary>
            Month,

            /// <summary>
            /// Day e.g. 19940402
            /// </summary>
            Day,

            /// <summary>
            /// Hour e.g. 1992040214
            /// </summary>
            Hour,

            /// <summary>
            /// Minute e.g. 199204021420
            /// </summary>
            Minute,

            /// <summary>
            /// Second e.g. 19920402142030
            /// </summary>
            Second,

            /// <summary>
            /// Millisecond e.g. 19920402142030.1244
            /// </summary>
            Millisecond
        }

        /// <summary>
        /// Date/time value.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Timezone of the date/time.
        /// </summary>
        public TimeSpan? TimeZone { get; set; }

        /// <summary>
        /// Defines the prescision of the date/time. This determines how it is stringified into CDA.
        /// </summary>
        public Precision? PrecisionIndicator { get; set; }

        /// <summary>
        /// Creates with date/time.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        public ISO8601DateTime(DateTime dateTime)
        {
            this.DateTime = dateTime;
        }

        /// <summary>
        /// Creates with date/time and precision.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        /// <param name="precisionIndicator">Precision indicator.</param>
        public ISO8601DateTime(DateTime dateTime, Precision precisionIndicator)
        {
            this.DateTime = dateTime;
            this.PrecisionIndicator = precisionIndicator;
        }

        /// <summary>
        /// Creates with date/time, precision and time zone.
        /// </summary>
        /// <param name="dateTime">Date/time.</param>
        /// <param name="precisionIndicator">Precision indicator.</param>
        /// <param name="timeZone">Time zone.</param>
        public ISO8601DateTime(DateTime dateTime, Precision precisionIndicator, TimeSpan timeZone)
        {
            this.DateTime = dateTime;
            this.PrecisionIndicator = precisionIndicator;
            this.TimeZone = timeZone;
        }

        /// <summary>
        /// Returns the string representation of the date/time.
        /// </summary>
        /// <returns>String representation of the ISO 8601 date/time.</returns>
        public new string ToString()
        {
            var format = "yyyyMMddHHmmss";

            switch (PrecisionIndicator)
            {
                case Precision.Year:
                    format = "yyyy";
                    break;
                case Precision.Month:
                    format = "yyyyMM";
                    break;
                case Precision.Day:
                    format = "yyyyMMdd";
                    break;
                case Precision.Hour:
                    format = "yyyyMMddHH";
                    break;
                case Precision.Minute:
                    format = "yyyyMMddHHmm";
                    break;
                case Precision.Second:
                    format = "yyyyMMddHHmmss";
                    break;
                case Precision.Millisecond:
                    format = "yyyyMMddHHmmss.ffff";
                    break;
            }

            var output = this.DateTime.ToString(format);

            if (TimeZone.HasValue)
            {
                output += string.Format("{0}{1:00}{2:00}",
                            TimeZone.Value.TotalMinutes >= 0 ? "+" : "-",
                            Math.Abs(TimeZone.Value.Hours),
                            Math.Abs(TimeZone.Value.Minutes));
            }

            return output;
        }
    }
}