namespace MyWebApi.Builders.Contracts
{
    using System;

    public interface IResponseModelDetailsTestBuilder<TResponseModel> : IResponseModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate);
    }
}
