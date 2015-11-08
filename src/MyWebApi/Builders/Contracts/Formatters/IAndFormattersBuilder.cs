// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Contracts.Formatters
{
    /// <summary>
    /// Used for adding AndAlso() method to the the formatter tests.
    /// </summary>
    public interface IAndFormattersBuilder : IFormattersBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining formatters tests.
        /// </summary>
        /// <returns>The same formatters test builder.</returns>
        IFormattersBuilder AndAlso();
    }
}
