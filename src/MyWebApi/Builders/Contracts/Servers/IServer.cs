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

namespace MyWebApi.Builders.Contracts.Servers
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
        void Starts(HttpConfiguration httpConfiguration = null);

        /// <summary>
        /// Starts new global OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Network port on which the server will listen for requests.</param>
        /// <param name="host">Network host on which the server will listen for requests.</param>
        void Starts<TStartup>(int port = OwinTestServer.DefaultPort, string host = OwinTestServer.DefaultHost);

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
    }
}
