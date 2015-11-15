namespace SourceControlSystem.Api
{
    using Common.Constants;
    using System.Reflection;
    using System.Web;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.WebApi));
        }
    }
}
