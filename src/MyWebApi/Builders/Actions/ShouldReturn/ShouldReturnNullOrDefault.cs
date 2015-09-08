namespace MyWebApi.Builders.Actions.ShouldReturn
{
    using System.Collections.Generic;
    using Common.Extensions;
    using Contracts.Base;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Class containing methods for testing BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Default()
        {
            if (Validator.CheckForDefaultValue(this.ActionResult) && this.CaughtException == null)
            {
                throw new HttpActionResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected action result to be the default value of {2}, but in fact it was not.",
                    this.ActionName,
                    this.Controller.GetName(),
                    this.ActionResult.GetName()));
            }

            return this.NewAndProvideTestBuilder();
        }

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        public IBaseTestBuilderWithActionResult<TActionResult> Null()
        {
            Validator.CheckIfTypeCanBeNull(this.ActionResult.GetType());
            return this.Default();
        }
    }
}
