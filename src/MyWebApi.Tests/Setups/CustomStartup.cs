// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups
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

                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/json"))
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"IntegerValue\":1,\"StringValue\":\"Test\"}");
                }

                if (context.Request.Method == "POST" && context.Request.Uri.OriginalString.EndsWith("/nomodel"))
                {
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"id\":1}");
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
