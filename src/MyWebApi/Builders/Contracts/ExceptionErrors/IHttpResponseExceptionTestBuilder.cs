// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.ExceptionErrors
{
    using System.Net;
    using Base;
    using HttpResponseMessages;

    /// <summary>
    /// Used for testing expected HttpResponseException.
    /// </summary>
    public interface IHttpResponseExceptionTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether caught HttpResponseException has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Provides methods to test whether caught HttpResponseException has specific HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder WithHttpResponseMessage();
    }
}
