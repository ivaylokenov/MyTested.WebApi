namespace Books.Tests.Mocks
{
    using Api;
    using Data;
    using Identity;
    using Models;
    using Repositories;

    public class MocksFactory
    {
        public static IRepository<Book> BooksRepository
        {
            get { return BooksRepositoryMock.Create(); }
        }

        public static IRepository<Author> AuthorsRepository
        {
            get { return AuthorsRepositoryMock.Create(); }
        }

        public static ApplicationUserManager ApplicationUserManager
        {
            get { return ApplicationUserManagerMock.Create(); }
        }
    }
}
