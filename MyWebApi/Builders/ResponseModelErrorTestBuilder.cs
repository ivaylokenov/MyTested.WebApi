namespace MyWebApi.Builders
{
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    using Base;
    using Contracts;
    using Exceptions;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    public class ResponseModelErrorTestBuilder : BaseTestBuilder, IResponseModelErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelErrorTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
            this.ModelState = controller.ModelState;
        }

        /// <summary>
        /// Gets validated model state of the provided ASP.NET Web API controller instance.
        /// </summary>
        /// <value>Model state dictionary containing all validation errors.</value>
        protected ModelStateDictionary ModelState { get; private set; }

        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        public void ContainingNoModelStateErrors()
        {
            if (!this.ModelState.IsValid)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model to have no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetType().Name));
            }
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        public void ContainingModelStateError(string errorKey)
        {
            if (!this.ModelState.ContainsKey(errorKey) || this.ModelState.Count == 0)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    this.ActionName,
                    this.Controller.GetType().Name,
                    errorKey));
            }
        }
    }
}
