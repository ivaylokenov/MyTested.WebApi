// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.HttpActionResults.Unauthorized
{
    /// <summary>
    /// Used for building mocked AuthenticationHeaderValue parameter.
    /// </summary>
    public interface IAuthenticationHeaderValueParameterBuilder
    {
        /// <summary>
        /// Sets parameter to the built authentication header value with the provided string.
        /// </summary>
        /// <param name="parameter">Authentication header value parameter as string.</param>
        void WithParameter(string parameter);
    }
}
