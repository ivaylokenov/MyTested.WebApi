namespace MyWebApi.Builders.Base
{
    using System;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithActionResult{TActionResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        protected BaseTestBuilderWithActionResult(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException)
        {
            this.ActionResult = actionResult;
        }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        internal TActionResult ActionResult { get; private set; }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <returns>Action result to be tested.</returns>
        public TActionResult AndProvideTheActionResult()
        {
            return this.ActionResult;
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
        /// Initializes new instance of builder providing AndAlso method.
        /// </summary>
        /// <returns>Test builder with AndAlso method.</returns>
        protected IAndTestBuilder<TActionResult> NewAndTestBuilder()
        {
            return new AndTestBuilder<TActionResult>(this.Controller, this.ActionName, this.CaughtException, this.ActionResult);
        }

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        protected new IBaseTestBuilderWithActionResult<TActionResult> NewAndProvideTestBuilder()
        {
            return new AndProvideTestBuilder<TActionResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult);
        }
    }
}
