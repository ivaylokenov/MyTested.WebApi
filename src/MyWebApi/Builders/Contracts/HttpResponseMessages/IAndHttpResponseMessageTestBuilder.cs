// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpResponseMessages
{
    /// <summary>
    /// Used for adding AndAlso() method to the the HTTP response message tests.
    /// </summary>
    public interface IAndHttpResponseMessageTestBuilder : IHttpResponseMessageTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder AndAlso();
    }
}
