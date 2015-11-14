using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Books.Api.Startup))]

namespace Books.Api
{
    using System.Reflection;
    using System.Web.Http;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            app.UseNinjectMiddleware(NinjectConfig.CreateKernel);

            ConfigureAuth(app);

            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            app.UseNinjectWebApi(config);
        }
    }
}
