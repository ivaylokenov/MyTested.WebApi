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
    using System;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.Owin.Hosting;

    public static class OwinTestServer
    {
        public const string DefaultHost = "http://localhost";
        public const int DefaultPort = 1234;
        
        public static IDisposable GlobalServer { get; private set; }

        public static HttpClient GlobalClient { get; private set; }

        public static bool GlobalIsStarted { get { return GlobalClient != null && GlobalServer != null; } }

        public static IDisposable CreateNewServer<TStartup>(StartOptions options)
        {
            return WebApp.Start<TStartup>(options);
        }

        public static HttpClient CreateNewClient(StartOptions options)
        {
            return new HttpClient
            {
                BaseAddress = new Uri(options.Urls.First())
            };
        }

        public static void StartGlobal<TStartup>(StartOptions options)
        {
            if (GlobalIsStarted)
            {
                StopGlobal();
            }

            GlobalServer = CreateNewServer<TStartup>(options);
            GlobalClient = CreateNewClient(options);
        }

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
