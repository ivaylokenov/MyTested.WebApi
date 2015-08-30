namespace MyWebApi.Builders.Contracts.Actions
{
    using System;
    using System.Net;

    using And;
    using BadRequests;
    using Base;
    using Models;
    using UnauthorizedResults;

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
        /// <returns>Model test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> ShouldHaveModelStateFor<TRequestModel>();

        /// <summary>
        /// Checks whether the tested action's provided model state is valid.
        /// </summary>
        /// <returns>Test builder with And method.</returns>
        IAndTestBuilder<TActionResult> ShouldHaveValidModelState();

        /// <summary>
        /// Checks whether the tested action's provided model state is not valid.
        /// </summary>
        /// <returns>Test builder with And method.</returns>
        IAndTestBuilder<TActionResult> ShouldHaveInvalidModelState();

        /// <summary>
        /// Tests whether action result is OkResult or OkNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        IResponseModelTestBuilder ShouldReturnOk();

        /// <summary>
        /// Tests whether action result is StatusCodeResult.
        /// </summary>
        void ShouldReturnStatusCode();

        /// <summary>
        /// Tests whether action result is StatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        void ShouldReturnStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        void ShouldReturnNotFound();

        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        IBadRequestTestBuilder ShouldReturnBadRequest();

        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedTestBuilder ShouldReturnUnauthorized();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model test builder.</returns>
        IModelErrorTestBuilder<TResponseModel> ShouldReturn<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Response model test builder.</returns>
        IModelErrorTestBuilder ShouldReturn(Type returnType);
    }
}
