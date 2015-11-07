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

            GlobalHttpServer.Start(httpConfiguration);
        }

        public void Starts<TStartup>(int port = 1234, string host = "localhost")
        {
            var options = new StartOptions(host)
            {
                Port = port
            };

            GlobalOwinServer.Start<TStartup>(options);
        }

        public void Ends()
        {
            var httpServerStoppedSuccessfully = GlobalHttpServer.Stop();
            var owinServerStoppedSuccessfully = GlobalOwinServer.Stop();

            if (!httpServerStoppedSuccessfully && !owinServerStoppedSuccessfully)
            {
                // TODO: throw
            }
        }

        public IServerBuilder Running()
        {
            throw new System.NotImplementedException();
        }
    }
}
