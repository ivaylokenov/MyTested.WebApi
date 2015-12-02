namespace SourceControlSystem.Api.Tests.ControllerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;
    using Models.Commits;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net;
    using MyTested.WebApi;

    [TestClass]
    public class CommitsControllerTests
    {
        private IControllerBuilder<CommitsController> controller; 

        [TestInitialize]
        public void Init()
        {
            this.controller = MyWebApi
                .Controller<CommitsController>()
                .WithResolvedDependencies(TestObjectFactory.GetCommitsService());
        }

        [TestMethod]
        public void GetByProjectIdShouldReturnOkResultWithData()
        {
            controller
                .Calling(c => c.GetByProjectId(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ListedCommitResponseModel>>()
                .Passing(m => m.Count == 1);
        }

        [TestMethod]
        public void UserHasCommitsShouldReturnUnauthorizedStatusCodeWithNoRequestHeader()
        {
            controller
                .Calling(c => c.UserHasCommits(With.Any<string>()))
                .ShouldReturn()
                .HttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public void ControllerShouldHaveRoutePrefix()
        {
            controller
                .ShouldHave()
                .Attributes(attr => attr.ChangingRoutePrefixTo("api/Commits"));
        }

        [TestMethod]
        public void UserHasCommitsShouldReturnFoundStatusCodeWithRequestHeader()
        {
            controller
                .WithHttpRequestMessage(req => req.WithHeader("MyCustomHeader", "MyValue"))
                .WithAuthenticatedUser()
                .Calling(c => c.UserHasCommits("User with commit"))
                .ShouldReturn()
                .HttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Found)
                .ContainingHeader(HttpHeader.Location, "http://telerikacademy.com/")
                .WithContentOfType<ObjectContent<bool>>()
                .WithResponseModel(true);
        }
    }
}
