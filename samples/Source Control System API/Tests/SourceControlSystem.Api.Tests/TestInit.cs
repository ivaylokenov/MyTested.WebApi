namespace SourceControlSystem.Api.Tests
{
    using System.Reflection;
    using System.Web.Http;
    using Common.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.WebApi));

            MyWebApi.IsRegisteredWith(WebApiConfig.Register);
        }
    }
}
