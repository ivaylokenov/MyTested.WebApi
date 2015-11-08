// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Builders.Contracts.Routes
{
    using System.Net.Http;

    /// <summary>
    /// Used for adding additional test cases to a route.
    /// </summary>
    public interface IResolvedRouteTestBuilder
    {
        /// <summary>
        /// Tests whether the resolved route will be handled by a HttpMessageHandler of the provided type.
        /// </summary>
        /// <typeparam name="THandler">Type of HttpMessageHandler.</typeparam>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToHandlerOfType<THandler>()
            where THandler : HttpMessageHandler;

        /// <summary>
        /// Tests whether the resolved route will not be handled by a HttpMessageHandler of the provided type.
        /// </summary>
        /// <typeparam name="THandler">Type of HttpMessageHandler.</typeparam>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToNoHandlerOfType<THandler>()
            where THandler : HttpMessageHandler;

        /// <summary>
        /// Tests whether the resolved route will not be handled by any HttpMessageHandler.
        /// </summary>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToNoHandler();

        /// <summary>
        /// Tests whether the resolved route will have valid model state.
        /// </summary>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToValidModelState();

        /// <summary>
        /// Tests whether the resolved route will have invalid model state.
        /// </summary>
        /// <param name="withNumberOfErrors">Expected number of errors. If default null is provided, the test builder checks only if any errors are found.</param>
        /// <returns>The same route test builder.</returns>
        IAndResolvedRouteTestBuilder ToInvalidModelState(int? withNumberOfErrors = null);
    }
}
