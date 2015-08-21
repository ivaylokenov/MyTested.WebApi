namespace MyWebApi.Builders.Results
{
    using Contracts;

    using Utilities;

    /// <summary>
    /// Used for testing the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult> 
        : BaseTestBuilder<TActionResult>, IActionResultTestBuilder<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(string actionName, TActionResult actionResult)
            : base(actionName, actionResult)
        {
        }

        private void ValidateActionReturnType<TExpectedType>(bool canBeAssignable = false)
        {
            var typeOfActionResult = this.ActionResult.GetType();
            var typeOfResponseData = typeof(TExpectedType);

            bool invalid = (canBeAssignable && !typeOfResponseData.IsAssignableFrom(typeOfActionResult))
                           || (!canBeAssignable && typeOfActionResult != typeOfResponseData);

            if (invalid)
            {
                throw new IHttpActionResultAssertionException(string.Format(
                    "When calling {0} expected action result to be a {1}, but instead received a {2}.",
                    this.ActionName,
                    typeOfResponseData.Name,
                    typeOfActionResult.Name));
            }
        }
    }
}
