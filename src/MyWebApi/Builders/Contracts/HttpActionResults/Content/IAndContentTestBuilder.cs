// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Content
{
    /// <summary>
    /// Used for adding AndAlso() method to the the content response tests.
    /// </summary>
    public interface IAndContentTestBuilder : IContentTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining content tests.
        /// </summary>
        /// <returns>The same content test builder.</returns>
        IContentTestBuilder AndAlso();
    }
}
