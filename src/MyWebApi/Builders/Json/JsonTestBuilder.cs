namespace MyWebApi.Builders.Json
{
    using System;
    using System.Web.Http;
    using Contracts.Json;
    using Models;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    /// <typeparam name="TActionResult">Type of internal server error result - InternalServerErrorResult or ExceptionResult.</typeparam>
    public class JsonTestBuilder<TActionResult> : BaseResponseModelTestBuilder<TActionResult>, IJsonTestBuilder
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
    }
}
