// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Attributes
{
    /// <summary>
    /// Used for adding AndAlso() method to the the attribute tests.
    /// </summary>
    public interface IAndControllerAttributesTestBuilder : IControllerAttributesTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        IControllerAttributesTestBuilder AndAlso();
    }
}
