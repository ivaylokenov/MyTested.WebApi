using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SourceControlSystem.Api.Startup))]

namespace SourceControlSystem.Api
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Common.Constants;
    using Data;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            
            WebApiConfig.Register(config);

            ConfigureAuth(app);

            app.UseAutofacMiddleware(WebApiConfig.DependencyContainer);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
