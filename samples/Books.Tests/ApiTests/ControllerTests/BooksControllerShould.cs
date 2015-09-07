namespace Books.Tests.ApiTests.ControllerTests
{
    using System.Collections.Generic;
    using System.Reflection;
    using Api.App_Start;
    using Api.Controllers;
    using Api.Models.ResponseModels;
    using Data;
    using Mocks;
    using Models;
    using MyWebApi;
    using NUnit.Framework;

    [TestFixture]
    public class BooksControllerShould
    {
        [SetUp]
        public void Initialize()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("Books.Api"));
        }

        [Test]
        public void ReturnThreeBooksWhenUserIsNotAuthorizedInGetAction()
        {
            MyWebApi
                .Controller<BooksController>()
                .WithResolvedDependencyFor<IRepository<Book>>(MocksFactory.BooksRepository)
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(c => c.Count == 3);
        }

        [Test]
        public void ReturnTenBooksWhenUserIsAuthorizedInGetAction()
        {
            MyWebApi
                .Controller<BooksController>()
                .WithResolvedDependencyFor<IRepository<Book>>(MocksFactory.BooksRepository)
                .WithAuthenticatedUser()
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(c => c.Count == 10);
        }

        [Test]
        public void ReturnUnathorizedWhenUserIsNotAuthorizedInGetByIdAction()
        {
            MyWebApi
                .Controller<BooksController>()
                .WithResolvedDependencyFor<IRepository<Book>>(MocksFactory.BooksRepository)
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .Unauthorized();
        }

        [Test]
        public void ReturnProperBookWhenUserIsAuthorizedInGetByIdAction()
        {
            MyWebApi
                .Controller<BooksController>()
                .WithResolvedDependencyFor<IRepository<Book>>(MocksFactory.BooksRepository)
                .WithAuthenticatedUser()
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<BookResponseModel>()
                .Passing(b => b.Id == 1);
        }
    }
}
