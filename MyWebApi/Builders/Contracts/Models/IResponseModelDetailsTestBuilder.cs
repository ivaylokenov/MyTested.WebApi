namespace MyWebApi.Builders.Contracts.Models
{
    using System;
    using ResponseModels;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IResponseModelDetailsTestBuilder<TResponseModel> : IResponseModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate);
    }
}
