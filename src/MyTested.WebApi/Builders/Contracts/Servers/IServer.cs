// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Builders.Contracts.Servers
{
    using System.Web.Http;
    using Common.Servers;

    /// <summary>
    /// Provides options to start and stop HTTP servers, as well as processing HTTP requests for full pipeline testing.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Starts new global HTTP server.
        /// </summary>
        /// <param name="httpConfiguration">Optional HTTP configuration to use. If no configuration is provided, the global configuration will be used instead.</param>
        /// <returns>Server builder.</returns>
        IServerBuilder Starts(HttpConfiguration httpConfiguration = null);

        /// <summary>
        /// Starts new global OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Network port on which the server will listen for requests.</param>
        /// <param name="host">Network host on which the server will listen for requests.</param>
        /// <returns>Server builder.</returns>
        IServerBuilder Starts<TStartup>(int port = OwinTestServer.DefaultPort, string host = OwinTestServer.DefaultHost);

        IServerBuilder IsLocatedAt(string baseAddress);

        /// <summary>
        /// Stops all currently started global HTTP or OWIN servers.
        /// </summary>
        void Stops();

        /// <summary>
        /// Processes HTTP requests on globally started HTTP or OWIN servers. If global OWIN server is started, it will be used. If not the method will check for global HTTP server to use. If such is not found, a new instance of HTTP server is created with the global HTTP configuration.
        /// </summary>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IServerBuilder Working();

        /// <summary>
        /// Starts new HTTP server to process a request.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use in the testing.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IServerBuilder Working(HttpConfiguration httpConfiguration);

        /// <summary>
        /// Starts new OWIN server to process a request.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Network port on which the server will listen for requests.</param>
        /// <param name="host">Network host on which the server will listen for requests.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IServerBuilder Working<TStartup>(int port = OwinTestServer.DefaultPort, string host = OwinTestServer.DefaultHost);

        IServerBuilder WorkingRemotely();

        IServerBuilder WorkingRemotely(string baseAddress);
    }
}
