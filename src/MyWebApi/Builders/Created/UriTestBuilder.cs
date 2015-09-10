namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts.Created;

    public class UriTestBuilder : IAndUriTestBuilder
    {
        private readonly MockedUri mockedUri;
        private readonly ICollection<Func<MockedUri, Uri, bool>> validations; 

        public UriTestBuilder()
        {
            this.mockedUri = new MockedUri();
            this.validations = new List<Func<MockedUri, Uri, bool>>();
        }

        public IAndUriTestBuilder WithHost(string host)
        {
            this.mockedUri.Host = host;
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            return this;
        }

        public IAndUriTestBuilder WithPort(int port)
        {
            this.mockedUri.Port = port;
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            return this;
        }

        public IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.mockedUri.AbsolutePath = absolutePath;
            this.validations.Add((expected, actual) => expected.AbsolutePath == actual.AbsolutePath);
            return this;
        }

        public IAndUriTestBuilder WithScheme(string scheme)
        {
            this.mockedUri.Scheme = scheme;
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            return this;
        }

        public IAndUriTestBuilder WithQuery(string query)
        {
            this.mockedUri.Query = query;
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            return this;
        }

        public IAndUriTestBuilder WithFragment(string fragment)
        {
            this.mockedUri.Fragment = fragment;
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            return this;
        }

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
