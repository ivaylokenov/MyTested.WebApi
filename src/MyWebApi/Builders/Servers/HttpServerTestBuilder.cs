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

namespace MyWebApi.Builders.Servers
{
    using System.Net.Http;
    using System.Threading;
    using Contracts.HttpResponseMessages;

    public class HttpServerTestBuilder : BaseServerTestBuilder
    {
        private readonly HttpMessageInvoker client;
        private readonly bool disposeAfterTest;

        public HttpServerTestBuilder(HttpMessageInvoker client, bool disposeAfterTest = false)
        {
            this.client = client;
            this.disposeAfterTest = disposeAfterTest;
        }

        public override IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage()
        {
            var result = this.client.SendAsync(this.HttpRequestMessage, CancellationToken.None).Result;
            if (this.disposeAfterTest)
            {
                this.client.Dispose();
            }

            return null;
        }
    }
}
