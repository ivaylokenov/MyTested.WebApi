// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Servers
{
    using System;
    using System.Net.Http;
    using Microsoft.Owin.Testing;

    /// <summary>
    /// Test server for full OWIN pipeline testing.
    /// </summary>
    public static class OwinTestServer
    {
        /// <summary>
        /// Default host on which the OWIN server will listen - http://localhost.
        /// </summary>
        public const string DefaultHost = "http://localhost";

        /// <summary>
        /// Default port on which the OWIN server will listen - 80.
        /// </summary>
        public const int DefaultPort = 80;

        /// <summary>
        /// Gets the global OWIN server used in the testing.
        /// </summary>
        /// <value>Test server instance.</value>
        public static TestServer GlobalServer { get; private set; }

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
        /// <param name="baseAddress">Base address to use for the requests.</param>
        /// <returns>OWIN test server.</returns>
        public static TestServer CreateNewServer<TStartup>(string baseAddress)
        {
            var server = TestServer.Create<TStartup>();
            server.BaseAddress = new Uri(baseAddress);
            return server;
        }

        /// <summary>
        /// Starts singleton global instance of the OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        public static void StartGlobal<TStartup>(string baseAddress)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = CreateNewServer<TStartup>(baseAddress);
            GlobalClient = GlobalServer.HttpClient;
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
