namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is OkResult.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        IResponseModelTestBuilder ShouldReturnOkResult();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseData">Expected response type.</typeparam>
        void ShouldReturn<TResponseData>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        void ShouldReturn(Type returnType);
    }
}
