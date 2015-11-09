// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Servers
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
