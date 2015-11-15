namespace SourceControlSystem.Api.Tests.ControllerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;
    using Models.Projects;
    using System.Web.Http.Cors;
    using System.Collections.Generic;
    using System;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsControllerTests
    {
        private IControllerBuilder<ProjectsController> controller;

        [TestInitialize]
        public void Init()
        {
            this.controller = MyWebApi
                .Controller<ProjectsController>()
                .WithResolvedDependencies(TestObjectFactory.GetProjectsService());
        }

        [TestMethod]
        public void GetShouldHaveCorsEnabled()
        {
            controller
                .Calling(c => c.Get())
                .ShouldHave()
                .ActionAttributes(attr => attr.ContainingAttributeOfType<EnableCorsAttribute>());
        }

        [TestMethod]
        public void GetShouldReturnOkWithProperResponse()
        {
            controller
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<SoftwareProjectDetailsResponseModel>>()
                .Passing(pr => pr.Count == 1);
        }

        [TestMethod]
        public void GetShouldHaveAuthorizedAttribute()
        {
            controller
                .Calling(c => c.Get(string.Empty))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void GetShouldReturnNotFoundWhenProjectIsNull()
        {
            controller
                .WithAuthenticatedUser()
                .Calling(c => c.Get("Invalid"))
                .ShouldReturn()
                .NotFound();
        }

        [TestMethod]
        public void GetShouldReturnOkWhenProjectIsNotNull()
        {
            controller
                .WithAuthenticatedUser()
                .Calling(c => c.Get("Valid"))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<SoftwareProjectDetailsResponseModel>()
                .Passing(pr =>
                {
                    Assert.AreEqual(new DateTime(2015, 11, 5, 23, 47, 12), pr.CreatedOn);
                    Assert.AreEqual("Test", pr.Name);
                    Assert.AreEqual(0, pr.TotalUsers);
                });
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequestWithNullId()
        {
            controller
                .Calling(c => c.Get(null))
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Project name cannot be null or empty.");
        }
        
        [TestMethod]
        public void PostShouldValidateModelState()
        {
            controller
                .Calling(c => c.Post(TestObjectFactory.GetInvalidModel()))
                .ShouldHave()
                .ModelStateFor<SaveProjectRequestModel>()
                .ContainingModelStateErrorFor(m => m.Name);
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWithInvalidModel()
        {
            controller
                .Calling(c => c.Post(TestObjectFactory.GetInvalidModel()))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<SaveProjectRequestModel>()
                .ContainingModelStateErrorFor(m => m.Name);
        }

        [TestMethod]
        public void AllShouldReturnOk()
        {
            controller
                .Calling(c => c.Get(1, 10))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<SoftwareProjectDetailsResponseModel>>()
                .Passing(pr => pr.Count == 1);
        }
    }
}
