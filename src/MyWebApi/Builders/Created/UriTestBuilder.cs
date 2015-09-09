namespace MyWebApi.Builders.Created
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Contracts.Created;

    public class UriTestBuilder : IAndUriTestBuilder
    {
        private readonly MockedUri mockedUri;
        private readonly ICollection<Func<Uri, MockedUri, bool>> validations; 

        public UriTestBuilder()
        {
            this.mockedUri = new MockedUri();
            this.validations = new List<Func<Uri, MockedUri, bool>>();
        }

        public IAndUriTestBuilder WithHost(string host)
        {
            this.mockedUri.Host = host;
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
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

        internal ICollection<Func<Uri, MockedUri, bool>> GetUriValidations()
        {
            return this.validations;
        }
    }
}
