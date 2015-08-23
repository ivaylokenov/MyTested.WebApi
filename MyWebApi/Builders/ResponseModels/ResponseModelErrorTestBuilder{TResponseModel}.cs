namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;

    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelErrorTestBuilder<TResponseModel> : ResponseModelErrorTestBuilder, IResponseModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelErrorTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ResponseModelErrorTestBuilder(ApiController controller, string actionName)
            : base(controller, actionName)
        {
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateError(string errorKey)
        {
            if (!this.ModelState.ContainsKey(errorKey) || this.ModelState.Count == 0)
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have a model error against key {2}, but none found.",
                    errorKey);
            }

            return new ResponseModelErrorDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this,
                errorKey,
                this.ModelState[errorKey].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TResponseModel, TMember>> memberWithError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithError);
            this.ContainingModelStateError(memberName);

            return new ResponseModelErrorDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this,
                memberName,
                this.ModelState[memberName].Errors);
        }

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> ContainingNoModelStateErrorFor<TMember>(Expression<Func<TResponseModel, TMember>> memberWithNoError)
        {
            var memberName = ExpressionParser.GetPropertyName(memberWithNoError);
            if (this.ModelState.ContainsKey(memberName))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected to have no model errors against key {2}, but found some.",
                    memberName);
            }

            return this;
        }

        private void ThrowNewResponseModelErrorAssertionException(string messageFormat, string errorKey)
        {
            throw new ResponseModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyGenericTypeName(),
                    errorKey));
        }
    }
}
