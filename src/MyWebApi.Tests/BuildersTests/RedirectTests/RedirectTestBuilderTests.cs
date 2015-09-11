namespace MyWebApi.Tests.BuildersTests.RedirectTests
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class RedirectTestBuilderTests
    {
        [Test]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation("http://somehost.com/someuri/1?query=Test");
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectActionWithUri action in WebApiController expected redirect result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.")]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation("http://somehost.com/");
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectActionWithUri action in WebApiController expected redirect result location to be URI valid, but instead received http://somehost!@#?Query==true.")]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation("http://somehost!@#?Query==true");
        }

        [Test]
        public void AtLocationWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectActionWithUri action in WebApiController expected redirect result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.")]
        public void AtLocationWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation(new Uri("http://somehost.com/"));
        }

        [Test]
        public void AtLocationWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation(location =>
                    location
                        .WithHost("somehost.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectActionWithUri action in WebApiController expected redirect result URI to equal the provided one, but was in fact different.")]
        public void AtLocationWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectActionWithUri())
                .ShouldReturn()
                .Redirect()
                .AtLocation(location =>
                    location
                        .WithHost("somehost12.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }
    }
}
