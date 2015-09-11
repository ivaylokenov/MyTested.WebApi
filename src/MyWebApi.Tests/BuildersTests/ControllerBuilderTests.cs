// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.BuildersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Results;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
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

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCall()
        {
            var voidActionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCall()
        {
            var voidActionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");
        }

        [Test]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrors()
        {
            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            var controller = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = controller.ModelState;

            Assert.IsFalse(modelState.IsValid);
            Assert.AreEqual(2, modelState.Values.Count);
            Assert.AreEqual("Integer", modelState.Keys.First());
            Assert.AreEqual("RequiredString", modelState.Keys.Last());
        }

        [Test]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            var controller = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = controller.ModelState;

            Assert.IsTrue(modelState.IsValid);
            Assert.AreEqual(0, modelState.Values.Count);
            Assert.AreEqual(0, modelState.Keys.Count);
        }

        [Test]
        public void WithoutValidationShouldNotValidateTheRequestModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();
        }

        [Test]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            var controllerBuilder = MyWebApi
                .Controller<WebApiController>()
                .WithAuthenticatedUser();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();

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
                .ShouldReturn()
                .Ok();

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
                .ShouldReturn()
                .NotFound();

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
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor(new RequestModel())
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .WithResolvedDependencyFor(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor(new RequestModel())
                .AndAlso()
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .AndAlso()
                .WithResolvedDependencyFor(new InjectedService())
                .AndAlso()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjects()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencies(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjects()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencies(new RequestModel(), new AnotherInjectedService(), new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "Dependency AnotherInjectedService is already registered for WebApiController controller.")]
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
            ExpectedMessage = "WebApiController could not be instantiated because it contains no constructor taking RequestModel, AnotherInjectedService, InjectedService, ResponseModel as parameters.")]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependencies()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .WithResolvedDependencyFor<ResponseModel>(new ResponseModel())
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            this.CheckActionName(actionResultTestBuilder, expectedActionName);
            var actionResult = actionResultTestBuilder.AndProvideTheActionResult();

            Assert.IsNotNull(actionResult);
            Assert.IsAssignableFrom<OkResult>(actionResult);
        }

        private void CheckActionName(IBaseTestBuilder testBuilder, string expectedActionName)
        {
            var actionName = testBuilder.AndProvideTheActionName();

            Assert.IsNotNullOrEmpty(actionName);
            Assert.AreEqual(expectedActionName, actionName);
        }
    }
}
