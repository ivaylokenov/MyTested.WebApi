namespace SourceControlSystem.Api
{
    using System;
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Common.Constants;
    using Data;
    using Microsoft.Owin.Security.OAuth;

    public static class WebApiConfig
    {
        public static IContainer DependencyContainer { get; private set; }

        public static Action<ContainerBuilder> DependencyRegistrationAction = builder =>
        {
            builder.RegisterApiControllers(Assembly.Load(Assemblies.WebApi));
            builder.RegisterType<SourceControlSystemDbContext>().As<ISourceControlSystemDbContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EfGenericRepository<>)).As(typeof(IRepository<>));
            builder.RegisterAssemblyTypes(Assembly.Load(Assemblies.DataServices)).AsImplementedInterfaces();
        };

        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            var builder = new ContainerBuilder();
            DependencyRegistrationAction(builder);
            DependencyContainer = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(DependencyContainer);
        }
    }
}
