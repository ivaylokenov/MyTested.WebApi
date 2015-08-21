namespace MyWebApi.Builders
{
    using Contracts;
    using Utilities;

    public abstract class BaseTestBuilder<TActionResult> : IBaseTestBuilder<TActionResult>
    {
        private string actionName;
        private TActionResult actionResult;

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
