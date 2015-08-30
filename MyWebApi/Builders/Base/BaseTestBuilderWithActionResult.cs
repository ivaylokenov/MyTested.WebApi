namespace MyWebApi.Builders.Base
{
    using System.Web.Http;
    using And;
    using Common.Extensions;
    using Contracts.And;
    using Contracts.Base;
    using Exceptions;
    using Microsoft.CSharp.RuntimeBinder;
    using Utilities;

    /// <summary>
    /// Base class for all test builders with action result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public abstract class BaseTestBuilderWithActionResult<TActionResult>
        : BaseTestBuilder, IBaseTestBuilderWithActionResult<TActionResult>
    {
        private TActionResult actionResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseTestBuilderWithActionResult(ApiController controller, string actionName, TActionResult actionResult)
            : base(controller, actionName)
        {
            this.ActionResult = actionResult;
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

        /// <summary>
        /// Gets response model from action result.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of response model.</typeparam>
        /// <returns>The response model.</returns>
        protected TResponseModel GetActualModel<TResponseModel>()
        {
            try
            {
                return this.ActionResult.GetType().CastTo<dynamic>(this.ActionResult).Content;
            }
            catch (RuntimeBinderException)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model of type {2}, but instead received null.",
                    this.ActionName,
                    this.Controller.GetName(),
                    typeof(TResponseModel).ToFriendlyTypeName()));
            }
        }

        /// <summary>
        /// Initializes new instance of builder providing And method.
        /// </summary>
        /// <returns>Test builder with And method.</returns>
        protected IAndTestBuilder<TActionResult> NewAndTestBuilder()
        {
            return new AndTestBuilder<TActionResult>(this.Controller, this.ActionName, this.ActionResult);
        }
    }
}
