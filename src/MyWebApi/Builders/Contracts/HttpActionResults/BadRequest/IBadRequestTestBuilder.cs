// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.BadRequest
{
    using System.Web.Http.ModelBinding;
    using Base;
    using Models;

    /// <summary>
    /// Used for testing bad request results.
    /// </summary>
    public interface IBadRequestTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests bad request result with specific error message using test builder.
        /// </summary>
        /// <returns>Bad request with error message test builder.</returns>
        IBadRequestErrorMessageTestBuilder WithErrorMessage();

        /// <summary>
        /// Tests bad request result with specific error message provided by string.
        /// </summary>
        /// <param name="message">Expected error message from bad request result.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithErrorMessage(string message);

        /// <summary>
        /// Tests bad request result with specific model state dictionary.
        /// </summary>
        /// <param name="modelState">Model state dictionary to deeply compare to the actual one.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException WithModelState(ModelStateDictionary modelState);

        /// <summary>
        /// Tests bad request result for model state errors using test builder.
        /// </summary>
        /// <typeparam name="TRequestModel">Type of model for which the model state errors will be tested.</typeparam>
        /// <returns>Model error test builder.</returns>
        IModelErrorTestBuilder<TRequestModel> WithModelStateFor<TRequestModel>();
    }
}
