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
    using System.Threading;
    using System.Threading.Tasks;

    public class ServerHttpMessageHandler : HttpMessageHandler
    {
        public ServerHttpMessageHandler(HttpMessageInvoker httpMessageInvoker)
        {
            this.HttpMessageInvoker = httpMessageInvoker;
        }

        public HttpMessageInvoker HttpMessageInvoker { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.HttpMessageInvoker.SendAsync(request, cancellationToken);
        }
    }
}
