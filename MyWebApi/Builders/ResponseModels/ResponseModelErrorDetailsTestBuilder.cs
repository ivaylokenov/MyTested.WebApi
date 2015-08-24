namespace MyWebApi.Builders.ResponseModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    using Base;
    using Contracts;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing specific response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public class ResponseModelErrorDetailsTestBuilder<TResponseModel> : BaseTestBuilder, IResponseModelErrorDetailsTestBuilder<TResponseModel>
    {
        private readonly IResponseModelErrorTestBuilder<TResponseModel> responseModelErrorTestBuilder;
        private readonly string currentErrorKey;
        private readonly IEnumerable<string> aggregatedErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModelErrorDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="responseModelErrorTestBuilder">Original response model error test builder.</param>
        /// <param name="errorKey">Key in ModelStateDictionary corresponding to this particular error.</param>
        /// <param name="aggregatedErrors">All errors found in ModelStateDictionary for given error key.</param>
        public ResponseModelErrorDetailsTestBuilder(
            ApiController controller,
            string actionName,
            IResponseModelErrorTestBuilder<TResponseModel> responseModelErrorTestBuilder,
            string errorKey,
            IEnumerable<ModelError> aggregatedErrors)
            : base(controller, actionName)
        {
            this.responseModelErrorTestBuilder = responseModelErrorTestBuilder;
            this.currentErrorKey = errorKey;
            this.aggregatedErrors = aggregatedErrors.Select(me => me.ErrorMessage);
        }

        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>The original response model error test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> ThatEquals(string errorMessage)
        {
            if (this.aggregatedErrors.All(e => e != errorMessage))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key {2} to be '{3}', but instead found '{4}'.",
                    errorMessage);
            }

            return this.responseModelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> BeginningWith(string beginMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.StartsWith(beginMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to start with '{3}', but instead found '{4}'.",
                    beginMessage);
            }

            return this.responseModelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> EndingWith(string endMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.EndsWith(endMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to end with '{3}', but instead found '{4}'.",
                    endMessage);
            }

            return this.responseModelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> Containing(string containsMessage)
        {
            if (!this.aggregatedErrors.Any(e => e.Contains(containsMessage)))
            {
                this.ThrowNewResponseModelErrorAssertionException(
                    "When calling {0} action in {1} expected error message for key '{2}' to contain '{3}', but instead found '{4}'.",
                    containsMessage);
            }

            return this.responseModelErrorTestBuilder;
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateError(string errorKey)
        {
            return this.responseModelErrorTestBuilder.ContainingModelStateError(errorKey);
        }

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TResponseModel, TMember>> memberWithError)
        {
            return this.responseModelErrorTestBuilder.ContainingModelStateErrorFor(memberWithError);
        }

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> ContainingNoModelStateErrorFor<TMember>(Expression<Func<TResponseModel, TMember>> memberWithNoError)
        {
            return this.responseModelErrorTestBuilder.ContainingNoModelStateErrorFor(memberWithNoError);
        }

        /// <summary>
        /// And method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Response model error details test builder.</returns>
        public IResponseModelErrorTestBuilder<TResponseModel> And()
        {
            return this.responseModelErrorTestBuilder;
        }

        private void ThrowNewResponseModelErrorAssertionException(string messageFormat, string operation)
        {
            throw new ResponseModelErrorAssertionException(string.Format(
                    messageFormat,
                    this.ActionName,
                    this.Controller.GetType().ToFriendlyTypeName(),
                    this.currentErrorKey,
                    operation,
                    string.Join(", ", this.aggregatedErrors)));  
        }
    }
}
