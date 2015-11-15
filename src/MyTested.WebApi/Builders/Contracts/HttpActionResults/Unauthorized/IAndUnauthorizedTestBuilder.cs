// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Unauthorized
{
    /// <summary>
    /// Used for adding AndAlso() method to the the unauthorized response tests.
    /// </summary>
    public interface IAndUnauthorizedTestBuilder : IUnauthorizedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining unauthorized result tests.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedTestBuilder AndAlso();
    }
}
