namespace Books.Tests.Mocks
{
    using Data;
    using Models;
    using Repositories;

    public class MocksFactory
    {
        public static IRepository<Book> BooksRepository
        {
            get { return BooksRepositoryMock.Create(); }
        }
    }
}
