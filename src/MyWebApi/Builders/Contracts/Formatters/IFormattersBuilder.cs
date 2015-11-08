// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Formatters
{
    using System.Net.Http.Formatting;

    /// <summary>
    /// Used for testing media type formatters in a created result.
    /// </summary>
    public interface IFormattersBuilder
    {
        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Instance of MediaTypeFormatter.</param>
        /// <returns>The same formatters test builder.</returns>
        IAndFormattersBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Instance of MediaTypeFormatter.</typeparam>
        /// <returns>The same formatters test builder.</returns>
        IAndFormattersBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();
    }
}
