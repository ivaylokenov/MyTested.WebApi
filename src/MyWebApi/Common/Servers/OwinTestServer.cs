// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Servers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.Owin.Hosting;

    /// <summary>
    /// Test server for full OWIN pipeline testing.
    /// </summary>
    public static class OwinTestServer
    {
        /// <summary>
        /// Default host on which the OWIN server will listen.
        /// </summary>
        public const string DefaultHost = "http://localhost";

        /// <summary>
        /// Default port on which the OWIN server will listen.
        /// </summary>
        public const int DefaultPort = 1234;

        /// <summary>
        /// Gets the global OWIN server used in the testing.
        /// </summary>
        /// <value>IDisposable instance.</value>
        public static IDisposable GlobalServer { get; private set; }

        /// <summary>
        /// Gets the global OWIN client used to send the request.
        /// </summary>
        /// <value>HttpClient instance.</value>
        public static HttpClient GlobalClient { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the HTTP is started and listening for requests.
        /// </summary>
        /// <value>True or false.</value>
        public static bool GlobalIsStarted
        {
            get
            {
                return GlobalClient != null && GlobalServer != null;
            }
        }

        /// <summary>
        /// Creates new OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="options">Start options to use for the requests.</param>
        /// <returns>IDisposable OWIN server.</returns>
        public static IDisposable CreateNewServer<TStartup>(StartOptions options)
        {
            return WebApp.Start<TStartup>(options);
        }

        /// <summary>
        /// Creates new OWIN client for the server.
        /// </summary>
        /// <param name="options">Start options to use for the requests.</param>
        /// <returns>HttpClient instance.</returns>
        public static HttpClient CreateNewClient(StartOptions options)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(options.Urls.First())
            };
        }

        /// <summary>
        /// Starts singleton global instance of the OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="options">Start options to use for the requests.</param>
        public static void StartGlobal<TStartup>(StartOptions options)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = CreateNewServer<TStartup>(options);
            GlobalClient = CreateNewClient(options);
        }

        /// <summary>
        /// Stops and disposes the global OWIN server.
        /// </summary>
        /// <returns>True or false, indicating whether the server was stopped successfully.</returns>
        public static bool StopGlobal()
        {
            if (GlobalServer == null || GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();
            GlobalServer.Dispose();

            GlobalClient = null;
            GlobalServer = null;

            return true;
        }
    }
}
