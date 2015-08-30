namespace MyWebApi.Builders.Base
{
    using System.Web.Http;

    using Common.Extensions;
    using Contracts;
    using Contracts.Base;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Base class for all test builders.
    /// </summary>
    public class BaseTestBuilder : IBaseTestBuilder
    {
        private ApiController controller;
        private string actionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        protected BaseTestBuilder(ApiController controller, string actionName)
        {
            this.Controller = controller;
            this.ActionName = actionName;
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
                Validator.CheckForNotWhiteSpaceString(value, errorMessageName: "ActionName");
                this.actionName = value;
            }
        }

        /// <summary>
        /// Checks whether the tested action's model state is valid.
        /// </summary>
        protected void CheckValidModelState()
        {
            if (!this.controller.ModelState.IsValid)
            {
                throw new ModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have valid model state with no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetName()));
            }
        }
    }
}
