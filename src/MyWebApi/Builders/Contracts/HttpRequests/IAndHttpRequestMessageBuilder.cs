// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.HttpRequests
{
    /// <summary>
    /// Used for adding AndAlso() method to the the HTTP request message builder.
    /// </summary>
    public interface IAndHttpRequestMessageBuilder : IHttpRequestMessageBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building HTTP request message.
        /// </summary>
        /// <returns>The same HTTP request message builder.</returns>
        IHttpRequestMessageBuilder AndAlso();
    }
}
