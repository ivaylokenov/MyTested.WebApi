namespace MyWebApi.Builders.Contracts
{
    using System;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<out TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Provides way to continue test case with specific model state errors.
        /// </summary>
        /// <typeparam name="TRequestModel">Request model type to be tested for errors.</typeparam>
        /// <returns>Response model test builder.</returns>
        IResponseModelErrorTestBuilder<TRequestModel> ShouldHaveModelStateFor<TRequestModel>();

        /// <summary>
        /// Tests whether action result is OkResult.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        IResponseModelTestBuilder ShouldReturnOk();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        IResponseModelErrorTestBuilder<TResponseModel> ShouldReturn<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        IResponseModelErrorTestBuilder ShouldReturn(Type returnType);
    }
}
