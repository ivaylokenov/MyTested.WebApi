namespace MyWebApi.Builders.Contracts
{
    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is OkResult.
        /// </summary>
        IResponseModelTestBuilder<TActionResult> ShouldReturnOkResult();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseData">Expected response type.</typeparam>
        void ShouldReturn<TResponseData>();
    }
}
