// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Json
{
    /// <summary>
    /// Used for adding AndAlso() method to the the JSON response tests.
    /// </summary>
    public interface IAndJsonTestBuilder : IJsonTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining JSON result tests.
        /// </summary>
        /// <returns>JSON result test builder.</returns>
        IJsonTestBuilder AndAlso();
    }
}
