namespace SourceControlSystem.Services.Data
{
    using System;
    using System.Linq;
    using Common.Constants;
    using Models;
    using SourceControlSystem.Data;
    using SourceControlSystem.Services.Data.Contracts;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<SoftwareProject> projects;
        private readonly IRepository<User> users;

        public ProjectsService(
            IRepository<SoftwareProject> projectsRepo,
            IRepository<User> usersRepo)
        {
            this.projects = projectsRepo;
            this.users = usersRepo;
        }

        public int Add(string name, string description, string creator, bool isPrivate = false)
        {
            var currentUser = this.users
                .All()
                .FirstOrDefault(u => u.UserName == creator);

            if (currentUser == null)
            {
                throw new ArgumentException("Current user cannot be found!");
            }

            var newProject = new SoftwareProject
            {
                Name = name,
                Description = description,
                Private = isPrivate,
                CreatedOn = DateTime.Now
            };

            newProject.Users.Add(currentUser);

            this.projects.Add(newProject);
            this.projects.SaveChanges();

            return newProject.Id;
        }

        public IQueryable<SoftwareProject> ById(string projectName, string username)
        {
            return this.projects
                .All()
                .Where(pr =>
                    pr.Name == projectName
                    && (!pr.Private
                        || (pr.Private
                            && pr.Users.Any(c => c.UserName == username))));
        }

        public IQueryable<SoftwareProject> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            return this.projects
                .All()
                .OrderByDescending(pr => pr.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
