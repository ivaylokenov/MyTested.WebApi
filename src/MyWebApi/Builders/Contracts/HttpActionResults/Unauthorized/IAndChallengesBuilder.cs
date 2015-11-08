// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.HttpActionResults.Unauthorized
{
    /// <summary>
    /// Used for building collection of AuthenticationHeaderValue with AndAlso() method.
    /// </summary>
    public interface IAndChallengesBuilder : IChallengesBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining header builders.
        /// </summary>
        /// <returns>The same challenge builder.</returns>
        IChallengesBuilder AndAlso();
    }
}
