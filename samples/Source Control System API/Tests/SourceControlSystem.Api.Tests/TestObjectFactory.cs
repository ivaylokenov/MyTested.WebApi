namespace SourceControlSystem.Api.Tests
{
    using System.Linq;
    using Models.Projects;
    using Moq;
    using Services.Data.Contracts;
    using System.Collections.Generic;
    using SourceControlSystem.Models;
    using System;

    public static class TestObjectFactory
    {
        private static IQueryable<SoftwareProject> projects = new List<SoftwareProject>
        {
            new SoftwareProject
            {
                CreatedOn = new DateTime(2015, 11, 5, 23, 47, 12),
                Description = "Test Description",
                Name = "Test",
                Private = true
            }
        }.AsQueryable();

        private static IQueryable<Commit> commits = new List<Commit>
        {
            new Commit
            {
                CreatedOn = new DateTime(2015, 11, 5, 23, 47, 12),
                User = new User
                {
                    UserName = "User with commit"
                },
                Id = 1,
            }
        }.AsQueryable();

        public static IProjectsService GetProjectsService()
        {
            var projectsService = new Mock<IProjectsService>();

            projectsService.Setup(pr => pr.All(
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(projects);

            projectsService.Setup(pr => pr.ById(
                    It.Is<string>(s => s == "Invalid"),
                    It.IsAny<string>()))
                .Returns(new List<SoftwareProject>().AsQueryable());

            projectsService.Setup(pr => pr.ById(
                    It.Is<string>(s => s == "Valid"),
                    It.IsAny<string>()))
                .Returns(projects);

            projectsService.Setup(pr => pr.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()))
                .Returns(1);

            return projectsService.Object;
        }
        
        public static ICommitsService GetCommitsService()
        {
            var commitsService = new Mock<ICommitsService>();

            commitsService.Setup(c => c.GetAllByProjectId(
                    It.IsAny<int>()))
                .Returns(commits);

            commitsService.Setup(c => c.UserHasCommits(
                    It.Is<string>(u => u == "User with commit")))
                .Returns(true);

            commitsService.Setup(c => c.UserHasCommits(
                    It.Is<string>(u => u != "User with commit")))
                .Returns(false);
                
            return commitsService.Object;
        }

        public static SaveProjectRequestModel GetInvalidModel()
        {
            return new SaveProjectRequestModel { Description = "TestDescription" };
        }
    }
}
