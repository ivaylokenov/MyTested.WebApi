// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

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
