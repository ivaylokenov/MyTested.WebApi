namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing specific response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IResponseModelErrorDetailsTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>The original response model error test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>The original response model error test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> Containing(string containsMessage);

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
        IResponseModelErrorDetailsTestBuilder<TResponseModel> ContainingModelStateErrorFor<TMember>(
            Expression<Func<TResponseModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>Response model error details test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> ContainingNoModelStateErrorFor<TMember>(
            Expression<Func<TResponseModel, TMember>> memberWithNoError);
    }
}
