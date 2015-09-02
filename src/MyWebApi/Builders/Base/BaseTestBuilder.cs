namespace MyWebApi.Builders.Base
{
    using System;
    using System.Web.Http;
    using And;
    using Common.Extensions;
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
        protected BaseTestBuilder(ApiController controller, string actionName, Exception caughtException)
        {
            this.Controller = controller;
            this.ActionName = actionName;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Gets the controller on which the action will be tested.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        internal ApiController Controller
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
        internal string ActionName
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

        internal Exception CaughtException { get; private set; }

        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET Web API controller on which the action is tested.</returns>
        public ApiController AndProvideTheController()
        {
            return this.Controller;
        }

        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <returns>Action name to be tested.</returns>
        public string AndProvideTheActionName()
        {
            return this.ActionName;
        }

        public Exception AndProvideTheCaughtException()
        {
            return this.CaughtException;
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

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder.</returns>
        protected IBaseTestBuilder NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
