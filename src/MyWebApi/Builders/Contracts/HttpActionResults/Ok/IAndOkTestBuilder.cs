// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.HttpActionResults.Ok
{
    /// <summary>
    /// Used for adding AndAlso() method to the the ok response tests.
    /// </summary>
    public interface IAndOkTestBuilder : IOkTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining ok tests.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IOkTestBuilder AndAlso();
    }
}
