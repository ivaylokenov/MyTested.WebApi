// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Builders.Contracts.HttpActionResults.Json
{
    using System;
    using System.Text;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public interface IJsonTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether JSON result has the default UTF8 encoding.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithDefaultEncoding();

        /// <summary>
        /// Tests whether JSON result has the provided encoding.
        /// </summary>
        /// <param name="encoding">Expected encoding to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithEncoding(Encoding encoding);

        /// <summary>
        /// Tests whether JSON result has the default JSON serializer settings.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithDefaulJsonSerializerSettings();

        /// <summary>
        /// Tests whether JSON result has the provided JSON serializer settings.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected JSON serializer settings to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings);

        /// <summary>
        /// Tests whether JSON result has JSON serializer settings by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating JSON serializer settings.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder);
    }
}
