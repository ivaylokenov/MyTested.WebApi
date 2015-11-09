// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Routes
{
    /// <summary>
    /// Used for adding And() method to the route request builder.
    /// </summary>
    public interface IAndShouldMapTestBuilder : IShouldMapTestBuilder
    {
        /// <summary>
        /// And method for better readability when building route HTTP request message.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        IShouldMapTestBuilder And();
    }
}
