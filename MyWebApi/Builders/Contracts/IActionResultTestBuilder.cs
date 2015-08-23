namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Net;

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
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        void ShouldHaveValidModelState();

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        void ShouldHaveInvalidModelState();

        /// <summary>
        /// Tests whether action result is OkResult.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        IResponseModelTestBuilder ShouldReturnOk();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model test builder.</returns>
        IResponseModelErrorTestBuilder<TResponseModel> ShouldReturn<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Response model test builder.</returns>
        IResponseModelErrorTestBuilder ShouldReturn(Type returnType);

        void ShouldReturnStatusCode();

        void ShouldReturnStatusCode(HttpStatusCode statusCode);
    }
}
