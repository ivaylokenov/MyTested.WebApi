namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for testing the response model type of test.
    /// </summary>
    public interface IResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>();

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class;

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="predicate">Predicate testing the response model.</param>
        IResponseModelErrorTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(Func<TResponseModel, bool> predicate);
    }
}
