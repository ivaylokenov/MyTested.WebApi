// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.HttpActionResults.Redirect
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Http;
    using Base;
    using Uris;

    /// <summary>
    /// Used for testing redirect results.
    /// </summary>
    public interface IRedirectTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether redirect result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(string location);

        /// <summary>
        /// Tests whether redirect result location passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the location.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocationPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether redirect result location passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocationPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether redirect result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(Uri location);

        /// <summary>
        /// Tests whether redirect result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException To<TController>(Expression<Func<TController, object>> actionCall)
            where TController : ApiController;

        /// <summary>
        /// Tests whether redirect result redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>Base test builder.</returns>
        IBaseTestBuilderWithCaughtException To<TController>(Expression<Action<TController>> actionCall)
            where TController : ApiController;
    }
}
