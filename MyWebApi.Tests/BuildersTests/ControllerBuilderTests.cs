namespace MyWebApi.Tests.BuildersTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Builders.Contracts;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Models;
    using Setups.Services;

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
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>()
                .WithAuthenticatedUser();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturnOk();

            var controllerUser = controllerBuilder.Controller.User;

            Assert.AreEqual(false, controllerUser.IsInRole("Any"));
            Assert.AreEqual("TestUser", controllerUser.Identity.Name);
            Assert.AreEqual("Passport", controllerUser.Identity.AuthenticationType);
            Assert.AreEqual(true, controllerUser.Identity.IsAuthenticated);
        }

        [Test]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>()
                .WithAuthenticatedUser(user => user
                    .WithUsername("NewUserName")
                    .WithAuthenticationType("Custom")
                    .InRole("NormalUser")
                    .InRoles("Moderator", "Administrator")
                    .InRoles(new[]
                    {
                        "SuperUser",
                        "MegaUser"
                    }));

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturnOk();

            var controllerUser = controllerBuilder.Controller.User;

            Assert.AreEqual("NewUserName", controllerUser.Identity.Name);
            Assert.AreEqual("Custom", controllerUser.Identity.AuthenticationType);
            Assert.AreEqual(true, controllerUser.Identity.IsAuthenticated);
            Assert.AreEqual(true, controllerUser.IsInRole("NormalUser"));
            Assert.AreEqual(true, controllerUser.IsInRole("Moderator"));
            Assert.AreEqual(true, controllerUser.IsInRole("Administrator"));
            Assert.AreEqual(true, controllerUser.IsInRole("SuperUser"));
            Assert.AreEqual(true, controllerUser.IsInRole("MegaUser"));
            Assert.AreEqual(false, controllerUser.IsInRole("AnotherRole"));
        }

        [Test]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUser()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturnNotFound();

            var controllerUser = controllerBuilder.Controller.User;

            Assert.AreEqual(false, controllerUser.IsInRole("Any"));
            Assert.AreEqual(null, controllerUser.Identity.Name);
            Assert.AreEqual(null, controllerUser.Identity.AuthenticationType);
            Assert.AreEqual(false, controllerUser.Identity.IsAuthenticated);
        }

        [Test]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependencies()
        {
            var controller = MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .Controller;

            Assert.IsNotNull(controller);
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsNull(controller.AnotherInjectedService);
            Assert.IsNull(controller.InjectedRequestModel);
        }

        [Test]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependencies()
        {
            var controller = MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .Controller;

            Assert.IsNotNull(controller);
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsNotNull(controller.AnotherInjectedService);
            Assert.IsNull(controller.InjectedRequestModel);
        }

        [Test]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependencies()
        {
            var controller = MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .Controller;

            Assert.IsNotNull(controller);
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsNotNull(controller.AnotherInjectedService);
            Assert.IsNotNull(controller.InjectedRequestModel);
        }

        [Test]
        public void WithResolvedDependencyForShouldChooseContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturnOk();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "Dependency IAnotherInjectedService is already registered for WebApiController controller.")]
        public void WithResolvedDependencyForShouldThrowExceptionWhenSameDependenciesAreRegistered()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService());
        }

        [Test]
        [ExpectedException(
            typeof(UnresolvedDependenciesException),
            ExpectedMessage = "WebApiController controller could not be instantiated because it contains no constructor taking RequestModel, IAnotherInjectedService, IInjectedService, ResponseModel as parameters.")]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependencies()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .WithResolvedDependencyFor<ResponseModel>(new ResponseModel())
                .Calling(c => c.OkResultAction())
                .ShouldReturnOk();
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
