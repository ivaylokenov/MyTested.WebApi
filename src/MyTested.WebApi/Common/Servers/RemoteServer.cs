// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Common.Servers
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Test server for remote server testing.
    /// </summary>
    public static class RemoteServer
    {
        /// <summary>
        /// Gets the global HTTP client used to send the request.
        /// </summary>
        /// <value>HttpClient instance.</value>
        public static HttpClient GlobalClient { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the remote server is configured and requests can be sent.
        /// </summary>
        /// <value>True or false.</value>
        public static bool GlobalIsConfigured
        {
            get { return GlobalClient != null; }
        }

        /// <summary>
        /// Creates new HTTP client for the remote server.
        /// </summary>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        /// <returns>HttpClient instance.</returns>
        public static HttpClient CreateNewClient(string baseAddress)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        /// <summary>
        /// Configures singleton global instance of the HTTP remote server client.
        /// </summary>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        public static void ConfigureGlobal(string baseAddress)
        {
            if (GlobalIsConfigured)
            {
                DisposeGlobal();
            }

            GlobalClient = CreateNewClient(baseAddress);
        }

        /// <summary>
        /// Disposes the global HTTP remote server client.
        /// </summary>
        /// <returns>True or false, indicating whether the client was disposed successfully.</returns>
        public static bool DisposeGlobal()
        {
            if (GlobalClient == null)
            {
                return false;
            }

            GlobalClient.Dispose();
            GlobalClient = null;

            return true;
        }
    }
}
