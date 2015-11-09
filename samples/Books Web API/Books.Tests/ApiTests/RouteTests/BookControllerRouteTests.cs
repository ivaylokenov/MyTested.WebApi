namespace Books.Tests.ApiTests.RouteTests
{
    using System.Net.Http;
    using Api.Controllers;
    using Api.Models.RequestModels;
    using MyTested.WebApi;
    using NUnit.Framework;

    [TestFixture]
    public class BookControllerRouteTests
    {
        [Test]
        public void GetShouldBeMappedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Books/Get")
                .WithHttpMethod(HttpMethod.Get)
                .To<BooksController>(c => c.Get());
        }

        [Test]
        public void GetByIdShouldBeMappedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("/Books/GetById/1")
                .WithHttpMethod(HttpMethod.Get)
                .To<BooksController>(c => c.Get(1));
        }

        [Test]
        public void PostShouldBeMappedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Books/Post")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{""Title"":""Valid Title"",""Description"":""Valid Description"",""AuthorUsername"":""Valid""}")
                .To<BooksController>(c => c.Post(new BookRequestModel
                {
                    Title = "Valid Title",
                    Description = "Valid Description",
                    AuthorUsername = "Valid"
                }));
        }
    }
}
