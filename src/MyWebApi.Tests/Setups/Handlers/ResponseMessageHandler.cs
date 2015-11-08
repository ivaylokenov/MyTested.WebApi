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

namespace MyWebApi.Tests.Setups.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public class ResponseMessageHandler : HttpMessageHandler
    {
        private readonly List<ResponseModel> responseModel;

        public ResponseMessageHandler()
        {
            this.responseModel = TestObjectFactory.GetListOfResponseModels();
        }

        public List<ResponseModel> ResponseModel
        {
            get { return this.responseModel; }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Run(
                () =>
                {
                    if (request.Headers.Contains("StringContent"))
                    {
                        return new HttpResponseMessage
                        {
                            Content = new StringContent("Test string")
                        };
                    }

                    if (request.Headers.Contains("NoContent"))
                    {
                        return new HttpResponseMessage(HttpStatusCode.NoContent);;
                    }

                    if (request.Headers.Contains("NotFound"))
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    if (request.Headers.Contains("FromRequest"))
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, this.responseModel);
                    }

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        ReasonPhrase = "Custom reason phrase",
                        Version = new Version(1, 1),
                        Content = new ObjectContent(this.responseModel.GetType(), this.responseModel, TestObjectFactory.GetCustomMediaTypeFormatter()),
                        RequestMessage = request
                    };

                    response.Headers.Add("TestHeader", "TestHeaderValue");
                    response.Content.Headers.Add("TestHeader", "TestHeaderValue");

                    return response;
                },
                cancellationToken);
        }
    }
}
