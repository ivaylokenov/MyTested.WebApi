// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Common.Servers
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// HTTP message handler to process pipeline test cases.
    /// </summary>
    public class ServerHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpMessageInvoker httpMessageInvoker;
        private readonly bool disposeInvoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHttpMessageHandler" /> class.
        /// </summary>
        /// <param name="httpMessageInvoker">HTTP message invoker to process the request.</param>
        /// <param name="disposeInvoker">Indicates whether the invoker should be disposed after request processing.</param>
        public ServerHttpMessageHandler(HttpMessageInvoker httpMessageInvoker, bool disposeInvoker)
        {
            this.httpMessageInvoker = httpMessageInvoker;
            this.disposeInvoker = disposeInvoker;
        }

        /// <summary>
        /// Sends the HTTP request to the provided server.
        /// </summary>
        /// <param name="request">HTTP request message to send.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>Task of HttpResponseMessage.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.httpMessageInvoker.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the System.Net.Http.HttpMessageHandler and optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to releases only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.disposeInvoker)
            {
                this.httpMessageInvoker.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
