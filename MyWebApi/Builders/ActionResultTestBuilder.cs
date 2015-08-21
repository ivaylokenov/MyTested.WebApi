namespace MyWebApi.Builders
{
    using System;
    using Contracts;
    using Utilities;

    /// <summary>
    /// Used for specifying the action result type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public class ActionResultTestBuilder<TActionResult> : IActionResultTestBuilder<TActionResult>
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
    }
}
