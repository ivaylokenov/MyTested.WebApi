namespace MyWebApi.Tests.BuildersTests.AndTests
{
    using System.Web.Http.Results;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class AndProvideTestBuilderTests
    {
        [Test]
        public void AndProvideControllerShouldReturnProperController()
        {
            var controller = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .AndProvideTheController();

            Assert.IsNotNull(controller);
        }

        [Test]
        public void AndProvideControllerShouldReturnProperActionName()
        {
            var actionName = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest()
                .WithErrorMessage()
                .AndProvideTheActionName();

            Assert.AreEqual("BadRequestWithErrorAction", actionName);
        }

        [Test]
        public void AndProvideControllerShouldReturnProperActionResult()
        {
            var actionResult = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturnStatusCode()
                .AndProvideTheActionResult();

            Assert.IsNotNull(actionResult);
            Assert.IsAssignableFrom<StatusCodeResult>(actionResult);
        }
    }
}
