namespace Books.Tests.ApiTests.ControllerTests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Api.Controllers;
    using Api.Models.RequestModels;
    using Api.Models.ResponseModels;
    using Mocks;
    using MyTested.WebApi;
    using NUnit.Framework;

    [TestFixture]
    public class BooksControllerTests
    {
        private IControllerBuilder<BooksController> controller;

        [SetUp]
        public void TestInit()
        {
            this.controller = MyWebApi
                .Controller<BooksController>()
                .WithResolvedDependencyFor(MocksFactory.BooksRepository)
                .WithResolvedDependencyFor(MocksFactory.AuthorsRepository);
        }

        [Test]
        public void AllowOnlyGetRequestsOnGetAction()
        {
            this.controller
                .Calling(c => c.Get())
                .ShouldHave()
                .ActionAttributes(attrs => attrs.RestrictingForRequestsWithMethod(HttpMethod.Get));
        }

        [Test]
        public void AllowOnlyAuthorizedGetRequestsOnGetByIdActionAndChangeTheRoute()
        {
            this.controller
                .Calling(c => c.Get(1))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForRequestsWithMethod(HttpMethod.Get)
                    .AndAlso()
                    .ChangingRouteTo("Books/GetById/{id}")
                    .AndAlso()
                    .RestrictingForAuthorizedRequests());
        }

        [Test]
        public void ReturnThreeBooksWhenUserIsNotAuthorizedInGetAction()
        {
            this.controller
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(c => c.Count == 3);
        }

        [Test]
        public void ReturnTenBooksWhenUserIsAuthorizedInGetAction()
        {
            this.controller
                .WithAuthenticatedUser()
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(c => c.Count == 10);
        }

        [Test]
        public void ReturnProperBookWhenUserIsAuthorizedInGetByIdAction()
        {
            this.controller
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<BookResponseModel>()
                .Passing(b => b.Id == 1);
        }

        [Test]
        public void ShouldAllowOnlyHttpPostOnBooksPostActionAndHaveRestrictionForAuthorizedUsers()
        {
            this.controller
                .Calling(c => c.Post(new BookRequestModel()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForRequestsWithMethod(HttpMethod.Post)
                    .AndAlso()
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<BookRequestModel>()
                .ContainingModelStateErrorFor(b => b.Title)
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.Description)
                .AndAlso()
                .ContainingModelStateErrorFor(b => b.Description);
        }

        [Test]
        public void ShouldReturnBadRequestWhenAuthorIsNull()
        {
            this.controller
                .Calling(c => c.Post(new BookRequestModel { Title = "Valid title", Description = "Valid description", AuthorUsername = "Invalid" }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Author does not exist!");
        }

        [Test]
        public void ShouldReturnCreatedWhenAuthorIsFoundAndModelStateIsValid()
        {
            this.controller
                .Calling(
                    c =>
                        c.Post(new BookRequestModel
                        {
                            Title = "Valid title",
                            Description = "Valid description",
                            AuthorUsername = "Valid"
                        }))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Created()
                .AtLocation("/Books/GetById/1")
                .WithResponseModelOfType<BookResponseModel>();
        }
    }
}
