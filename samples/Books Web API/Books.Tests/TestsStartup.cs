namespace Books.Tests
{
    using System.Reflection;
    using System.Web.Http;
    using Api;
    using MyTested.WebApi;
    using NUnit.Framework;

    [SetUpFixture]
    public class TestsStartup
    {
        [SetUp]
        public void SetUpTests()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("Books.Api"));

            MyWebApi.IsRegisteredWith(WebApiConfig.Register);
        }
    }
}
