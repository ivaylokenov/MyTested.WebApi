namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing the response model errors.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IResponseModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether tested action's model state is valid.
        /// </summary>
        void ContainingNoErrors();

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        void AndModelError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TProperty">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        void AndModelErrorFor<TProperty>(Expression<Func<TResponseModel, TProperty>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TProperty">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This in order to support method chaining.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> AndNoModelErrorFor<TProperty>(
            Expression<Func<TResponseModel, TProperty>> memberWithNoError);
    }
}
