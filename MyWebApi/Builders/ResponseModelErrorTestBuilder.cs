namespace MyWebApi.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using Base;
    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelErrorTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorTestBuilder<TResponseModel>
    {
        private ModelStateDictionary modelState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelErrorTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
            this.modelState = controller.ModelState;
        }

        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        public void ContainingNoErrors()
        {
            if (!this.modelState.IsValid)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected response model to have no errors, but it had some.",
                    this.ActionName,
                    this.Controller.GetType().Name));
            }
        }

        public void AndModelError(string errorKey)
        {
            if (!this.modelState.ContainsKey(errorKey) || this.modelState.Count == 0)
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} to have a model error against key {2}, but none found.",
                    this.ActionName,
                    this.Controller.GetType().Name,
                    errorKey));
            }
        }

        public void AndModelErrorFor<TProperty>(Expression<Func<TResponseModel, TProperty>> memberWithError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithError);
            this.AndModelError(memberName);
        }

        public IResponseModelErrorTestBuilder<TResponseModel> AndNoModelErrorFor<TProperty>(Expression<Func<TResponseModel, TProperty>> memberWithNoError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithNoError);
            if (this.modelState.ContainsKey(memberName))
            {
                throw new ResponseModelErrorAssertionException(string.Format(
                    "When calling {0} action in {1} expected to have no model errors against key {2}, but found some.",
                    this.ActionName,
                    this.Controller.GetType().Name,
                    memberName));
            }

            return this;
        }
    }
}
