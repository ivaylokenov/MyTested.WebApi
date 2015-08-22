namespace MyWebApi.Builders.Contracts
{
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
    }
}
