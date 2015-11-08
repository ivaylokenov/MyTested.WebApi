// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Common.Servers
{
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Test server for full pipeline testing.
    /// </summary>
    public static class HttpTestServer
    {
        /// <summary>
        /// Gets the global HTTP server used in the testing.
        /// </summary>
        /// <value>HttpServer instance.</value>
        public static HttpServer GlobalServer { get; private set; }

        /// <summary>
        /// Gets the global HTTP client used to send the request.
        /// </summary>
        /// <value>HttpMessageInvoker instance.</value>
        public static HttpMessageInvoker GlobalClient { get; private set; }

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
        /// Creates new HTTP client for the server.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the requests.</param>
        /// <returns>HttpMessageInvoker instance.</returns>
        public static HttpMessageInvoker CreateNewClient(HttpConfiguration httpConfiguration)
        {
            var httpServer = new HttpServer(httpConfiguration);
            return new HttpMessageInvoker(httpServer);
        }

        /// <summary>
        /// Starts singleton global instance of the HTTP server.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use for the requests.</param>
        public static void StartGlobal(HttpConfiguration httpConfiguration)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = new HttpServer(httpConfiguration);
            GlobalClient = new HttpMessageInvoker(GlobalServer, true);
        }

        /// <summary>
        /// Stops and disposes the global HTTP server.
        /// </summary>
        /// <returns>True or false, indicating whether the server was stopped successfully.</returns>
        public static bool StopGlobal()
        {
            if (GlobalServer == null || GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();

            GlobalClient = null;
            GlobalServer = null;

            return true;
        }
    }
}
