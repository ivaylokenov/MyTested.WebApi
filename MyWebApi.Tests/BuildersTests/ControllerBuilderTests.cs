namespace MyWebApi.Tests.BuildersTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Builders.Contracts;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class ControllerBuilderTests
    {
        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction());

            CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.AsyncOkResultAction());

            CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        [Test]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrors()
        {
            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel));

            var modelState = actionResultTestBuilder.Controller.ModelState;

            Assert.IsFalse(modelState.IsValid);
            Assert.AreEqual(2, modelState.Values.Count);
            Assert.AreEqual("Integer", modelState.Keys.First());
            Assert.AreEqual("RequiredString", modelState.Keys.Last());
        }

        [Test]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel));

            var modelState = actionResultTestBuilder.Controller.ModelState;

            Assert.IsTrue(modelState.IsValid);
            Assert.AreEqual(0, modelState.Values.Count);
            Assert.AreEqual(0, modelState.Keys.Count);
        }

        [Test]
        public void WithAuthorizedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>()
                .WithAuthorizedUser();

            controllerBuilder
                .Calling(c => c.Authorized())
                .ShouldReturnOk();

            var controllerUser = controllerBuilder.Controller.User;

            Assert.AreEqual(false, controllerUser.IsInRole("Any"));
            Assert.AreEqual("TestUser", controllerUser.Identity.Name);
            Assert.AreEqual("Passport", controllerUser.Identity.AuthenticationType);
            Assert.AreEqual(true, controllerUser.Identity.IsAuthenticated);
        }

        [Test]
        public void WithAuthorizedNotCalledShouldNotHaveAuthorizedUser()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>();

            controllerBuilder
                .Calling(c => c.Authorized())
                .ShouldReturnNotFound();

            var controllerUser = controllerBuilder.Controller.User;

            Assert.AreEqual(false, controllerUser.IsInRole("Any"));
            Assert.AreEqual(null, controllerUser.Identity.Name);
            Assert.AreEqual(null, controllerUser.Identity.AuthenticationType);
            Assert.AreEqual(false, controllerUser.Identity.IsAuthenticated);
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            var actionName = actionResultTestBuilder.ActionName;
            var actionResult = actionResultTestBuilder.ActionResult;
            
            Assert.IsNotNullOrEmpty(actionName);
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(expectedActionName, actionName);
            Assert.IsAssignableFrom<OkResult>(actionResult);
        }
    }
}
