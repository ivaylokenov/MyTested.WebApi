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

namespace MyWebApi.Tests.Setups
{
    using Owin;

    public class CustomStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/test"))
                {
                    context.Response.StatusCode = 302;
                    return context.Response.WriteAsync("Found!");
                }

                if (context.Request.Headers.ContainsKey("CustomHeader"))
                {
                    return context.Response.WriteAsync("OK!");
                }

                context.Response.StatusCode = 404;
                return context.Response.WriteAsync("Not found!");
            });
        }
    }
}
