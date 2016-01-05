// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders
{
    using System.Web.Http;
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
        /// Sets the global base address to be used across the test cases. Default is local host.
        /// </summary>
        /// <param name="baseAddress">Base address to use.</param>
        /// <returns>The same HTTP configuration builder.</returns>
        public IHttpConfigurationBuilder WithBaseAddress(string baseAddress)
        {
            MyWebApi.BaseAddress = baseAddress;

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
