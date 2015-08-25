namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for testing the response model type of action.
    /// </summary>
    public interface IResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        void WithNoResponseModel();

        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IResponseModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
            where TResponseModel : class;
    }
}
