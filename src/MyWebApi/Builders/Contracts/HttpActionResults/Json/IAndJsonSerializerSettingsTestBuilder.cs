// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.HttpActionResults.Json
{
    /// <summary>
    /// Used for testing JSON serializer settings in a JSON result with AndAlso() method.
    /// </summary>
    public interface IAndJsonSerializerSettingsTestBuilder : IJsonSerializerSettingsTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining JSON serializer settings test builder.
        /// </summary>
        /// <returns>JSON serializer settings test builder.</returns>
        IJsonSerializerSettingsTestBuilder AndAlso();
    }
}
