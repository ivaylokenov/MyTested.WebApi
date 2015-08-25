namespace MyWebApi.Builders.Contracts.ResponseModels
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IResponseModelErrorTestBuilder<TResponseModel> : IResponseModelErrorTestBuilder
    {
        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Response model error details test builder.</returns>
        IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Response model error details test builder.</returns>
        IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TResponseModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> ContainingNoModelStateErrorFor<TMember>(
            Expression<Func<TResponseModel, TMember>> memberWithNoError);

        /// <summary>
        /// And method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Response model error details test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> And();
    }
}
