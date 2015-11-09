// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Handlers
{
    using System.Net.Http;
    using Base;
    using HttpResponseMessages;

    /// <summary>
    /// Used for testing HTTP message handlers.
    /// </summary>
    public interface IHttpMessageHandlerTestBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Tests the HTTP handler for returning HTTP response message successfully.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpHandlerResponseMessageTestBuilder ShouldReturnHttpResponseMessage();

        /// <summary>
        /// Gets the HTTP request message used in the testing.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        HttpRequestMessage AndProvideTheHttpRequestMessage();
    }
}
