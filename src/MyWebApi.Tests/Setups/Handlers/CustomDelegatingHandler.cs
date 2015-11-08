// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.Setups.Handlers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("CustomHeader"))
            {
                var response = await base.SendAsync(request, cancellationToken);
                response.Headers.Add("CustomHeader", "CustomHeader");
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}
