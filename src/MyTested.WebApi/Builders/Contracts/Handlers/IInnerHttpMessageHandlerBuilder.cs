// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Handlers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Used for adding inner HTTP message handlers.
    /// </summary>
    public interface IInnerHttpMessageHandlerBuilder
    {
        /// <summary>
        /// Sets inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        void WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new();

        /// <summary>
        /// Sets the provided instance as an inner HTTP handler to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="innerHandler">Instance of type HttpMessageHandler.</param>
        void WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler;

        /// <summary>
        /// Sets inner HTTP handler by using construction function to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="construction">Construction function returning the instantiated inner HttpMessageHandler.</param>
        void WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler;

        /// <summary>
        /// Sets inner HTTP handler by using builder to the current handler, if it is of type DelegatingHandler. 
        /// </summary>
        /// <typeparam name="TInnerHandler">Inner HttpMessageHandler type to set.</typeparam>
        /// <param name="httpMessageHandlerBuilder">Inner HttpMessageHandler builder.</param>
        void WithInnerHandler<TInnerHandler>(
            Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new();
    }
}
