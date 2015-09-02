namespace MyWebApi.Builders.And
{
    using System;
    using System.Web.Http;
    using Base;

    /// <summary>
    /// Provides controller, action and action result information.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class AndProvideTestBuilder<TActionResult> : BaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndProvideTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public AndProvideTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
    }
}
