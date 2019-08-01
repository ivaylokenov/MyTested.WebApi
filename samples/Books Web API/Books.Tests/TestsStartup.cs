namespace Books.Tests
{
    using System.Reflection;
    using Api;
    using MyTested.WebApi;
    using NUnit.Framework;

    [SetUpFixture]
    public class TestsStartup
    {
        [OneTimeSetUp]
        public void SetUpTests()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("Books.Api"));

            MyWebApi.IsRegisteredWith(WebApiConfig.Register);
        }
    }
}
