// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.HttpActionResults.Created
{
    /// <summary>
    /// Used for adding AndAlso() method to the the created response tests.
    /// </summary>
    public interface IAndCreatedTestBuilder : ICreatedTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining created tests.
        /// </summary>
        /// <returns>The same created test builder.</returns>
        ICreatedTestBuilder AndAlso();
    }
}
