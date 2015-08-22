namespace MyWebApi.Builders.Base
{
    using System.Web.Http;

    using Contracts;
    using Utilities;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        private ApiController controller;
        private string actionName;
        private TActionResult actionResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseTestBuilderWithActionResult(ApiController controller, string actionName, TActionResult actionResult)
        {
            this.Controller = controller;
            this.ActionName = actionName;
            this.ActionResult = actionResult;
        }

        /// <summary>
        /// Gets the controller on which the action will be tested.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        public ApiController Controller
        {
            get
            {
                return this.controller;
            }

            private set
            {
                Validator.CheckForNullReference(value, errorMessageName: "Controller");
                this.controller = value;
            }
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
