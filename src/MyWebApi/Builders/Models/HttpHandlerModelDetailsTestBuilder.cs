// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Builders.Models
{
    using System;
    using System.Net.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing HTTP response message content model from HTTP message handler.
    /// </summary>
    /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
    public class HttpHandlerModelDetailsTestBuilder<TResponseModel>
        : BaseHandlerTestBuilder, IHttpHandlerModelDetailsTestBuilder<TResponseModel>
    {
        private readonly IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder;
        private readonly TResponseModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerModelDetailsTestBuilder{TResponseModel}" /> class.
        /// </summary>
        /// <param name="handler">The HTTP message handler, from which the response model is collected.</param>
        /// <param name="httpHandlerResponseMessageTestBuilder">The original HTTP handler response message test builder.</param>
        /// <param name="model">The response model to test.</param>
        public HttpHandlerModelDetailsTestBuilder(
            HttpMessageHandler handler,
            IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder,
            TResponseModel model = default(TResponseModel))
            : base(handler)
        {
            this.httpHandlerResponseMessageTestBuilder = httpHandlerResponseMessageTestBuilder;
            this.model = model;
        }

        /// <summary>
        /// Tests whether the returned response model from the HTTP response message passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the HTTP response message.</returns>
        public IHttpHandlerResponseMessageTestBuilder Passing(Action<TResponseModel> assertions)
        {
            assertions(this.model);
            return this.httpHandlerResponseMessageTestBuilder;
        }

        /// <summary>
        /// Tests whether the returned response model from the HTTP response message passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the HTTP response message.</returns>
        public IHttpHandlerResponseMessageTestBuilder Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.model))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When testing {0} expected HTTP response message content model {1} to pass the given condition, but it failed.",
                            this.Handler.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return this.httpHandlerResponseMessageTestBuilder;
        }

        /// <summary>
        /// Gets the HTTP response message content model used in the testing.
        /// </summary>
        /// <returns>Instance of the content model type.</returns>
        public TResponseModel AndProvideTheModel()
        {
            return this.model;
        }
    }
}
