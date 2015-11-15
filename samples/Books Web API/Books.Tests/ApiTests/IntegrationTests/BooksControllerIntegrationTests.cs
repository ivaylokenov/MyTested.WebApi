namespace Books.Tests.ApiTests.IntegrationTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Api;
    using Api.Models.ResponseModels;
    using Data;
    using Microsoft.Owin.Security;
    using Mocks;
    using Models;
    using MyTested.WebApi;
    using MyTested.WebApi.Builders.Contracts.Servers;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class BooksControllerIntegrationTests
    {
        private IServerBuilder server;
        private string accessToken;

        [TestFixtureSetUp]
        public void Init()
        {
            NinjectConfig.RebindAction = kernel =>
            {
                kernel.Rebind<IRepository<Book>>().ToConstant(MocksFactory.BooksRepository);
                kernel.Rebind<IRepository<Author>>().ToConstant(MocksFactory.AuthorsRepository);
                kernel.Rebind<ApplicationUserManager>().ToConstant(MocksFactory.ApplicationUserManager);
            };

            MyWebApi.Server().Starts<Startup>();
            this.server = MyWebApi.Server().Working();

            this.GetAccessToken();
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

        [Test]
        public void BooksControllerShouldReturnCorrectBooksForAuthorizedUsers()
        {
            server
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("/api/Books/Get")
                    .WithMethod(HttpMethod.Get)
                    .WithHeader(HttpHeader.Authorization, "Bearer " + this.accessToken))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<List<BookResponseModel>>()
                .Passing(m => m.Count == 10);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            MyWebApi.Server().Stops();
        }

        private void GetAccessToken()
        {
            var message = server
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("/token")
                    .WithMethod(HttpMethod.Post)
                    .WithFormUrlEncodedContent("username=TestAuthor@test.com&password=testpass&grant_type=password"))
                .ShouldReturnHttpResponseMessage()
                .AndProvideTheHttpResponseMessage();

            var result = JObject.Parse(message.Content.ReadAsStringAsync().Result);
            this.accessToken = (string)result["access_token"];
        }
    }
}
