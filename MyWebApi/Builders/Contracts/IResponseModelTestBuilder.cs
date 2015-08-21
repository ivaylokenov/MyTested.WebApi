namespace MyWebApi.Builders.Contracts
{
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
    }
}
