namespace MyWebApi.Builders.Contracts.Actions
{
    using System;
    using System.Net;
    using BadRequests;
    using Base;
    using Created;
    using InternalServerErrors;
    using Json;
    using Models;
    using Unauthorized;

    /// <summary>
    /// Used for testing action returned result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> DefaultValue();

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Null();

        /// <summary>
        /// Tests whether action result is not null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotNull();

        /// <summary>
        /// Tests whether action result is OkResult or OkNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Response model test builder.</returns>
        IResponseModelTestBuilder Ok();

        /// <summary>
        /// Tests whether action result is CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Created test builder.</returns>
        ICreatedTestBuilder Created();

        /// <summary>
        /// Tests whether action result is StatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode();

        /// <summary>
        /// Tests whether action result is StatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotFound();

        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        IBadRequestTestBuilder BadRequest();

        /// <summary>
        /// Tests whether action result is ConflictResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Conflict();

        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedTestBuilder Unauthorized();

        /// <summary>
        /// Tests whether action result is InternalServerErrorResult or ExceptionResult.
        /// </summary>
        /// <returns>Internal server error test builder.</returns>
        IInternalServerErrorTestBuilder InternalServerError();

        /// <summary>
        /// Tests whether action result is JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        IJsonTestBuilder Json();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType);
    }
}
