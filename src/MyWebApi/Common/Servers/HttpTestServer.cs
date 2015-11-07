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

    public static class HttpTestServer
    {
        public static HttpServer GlobalServer { get; private set; }

        public static HttpMessageInvoker GlobalClient { get; private set; }

        public static bool GlobalStarted { get { return GlobalClient != null && GlobalServer != null; } }

        public static HttpMessageInvoker CreateNewClient(HttpConfiguration httpConfiguration)
        {
            var httpServer = new HttpServer(httpConfiguration);
            return new HttpMessageInvoker(httpServer);
        }

        public static void StartGlobal(HttpConfiguration httpConfiguration)
        {
            if (GlobalStarted)
            {
                StopGlobal();
            }

            GlobalServer = new HttpServer(httpConfiguration);
            GlobalClient = new HttpMessageInvoker(GlobalServer, true);
        }

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
