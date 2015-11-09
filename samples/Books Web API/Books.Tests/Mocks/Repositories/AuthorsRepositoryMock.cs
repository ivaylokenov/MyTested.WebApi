namespace Books.Tests.Mocks.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using Moq;

    public class AuthorsRepositoryMock
    {
        public static IRepository<Author> Create()
        {
            var listOfAuthors = new List<Author>
            {
                new Author
                {
                    UserName = "Valid"
                }
            };

            var repository = new Mock<IRepository<Author>>();
            repository.Setup(r => r.All()).Returns(listOfAuthors.AsQueryable());

            return repository.Object;
        }
    }
}
