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

namespace MyWebApi.Builders.HttpActionResults.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.HttpActionResults.Json;
    using Exceptions;
    using Models;
    using Newtonsoft.Json;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class JsonTestBuilder<TActionResult> : BaseResponseModelTestBuilder<TActionResult>, IAndJsonTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public JsonTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        /// <summary>
        /// Tests whether JSON result has the default UTF8 encoding.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithDefaultEncoding()
        {
            return this.WithEncoding(new UTF8Encoding(false, true));
        }

        /// <summary>
        /// Tests whether JSON result has the provided encoding.
        /// </summary>
        /// <param name="encoding">Expected encoding to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithEncoding(Encoding encoding)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualEncoding = this.GetActionResultAsDynamic().Encoding as Encoding;
                if (!encoding.Equals(actualEncoding))
                {
                    throw new JsonResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected JSON result encoding to be {2}, but instead received {3}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        this.GetEncodingName(encoding),
                        this.GetEncodingName(actualEncoding)));
                }
            });

            return this;
        }

        /// <summary>
        /// Tests whether JSON result has the default JSON serializer settings.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithDefaulJsonSerializerSettings()
        {
            return this.WithJsonSerializerSettings(s => this.PopulateFullJsonSerializerSettingsTestBuilder(s));
        }

        /// <summary>
        /// Tests whether JSON result has the provided JSON serializer settings.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected JSON serializer settings to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.WithJsonSerializerSettings(s => this.PopulateFullJsonSerializerSettingsTestBuilder(s, jsonSerializerSettings));
        }

        /// <summary>
        /// Tests whether JSON result has JSON serializer settings by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating JSON serializer settings.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder)
        {
            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualJsonSerializerSettings =
                this.GetActionResultAsDynamic().SerializerSettings as JsonSerializerSettings;

                var newJsonSerializerSettingsTestBuilder = new JsonSerializerSettingsTestBuilder();
                jsonSerializerSettingsBuilder(newJsonSerializerSettingsTestBuilder);
                var expectedJsonSerializerSettings = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettings();

                var validations = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettingsValidations();
                this.ValidateJsonSerializerSettings(
                    expectedJsonSerializerSettings,
                    actualJsonSerializerSettings,
                    validations);
            });

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining JSON result tests.
        /// </summary>
        /// <returns>JSON result test builder.</returns>
        public IJsonTestBuilder AndAlso()
        {
            return this;
        }

        private string GetEncodingName(Encoding encoding)
        {
            var fullEncodingName = encoding.ToString();
            var lastIndexOfDot = fullEncodingName.LastIndexOf(".", StringComparison.Ordinal);
            return fullEncodingName.Substring(lastIndexOfDot + 1);
        }

        private void ValidateJsonSerializerSettings(
            JsonSerializerSettings expectedSettings,
            JsonSerializerSettings actualSettings,
            IEnumerable<Func<JsonSerializerSettings, JsonSerializerSettings, bool>> validations)
        {
            if (validations.Any(v => !v(expectedSettings, actualSettings)))
            {
                throw new JsonResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result serializer settings to equal the provided ones, but were in fact different.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
        }

        private void PopulateFullJsonSerializerSettingsTestBuilder(
            IJsonSerializerSettingsTestBuilder jsonSerializerSettingsTestBuilder,
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            var contractResolver = jsonSerializerSettings != null ? jsonSerializerSettings.ContractResolver : null;
            jsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings();
            jsonSerializerSettingsTestBuilder
                .WithCulture(jsonSerializerSettings.Culture)
                .WithContractResolver(contractResolver)
                .WithConstructorHandling(jsonSerializerSettings.ConstructorHandling)
                .WithDateFormatHandling(jsonSerializerSettings.DateFormatHandling)
                .WithDateParseHandling(jsonSerializerSettings.DateParseHandling)
                .WithDateTimeZoneHandling(jsonSerializerSettings.DateTimeZoneHandling)
                .WithDefaultValueHandling(jsonSerializerSettings.DefaultValueHandling)
                .WithFormatting(jsonSerializerSettings.Formatting)
                .WithMaxDepth(jsonSerializerSettings.MaxDepth)
                .WithMissingMemberHandling(jsonSerializerSettings.MissingMemberHandling)
                .WithNullValueHandling(jsonSerializerSettings.NullValueHandling)
                .WithObjectCreationHandling(jsonSerializerSettings.ObjectCreationHandling)
                .WithPreserveReferencesHandling(jsonSerializerSettings.PreserveReferencesHandling)
                .WithReferenceLoopHandling(jsonSerializerSettings.ReferenceLoopHandling)
                .WithTypeNameAssemblyFormat(jsonSerializerSettings.TypeNameAssemblyFormat)
                .WithTypeNameHandling(jsonSerializerSettings.TypeNameHandling);
        }
    }
}
