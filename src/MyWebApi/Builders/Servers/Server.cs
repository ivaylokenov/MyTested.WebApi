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
    using System;
    using System.Web.Http;
    using Common.Servers;
    using Contracts.Servers;
    using Microsoft.Owin.Hosting;
    using Utilities.Validators;

    public class Server : IServer
    {
        public void Starts(HttpConfiguration httpConfiguration = null)
        {
            if (httpConfiguration == null)
            {
                HttpConfigurationValidator.ValidateGlobalConfiguration("server pipeline");
                httpConfiguration = MyWebApi.Configuration;
            }

            HttpTestServer.StartGlobal(httpConfiguration);
        }

        public void Starts<TStartup>(int port = OwinTestServer.DefaultPort, string host = OwinTestServer.DefaultHost)
        {
            OwinTestServer.StartGlobal<TStartup>(this.GetStartOptions(port, host));
        }

        public void Stops()
        {
            var httpServerStoppedSuccessfully = HttpTestServer.StopGlobal();
            var owinServerStoppedSuccessfully = OwinTestServer.StopGlobal();

            if (!httpServerStoppedSuccessfully && !owinServerStoppedSuccessfully)
            {
                throw new InvalidOperationException("There are no running test servers to stop. Calling MyWebApi.Server().Stops() should be done only after MyWebApi.Server.Starts() is invoked.");
            }
        }

        public IServerBuilder Running()
        {
            if (!OwinTestServer.GlobalStarted)
            {
                return new ServerTestBuilder(OwinTestServer.GlobalClient);
            }

            if (!HttpTestServer.GlobalStarted)
            {
                return new ServerTestBuilder(HttpTestServer.GlobalClient);
            }

            if (MyWebApi.Configuration != null)
            {
                return this.Running(MyWebApi.Configuration);
            }

            throw new InvalidOperationException("No test servers are started or could be started for this particular test case. Either call MyWebApi.Server.Starts() to start a new test server or provide global or test specific HttpConfiguration.");
        }

        public IServerBuilder Running(HttpConfiguration httpConfiguration)
        {
            return new ServerTestBuilder(HttpTestServer.CreateNewClient(httpConfiguration), true);
        }

        public IServerBuilder Running<TStartup>(int port = OwinTestServer.DefaultPort, string host = OwinTestServer.DefaultHost)
        {
            var options = this.GetStartOptions(port, host);
            using (OwinTestServer.CreateNewServer<TStartup>(options))
            {
                return new ServerTestBuilder(OwinTestServer.CreateNewClient(options), true);
            }
        }

        private StartOptions GetStartOptions(int port, string host)
        {
            var hostWithPort = string.Format("{0}:{1}", host, port);
            return new StartOptions(hostWithPort);
        }
    }
}
