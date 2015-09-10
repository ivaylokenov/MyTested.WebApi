namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts.Created;

    /// <summary>
    /// Used for testing URI location in a created result.
    /// </summary>
    public class UriTestBuilder : IAndUriTestBuilder
    {
        private readonly MockedUri mockedUri;
        private readonly ICollection<Func<MockedUri, Uri, bool>> validations; 

        /// <summary>
        /// Initializes a new instance of the <see cref="UriTestBuilder" /> class.
        /// </summary>
        public UriTestBuilder()
        {
            this.mockedUri = new MockedUri();
            this.validations = new List<Func<MockedUri, Uri, bool>>();
        }

        /// <summary>
        /// Tests whether the URI has the same host as the provided one.
        /// </summary>
        /// <param name="host">Host part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithHost(string host)
        {
            this.mockedUri.Host = host;
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            return this;
        }

        /// <summary>
        /// Tests whether the URI has the same port as the provided one.
        /// </summary>
        /// <param name="port">Port part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithPort(int port)
        {
            this.mockedUri.Port = port;
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            return this;
        }

        /// <summary>
        /// Tests whether the URI has the same absolute path as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.mockedUri.AbsolutePath = absolutePath;
            this.validations.Add((expected, actual) => expected.AbsolutePath == actual.AbsolutePath);
            return this;
        }

        /// <summary>
        /// Tests whether the URI has the same scheme as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithScheme(string scheme)
        {
            this.mockedUri.Scheme = scheme;
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            return this;
        }

        /// <summary>
        /// Tests whether the URI has the same query as the provided one.
        /// </summary>
        /// <param name="query">Query part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithQuery(string query)
        {
            this.mockedUri.Query = query;
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            return this;
        }

        /// <summary>
        /// Tests whether the URI has the same fragment as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public IAndUriTestBuilder WithFragment(string fragment)
        {
            this.mockedUri.Fragment = fragment;
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining URI tests.
        /// </summary>
        /// <returns>The same URI test builder.</returns>
        public IUriTestBuilder AndAlso()
        {
            return this;
        }

        internal MockedUri GetUri()
        {
            return this.mockedUri;
        }

        internal ICollection<Func<MockedUri, Uri, bool>> GetUriValidations()
        {
            return this.validations;
        }
    }
}
