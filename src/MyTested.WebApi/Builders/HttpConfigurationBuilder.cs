// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using Common.Servers;
    using Contracts;
    using Servers;

    /// <summary>
    /// HTTP configuration builder.
    /// </summary>
    public class HttpConfigurationBuilder : IHttpConfigurationBuilder
    {
        private readonly HttpConfiguration httpConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConfigurationBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">HttpConfiguration instance used in the builder.</param>
        public HttpConfigurationBuilder(HttpConfiguration httpConfiguration)
        {
            this.httpConfiguration = httpConfiguration;
            this.SetErrorDetailPolicyAndInitialize(IncludeErrorDetailPolicy.Always);
        }

        /// <summary>
        /// Starts HTTP server with the provided configuration.
        /// </summary>
        /// <returns>Server builder.</returns>
        public IServerBuilder AndStartsServer()
        {
            return new Server().Starts(this.httpConfiguration);
        }

        /// <summary>
        /// Sets the error detail policy used in the testing. Default is 'Always'.
        /// </summary>
        /// <param name="errorDetailPolicy">Error details policy to use.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        public IHttpConfigurationBuilder WithErrorDetailPolicy(IncludeErrorDetailPolicy errorDetailPolicy)
        {
            this.SetErrorDetailPolicyAndInitialize(errorDetailPolicy);
            return this;
        }

        /// <summary>
        /// Sets the dependency resolver used in the testing.
        /// </summary>
        /// <param name="dependencyResolver">IDependencyResolver instance to use for all tests.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        public IHttpConfigurationBuilder WithDependencyResolver(IDependencyResolver dependencyResolver)
        {
            this.httpConfiguration.DependencyResolver = dependencyResolver;
            return this;
        }

        /// <summary>
        /// Sets the dependency resolver used in the testing by using construction function.
        /// </summary>
        /// <param name="construction">Construction function returning the dependency resolver.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        public IHttpConfigurationBuilder WithDependencyResolver(Func<IDependencyResolver> construction)
        {
            return this.WithDependencyResolver(construction());
        }

        private void SetErrorDetailPolicy(IncludeErrorDetailPolicy errorDetailPolicy)
        /// <summary>
        /// Sets the global base address to be used across the test cases. Default is local host.
        /// </summary>
        /// <param name="baseAddress">Base address to use.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        public IHttpConfigurationBuilder WithBaseAddress(string baseAddress)
        {
            MyWebApi.BaseAddress = new Uri(baseAddress, UriKind.Absolute);

            if (!RemoteServer.GlobalIsConfigured)
            {
                RemoteServer.ConfigureGlobal(baseAddress);
            }

            return this;
        }

        private void SetErrorDetailPolicyAndInitialize(IncludeErrorDetailPolicy errorDetailPolicy)
        {
            if (this.httpConfiguration != null)
            {
                this.httpConfiguration.IncludeErrorDetailPolicy = errorDetailPolicy;
                this.httpConfiguration.EnsureInitialized();
            }
        }
    }
}
