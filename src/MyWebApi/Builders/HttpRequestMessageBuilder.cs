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

namespace MyWebApi.Builders
{
    using System;
    using System.Net.Http;
    using Contracts.HttpRequests;
    using Contracts.Uri;
    using Exceptions;
    using Uris;
    using Utilities.Validators;

    public class HttpRequestMessageBuilder : IAndHttpRequestMessageBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        public HttpRequestMessageBuilder()
        {
            this.requestMessage = new HttpRequestMessage();
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(string requestLocation)
        {
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                requestLocation,
                ThrowNewInvalidHttpRequestMessageException);

            return this;
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(Uri requestLocation)
        {
            this.requestMessage.RequestUri = requestLocation;
            return this;
        }

        public IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> requestUriTestBuilder)
        {
            var uriTestBuilder = new MockedUriBuilder();
            requestUriTestBuilder(uriTestBuilder);
            this.requestMessage.RequestUri = uriTestBuilder.GetUri();
            return this;
        }

        public IHttpRequestMessageBuilder And()
        {
            return this;
        }

        private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        {
            throw new InvalidHttpRequestMessageException(string.Format(
                "When building HttpRequestMessage expected {0} to be {1}, but instead received {2}",
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
