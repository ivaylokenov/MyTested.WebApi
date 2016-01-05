// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Extensions
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Provides extension methods to HttpRequestMessage.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Transforms HTTP request URI from relative to absolute with fake host.
        /// </summary>
        /// <param name="request">HTTP request message to transform.</param>
        public static void TransformToAbsoluteRequestUri(this HttpRequestMessage request)
        {
            if (request.RequestUri != null && !request.RequestUri.IsAbsoluteUri)
            {
                request.RequestUri = new Uri(MyWebApi.BaseAddress, request.RequestUri);
            }
        }
    }
}
