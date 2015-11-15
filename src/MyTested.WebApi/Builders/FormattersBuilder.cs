// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Formatting;
    using Contracts.Formatters;

    /// <summary>
    /// Used for testing media type formatters in a created result.
    /// </summary>
    public class FormattersBuilder : IAndFormattersBuilder
    {
        private readonly ICollection<MediaTypeFormatter> mediaTypeFormatters;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattersBuilder" /> class.
        /// </summary>
        public FormattersBuilder()
        {
            this.mediaTypeFormatters = new List<MediaTypeFormatter>();
        }

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <param name="mediaTypeFormatter">Instance of MediaTypeFormatter.</param>
        /// <returns>The same formatters test builder.</returns>
        public IAndFormattersBuilder ContainingMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            this.mediaTypeFormatters.Add(mediaTypeFormatter);
            return this;
        }

        /// <summary>
        /// Tests whether created result contains the provided media type formatter.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Instance of MediaTypeFormatter.</typeparam>
        /// <returns>The same formatters test builder.</returns>
        public IAndFormattersBuilder ContainingMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ContainingMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// AndAlso method for better readability when chaining formatters tests.
        /// </summary>
        /// <returns>The same formatters test builder.</returns>
        public IFormattersBuilder AndAlso()
        {
            return this;
        }

        internal ICollection<MediaTypeFormatter> GetMediaTypeFormatters()
        {
            return this.mediaTypeFormatters;
        }
    }
}
