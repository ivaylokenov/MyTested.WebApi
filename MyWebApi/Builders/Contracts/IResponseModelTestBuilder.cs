namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for testing the response model type of test.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IResponseModelTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
        void WithResponseModel<TResponseData>();

        /// <summary>
        /// Tests whether an object is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        void WithResponseModel<TResponseData>(TResponseData expectedModel)
            where TResponseData : class;

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <typeparam name="TResponseData">Type of the response model.</typeparam>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        void WithResponseModel<TResponseData>(Action<TResponseData> assertions);
    }
}
