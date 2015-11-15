// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Routes
{
    /// <summary>
    /// Used for adding AndAlso() method to the route test builder.
    /// </summary>
    public interface IAndResolvedRouteTestBuilder : IResolvedRouteTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building route tests.
        /// </summary>
        /// <returns>The same route builder.</returns>
        IResolvedRouteTestBuilder AndAlso();
    }
}
