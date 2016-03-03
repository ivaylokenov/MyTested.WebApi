// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    /// <summary>
    /// HTTP configuration builder.
    /// </summary>
    public interface IHttpConfigurationBuilder
    {
        /// <summary>
        /// Starts HTTP server with the provided configuration.
        /// </summary>
        /// <returns>Server builder.</returns>
        IServerBuilder AndStartsServer();

        /// <summary>
        /// Sets the error detail policy used in the testing. Default is 'Always'.
        /// </summary>
        /// <param name="errorDetailPolicy">Error details policy to use.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        IHttpConfigurationBuilder WithErrorDetailPolicy(IncludeErrorDetailPolicy errorDetailPolicy);

        /// <summary>
        /// Sets the dependency resolver used in the testing.
        /// </summary>
        /// <param name="dependencyResolver">IDependencyResolver instance to use for all tests.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        IHttpConfigurationBuilder WithDependencyResolver(IDependencyResolver dependencyResolver);

        /// <summary>
        /// Sets the dependency resolver used in the testing by using construction function.
        /// </summary>
        /// <param name="construction">Construction function returning the dependency resolver.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        IHttpConfigurationBuilder WithDependencyResolver(Func<IDependencyResolver> construction);
    }
}
