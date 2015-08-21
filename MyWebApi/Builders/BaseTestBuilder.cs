namespace MyWebApi.Builders
{
    using Contracts;
    
    using Utilities;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public abstract class BaseTestBuilder<TActionResult> : IBaseTestBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseTestBuilder(string actionName, TActionResult actionResult)
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
    }
}
