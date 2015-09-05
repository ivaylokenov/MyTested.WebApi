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

        public void WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            var actualJsonSerializerSettings =
                this.GetActionResultAsDynamic(this.ActionResult).SerializerSettings as JsonSerializerSettings;


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

        private bool CompareJsonSerializerSettings(
            JsonSerializerSettings expectedSettings,
            JsonSerializerSettings actualSettings)
        {
            if (expectedSettings.Culture.Equals(actualSettings.Culture))
            {
                return false;
            }

            if (expectedSettings.ConstructorHandling != actualSettings.ConstructorHandling)
            {
                return false;
            }

            if (expectedSettings.DateFormatHandling != actualSettings.DateFormatHandling)
            {
                return false;
            }

            if (expectedSettings.DateParseHandling != actualSettings.DateParseHandling)
            {
                return false;
            }

            if (expectedSettings.DateTimeZoneHandling != actualSettings.DateTimeZoneHandling)
            {
                return false;
            }

            if (expectedSettings.DefaultValueHandling != actualSettings.DefaultValueHandling)
            {
                return false;
            }

            if (expectedSettings.Formatting != actualSettings.Formatting)
            {
                return false;
            }

            if (expectedSettings.MaxDepth != actualSettings.MaxDepth)
            {
                return false;
            }

            if (expectedSettings.MissingMemberHandling != actualSettings.MissingMemberHandling)
            {
                return false;
            }

            if (expectedSettings.NullValueHandling != actualSettings.NullValueHandling)
            {
                return false;
            }

            if (expectedSettings.ObjectCreationHandling != actualSettings.ObjectCreationHandling)
            {
                return false;
            }

            if (expectedSettings.PreserveReferencesHandling != actualSettings.PreserveReferencesHandling)
            {
                return false;
            }

            if (expectedSettings.ReferenceLoopHandling != actualSettings.ReferenceLoopHandling)
            {
                return false;
            }

            if (expectedSettings.TypeNameAssemblyFormat != actualSettings.TypeNameAssemblyFormat)
            {
                return false;
            }

            if (expectedSettings.TypeNameHandling != actualSettings.TypeNameHandling)
            {
                return false;
            }

            return true;
        }
    }
}
