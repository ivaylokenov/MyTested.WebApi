namespace Books.Tests.Mocks.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using Moq;

    public class BooksRepositoryMock
    {
        public static IRepository<Book> Create()
        {
            var listOfBooks = new List<Book>();
            for (int i = 0; i < 20; i++)
            {
                listOfBooks.Add(new Book
                {
                    Id = i,
                    Title = "Book " + i,
                    Description = "Description" + i,
                    Author = new Author
                    {
                        FirstName = "FirstName " + i,
                        LastName = "LastName " + i
                    }
                });
            }

            var repository = new Mock<IRepository<Book>>();
            repository.Setup(r => r.All()).Returns(listOfBooks.AsQueryable());

            return repository.Object;
        }
    }
}
