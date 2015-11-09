// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Unauthorized
{
    using System;

    /// <summary>
    /// Used for building collection of AuthenticationHeaderValue.
    /// </summary>
    public interface IChallengesBuilder
    {
        /// <summary>
        /// Adds built header to the collection of authentication header values.
        /// </summary>
        /// <param name="authenticationHeaderValueBuilder">Action providing authentication header value builder.</param>
        /// <returns>The same challenge builder.</returns>
        IAndChallengesBuilder ContainingHeader(Action<IAuthenticationHeaderValueBuilder> authenticationHeaderValueBuilder);
    }
}
