namespace SourceControlSystem.Services.Data.Tests.TestObjects
{
    using System;
    using SourceControlSystem.Models;

    public static class TestObjectFactory
    {
        public static InMemoryRepository<SoftwareProject> GetProjectsRepository(int numberOfProjects = 25)
        {
            var repo = new InMemoryRepository<SoftwareProject>();

            for (int i = 0; i < numberOfProjects; i++)
            {
                var date = new DateTime(2015, 11, 5, 23, 47, 12);
                date.AddDays(i);

                repo.Add(new SoftwareProject
                {
                    Id = i,
                    CreatedOn = date,
                    Description = "Test Description " + i,
                    Name = "Test " + i,
                    Private = true
                });
            }

            return repo;
        }

        public static InMemoryRepository<User> GetUsersRepository(int numberOfUsers = 25)
        {
            var repo = new InMemoryRepository<User>();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var date = new DateTime(2015, 11, 5, 23, 47, 12);
                date.AddDays(i);

                repo.Add(new User
                {
                    UserName = "Test User " + i,
                    Email = "Test Email " + i,
                    FirstName = "Test name " + i,
                    LastName = "Test name " + i,
                    PasswordHash = Guid.NewGuid().ToString()
                });
            }

            return repo;
        }
    }
}
