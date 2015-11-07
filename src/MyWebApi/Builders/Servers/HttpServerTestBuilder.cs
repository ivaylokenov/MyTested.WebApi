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

namespace MyWebApi.Builders.Servers
{
    using System.Net.Http;
    using System.Web.Http;

    public class HttpServerTestBuilder
    {
        private static HttpServer globalHttpServer;
        private static HttpMessageInvoker globalHttpMessageInvoker;

        internal static void StartGlobalHttpServer(HttpConfiguration httpConfiguration)
        {
            globalHttpServer = new HttpServer(httpConfiguration);
            globalHttpMessageInvoker = new HttpMessageInvoker(globalHttpServer, true);
        }

        internal static bool StopGlobalHttpServer()
        {
            if (globalHttpServer == null || globalHttpMessageInvoker == null)
            {
                return false;
            }

            globalHttpMessageInvoker.Dispose();

            globalHttpMessageInvoker = null;
            globalHttpServer = null;

            return true;
        }
    }
}
