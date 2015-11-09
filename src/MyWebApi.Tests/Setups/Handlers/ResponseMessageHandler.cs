// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Handlers
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
