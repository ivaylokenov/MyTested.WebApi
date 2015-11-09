namespace Books.Tests.ApiTests.IntegrationTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Api;
    using Api.Models.ResponseModels;
    using Data;
    using Mocks;
    using Models;
    using MyTested.WebApi;
    using MyTested.WebApi.Builders.Contracts.Servers;
    using NUnit.Framework;

    [TestFixture]
    public class BooksControllerIntegrationTests
    {
        private IServerBuilder server;

        [TestFixtureSetUp]
        public void Init()
        {
            NinjectConfig.RebindAction = kernel =>
            {
                kernel.Rebind<IRepository<Book>>().ToConstant(MocksFactory.BooksRepository);
                kernel.Rebind<IRepository<Author>>().ToConstant(MocksFactory.AuthorsRepository);
            };

            MyWebApi.Server().Starts<Startup>();
            this.server = MyWebApi.Server().Working();
        }

        [Test]
        public void BooksControllerShouldReturnCorrectBooksForUnauthorizedUsers()
        {
            server
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("/api/Books/Get")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(m => m.Count == 3);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            MyWebApi.Server().Stops();
        }
    }
}
