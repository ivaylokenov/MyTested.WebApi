namespace MyWebApi.Builders.Json
{
    using System;
    using System.Text;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Json;
    using Exceptions;
    using Models;
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
            var actualEncoding = this.ActionResult.GetType().CastTo<dynamic>(this.ActionResult).Encoding as Encoding;
            if (!encoding.Equals(actualEncoding))
            {
                throw new JsonResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result encoding to be {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    encoding,
                    actualEncoding));
            }

            return this;
        }

        public IJsonTestBuilder AndAlso()
        {
            return this;
        }
    }
}
