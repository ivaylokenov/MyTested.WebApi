namespace MyWebApi.Builders.Results
{
    using Contracts;

    using Utilities;

    /// <summary>
    /// Used for testing the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public partial class ActionResultTestBuilder<TActionResult> : IActionResultTestBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public ActionResultTestBuilder(string actionName, TActionResult actionResult)
        {
            this.ActionName = actionName;
            this.ActionResult = actionResult;
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        public string ActionName
        {
            get
            {
                return this.actionName;
            }

            private set
            {
                Validator.CheckForNotEmptyString(value, errorMessageName: "ActionName");
                this.actionName = value;
            }
        }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        public TActionResult ActionResult
        {
            get
            {
                return this.actionResult;
            }

            private set
            {
                Validator.CheckForNullReference(value, errorMessageName: "ActionResult");
                this.actionResult = value;
            }
        }

        private void ValidateActionReturnType<TExpectedType>()
            where TExpectedType : class
        {
            var castedOkResult = this.ActionResult as TExpectedType;
            if (castedOkResult == null)
            {
                throw new IHttpActionResultAssertionException(string.Format(
                    "When calling {0} expected action result to be a {1}, but instead received a {2}.",
                    this.ActionName,
                    typeof(TExpectedType).Name,
                    this.ActionResult.GetType().Name));
            }
        }
    }
}
