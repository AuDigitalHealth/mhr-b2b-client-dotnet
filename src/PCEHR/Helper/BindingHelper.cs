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
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;

namespace Nehta.VendorLibrary.PCEHR
{
    /// <summary>
    /// Helper class used for creating the WCF bindings used by the clients.
    /// </summary>
    public static class BindingHelper
    {
        /// <summary>
        /// Creates the standard TLS binding configuration.
        /// </summary>
        /// <returns>TLS binding configuration.</returns>
        public static CustomBinding CreateTlsBinding()
        {
            // Set up binding
            var tlsBinding = new CustomBinding();

            var tlsEncoding = new TextMessageEncodingBindingElement();
            tlsEncoding.ReaderQuotas.MaxDepth = 2147483647;
            tlsEncoding.ReaderQuotas.MaxStringContentLength = 2147483647;
            tlsEncoding.ReaderQuotas.MaxArrayLength = 2147483647;
            tlsEncoding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            tlsEncoding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            tlsBinding.OpenTimeout = new TimeSpan(0, 0, 1, 0);
            tlsBinding.ReceiveTimeout = new TimeSpan(0, 0, 1, 0);
            tlsBinding.SendTimeout = new TimeSpan(0, 0, 1, 0);
            tlsBinding.CloseTimeout = new TimeSpan(0, 0, 1, 0);

            var httpsTransport = new HttpsTransportBindingElement();
            httpsTransport.RequireClientCertificate = true;
            httpsTransport.MaxReceivedMessageSize = 2147483647;
            httpsTransport.MaxBufferSize = 2147483647;

            tlsBinding.Elements.Add(tlsEncoding);
            tlsBinding.Elements.Add(httpsTransport);

            return tlsBinding;
        }

        /// <summary>
        /// Creates the standard TLS MTOM binding configuration.
        /// </summary>
        /// <returns>MTOM binding configuration.</returns>
        public static WSHttpBinding CreateMtomTlsBinding()
        {
            // Set up binding
            var binding = new WSHttpBinding();

            binding.MaxBufferPoolSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MessageEncoding = WSMessageEncoding.Mtom;

            binding.ReaderQuotas.MaxDepth = 2147483647;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            binding.OpenTimeout = new TimeSpan(0, 0, 1, 0);
            binding.ReceiveTimeout = new TimeSpan(0, 0, 3, 0);
            binding.SendTimeout = new TimeSpan(0, 0, 3, 0);
            binding.CloseTimeout = new TimeSpan(0, 0, 1, 0);

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

            return binding;
        }
    }
}
