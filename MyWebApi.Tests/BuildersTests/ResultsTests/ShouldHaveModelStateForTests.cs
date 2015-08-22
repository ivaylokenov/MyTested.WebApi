namespace MyWebApi.Tests.BuildersTests.ResultsTests
{
    using ControllerSetups;
    using ControllerSetups.Models;
    using NUnit.Framework;

    [TestFixture]
    public class ShouldHaveModelStateForTests
    {
        [Test]
        public void ShouldHaveModelStateForShouldChainCorrectly()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(r => r.Id)
                .ContainingModelStateErrorFor(r => r.Name);
        }
    }
}
