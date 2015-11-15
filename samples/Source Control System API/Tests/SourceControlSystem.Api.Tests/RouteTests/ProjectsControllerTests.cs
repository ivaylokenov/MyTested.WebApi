namespace SourceControlSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Controllers;
    using System.Net.Http;
    using Models.Projects;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsControllerTests
    {
        [TestMethod]
        public void GetShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Projects")
                .To<ProjectsController>(c => c.Get());
        }

        [TestMethod]
        public void GetWithIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Projects/Test")
                .To<ProjectsController>(c => c.Get("Test"));
        }

        [TestMethod]
        public void AllShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/all?page=1&pageSize=10")
                .To<ProjectsController>(c => c.Get(1, 10));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Projects")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""Name"": ""Test"", ""Private"": true }")
                .To<ProjectsController>(c => c.Post(new SaveProjectRequestModel
                {
                    Name = "Test",
                    Private = true
                }));
        }
    }
}
