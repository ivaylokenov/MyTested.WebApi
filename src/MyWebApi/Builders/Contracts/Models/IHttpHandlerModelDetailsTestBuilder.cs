// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Models
{
    using System;
    using Base;
    using HttpResponseMessages;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from HTTP response message content.</typeparam>
    public interface IHttpHandlerModelDetailsTestBuilder<out TResponseModel> : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Tests whether the returned response model from the tested handler passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the HTTP response message results from handlers.</returns>
        IHttpHandlerResponseMessageTestBuilder Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the tested handler passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the HTTP response message results from handlers.</returns>
        IHttpHandlerResponseMessageTestBuilder Passing(Func<TResponseModel, bool> predicate);

        /// <summary>
        /// Gets the HTTP response message content model used in the testing.
        /// </summary>
        /// <returns>Instance of the content model type.</returns>
        TResponseModel AndProvideTheModel();
    }
}
