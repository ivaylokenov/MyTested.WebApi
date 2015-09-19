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

namespace MyWebApi.Builders.Uris
{
    using System;
    using Common;
    using Contracts.Uri;

    using SystemUriBuilder = System.UriBuilder;

    /// <summary>
    /// Used for creating URI.
    /// </summary>
    public class MockedUriBuilder : IAndUriTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedUriBuilder" /> class.
        /// </summary>
        public MockedUriBuilder()
        {
            this.MockedUri = new MockedUri();
        }

        /// <summary>
        /// Gets the built mocked URI instance.
        /// </summary>
        /// <value>Mocked URI.</value>
        protected MockedUri MockedUri { get; private set; }

        /// <summary>
        /// Adds host to the built URI.
        /// </summary>
        /// <param name="host">Host part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithHost(string host)
        {
            this.MockedUri.Host = host;
            return this;
        }

        /// <summary>
        /// Adds port to the built URI.
        /// </summary>
        /// <param name="port">Port part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithPort(int port)
        {
            this.MockedUri.Port = port;
            return this;
        }

        /// <summary>
        /// Adds absolute path to the built URI.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.MockedUri.AbsolutePath = absolutePath;
            return this;
        }

        /// <summary>
        /// Adds scheme to the built URI.
        /// </summary>
        /// <param name="scheme">Scheme part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithScheme(string scheme)
        {
            this.MockedUri.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Adds query string to the built URI.
        /// </summary>
        /// <param name="query">Query part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithQuery(string query)
        {
            this.MockedUri.Query = query;
            return this;
        }

        /// <summary>
        /// Adds fragment to the built URI.
        /// </summary>
        /// <param name="fragment">Document fragment part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public virtual IAndUriTestBuilder WithFragment(string fragment)
        {
            this.MockedUri.Fragment = fragment;
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining URI builder.
        /// </summary>
        /// <returns>The same URI test builder.</returns>
        public IUriTestBuilder AndAlso()
        {
            return this;
        }

        internal MockedUri GetMockedUri()
        {
            return this.MockedUri;
        }

        internal Uri GetUri()
        {
            var uriBuilder = new SystemUriBuilder(
                this.MockedUri.Scheme,
                this.MockedUri.Host,
                this.MockedUri.Port,
                this.MockedUri.AbsolutePath);

            return uriBuilder.Uri;
        }
    }
}
