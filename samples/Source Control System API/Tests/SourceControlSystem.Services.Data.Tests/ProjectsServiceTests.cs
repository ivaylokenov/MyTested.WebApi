namespace SourceControlSystem.Services.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Contracts;
    using System;
    using System.Linq;
    using TestObjects;
    using Models;

    [TestClass]
    public class ProjectsServiceTests
    {
        private IProjectsService projectsService;

        private InMemoryRepository<SoftwareProject> projectsRepo;
        private InMemoryRepository<User> userRepo;

        [TestInitialize]
        public void Init()
        {
            this.userRepo = TestObjectFactory.GetUsersRepository();
            this.projectsRepo = TestObjectFactory.GetProjectsRepository();

            this.projectsService = new ProjectsService(
                projectsRepo,
                this.userRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddShouldThrowExceptionWithNotFoundUser()
        {
            var result = this.projectsService.Add("Test", "Test", "Invalid User");
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.projectsService.Add("Test", "Test", "Test User 1");

            Assert.AreEqual(1, this.projectsRepo.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateUserAndProjectToDatabase()
        {
            var result = this.projectsService.Add("Test", "Test Description", "Test User 1");

            var project = this.projectsRepo.All().FirstOrDefault(pr => pr.Name == "Test");

            Assert.IsNotNull(project);
            Assert.AreEqual("Test", project.Name);
            Assert.AreEqual("Test Description", project.Description);
            Assert.AreEqual(1, project.Users.Count);
            Assert.AreEqual("Test User 1", project.Users.First().UserName);
        }
    }
}
