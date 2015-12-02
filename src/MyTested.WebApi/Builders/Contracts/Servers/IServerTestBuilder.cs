// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Servers
{
    using HttpResponseMessages;

    /// <summary>
    /// Provides options to test the HTTP response from a server.
    /// </summary>
    public interface IServerTestBuilder
    {
        /// <summary>
        /// Tests for a particular HTTP response message.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpHandlerResponseMessageWithTimeTestBuilder ShouldReturnHttpResponseMessage();
    }
}
