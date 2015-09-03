namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using ShouldHave;
    using ShouldReturn;
    using Utilities;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Actions.ShouldHave.ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IShouldHaveTestBuilder<TActionResult> ShouldHave()
        {
            return new ShouldHaveTestBuilder<TActionResult>(this.Controller, this.ActionName, this.CaughtException, this.ActionResult);
        }

        public IShouldThrowTestBuilder ShouldThrow()
        {
            return new ShouldThrowTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }

        public IShouldReturnTestBuilder<TActionResult> ShouldReturn()
        {
            Validator.CheckForException(this.CaughtException);
            return new ShouldReturnTestBuilder<TActionResult>(this.Controller, this.ActionName, this.CaughtException, this.ActionResult);
        }
    }
}
