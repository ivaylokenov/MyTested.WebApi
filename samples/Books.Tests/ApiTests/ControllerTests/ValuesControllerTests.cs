namespace Books.Tests.ApiTests.ControllerTests
{
    using System.Collections.Generic;
    using Api.Controllers;
    using MyWebApi;
    using NUnit.Framework;

    [TestFixture]
    public class ValuesControllerTests
    {
        [Test]
        public void ValuesControllerShouldTest()
        {
            MyWebApi
                .Controller<ValuesController>()
                .Calling(c => c.Get())
                .ShouldReturn<IEnumerable<string>>();
        }
    }
}
