// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Uris
{
    /// <summary>
    /// Used for adding AndAlso() method to the the URI tests.
    /// </summary>
    public interface IAndUriTestBuilder : IUriTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining URI tests.
        /// </summary>
        /// <returns>The same URI test builder.</returns>
        IUriTestBuilder AndAlso();
    }
}
