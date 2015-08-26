namespace MyWebApi.Builders.Contracts.Models
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder
    {
        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        IModelErrorTestBuilder<TModel> ContainingNoModelStateErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithNoError);

        /// <summary>
        /// And method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> And();
    }
}
