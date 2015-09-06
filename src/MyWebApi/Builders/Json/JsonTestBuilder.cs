namespace MyWebApi.Builders.Json
{
    using System;
    using System.Text;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Json;
    using Exceptions;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller..</typeparam>
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

        public IAndJsonTestBuilder WithDefaultEncoding()
        {
            return this.WithEncoding(new UTF8Encoding(false, true));
        }

        public IAndJsonTestBuilder WithEncoding(Encoding encoding)
        {
            var actualEncoding = this.GetActionResultAsDynamic(this.ActionResult).Encoding as Encoding;
            if (!encoding.Equals(actualEncoding))
            {
                throw new JsonResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result encoding to be {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    this.GetEncodingName(encoding),
                    this.GetEncodingName(actualEncoding)));
            }

            return this;
        }

        public IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            var actualJsonSerializerSettings =
                this.GetActionResultAsDynamic(this.ActionResult).SerializerSettings as JsonSerializerSettings;

            if (!this.CompareJsonSerializerSettings(jsonSerializerSettings, actualJsonSerializerSettings))
            {
                throw new JsonResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result serializer settings to equal the provided ones, but were in fact different.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return this;
        }

        public IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsBuilder> jsonSerializerSettingsBuilder)
        {
            var newJsonSerializerSettingsBuilder = new JsonSerializerSettingsBuilder();
            jsonSerializerSettingsBuilder(newJsonSerializerSettingsBuilder);
            var expectedJsonSerializerSettings = newJsonSerializerSettingsBuilder.GetJsonSerializerSettings();
            this.WithJsonSerializerSettings(expectedJsonSerializerSettings);
            return this;
        }

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

        public bool CompareJsonSerializerSettings(
            JsonSerializerSettings expectedSettings,
            JsonSerializerSettings actualSettings)
        {
            return Validator.CheckEquality(expectedSettings.Culture, actualSettings.Culture)
                && Validator.CheckEquality(expectedSettings.ConstructorHandling, actualSettings.ConstructorHandling)
                && Validator.CheckEquality(expectedSettings.DateFormatHandling, actualSettings.DateFormatHandling)
                && Validator.CheckEquality(expectedSettings.DateParseHandling, actualSettings.DateParseHandling)
                && Validator.CheckEquality(expectedSettings.DateTimeZoneHandling, actualSettings.DateTimeZoneHandling)
                && Validator.CheckEquality(expectedSettings.DefaultValueHandling, actualSettings.DefaultValueHandling)
                && Validator.CheckEquality(expectedSettings.Formatting, actualSettings.Formatting)
                && Validator.CheckEquality(expectedSettings.MaxDepth, actualSettings.MaxDepth)
                && Validator.CheckEquality(expectedSettings.MissingMemberHandling, actualSettings.MissingMemberHandling)
                && Validator.CheckEquality(expectedSettings.NullValueHandling, actualSettings.NullValueHandling)
                && Validator.CheckEquality(expectedSettings.ObjectCreationHandling, actualSettings.ObjectCreationHandling)
                && Validator.CheckEquality(expectedSettings.PreserveReferencesHandling, actualSettings.PreserveReferencesHandling)
                && Validator.CheckEquality(expectedSettings.ReferenceLoopHandling, actualSettings.ReferenceLoopHandling)
                && Validator.CheckEquality(expectedSettings.TypeNameAssemblyFormat, actualSettings.TypeNameAssemblyFormat)
                && Validator.CheckEquality(expectedSettings.TypeNameHandling, actualSettings.TypeNameHandling);
        }
    }
}
