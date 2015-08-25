namespace MyWebApi.Tests.BuildersTests.UnauthorizedResultsTests
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using Common;
    using Exceptions;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class UnauthorizedResultTestBuilderTests
    {
        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithScheme()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("TestScheme");
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderWithScheme()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("Scheme");
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithSchemeEnum()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(AuthenticationScheme.Basic);
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Anonymous scheme and no matter what parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderWithSchemeEnum()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(AuthenticationScheme.Anonymous);
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithSchemeAndParamter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("TestScheme", "TestParameter");
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and Parameter parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderWithSchemeAndParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("Scheme", "Parameter");
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeader()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("TestScheme"));
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("TestScheme", "TestParameter"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeader()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("Scheme"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and Parameter parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderWithParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("Scheme", "Parameter"));
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeaders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(new[]
                {
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("Basic"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter")
                });
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeadersInDifferentOrder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(new[]
                {
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
                    new AuthenticationHeaderValue("Basic")
                });
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have 2 authentication header challenges, but found 3.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyNotAllProvidedHeadersInDifferentOrder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(new[]
                {
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("Basic")
                });
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyAllProvidedHeadersWithWrongData()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(new[]
                {
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
                    new AuthenticationHeaderValue("Scheme")
                });
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeadersAndParams()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("Basic"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"));
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeadersInDifferentOrderAndParams()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
                    new AuthenticationHeaderValue("Basic"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have 2 authentication header challenges, but found 3.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyNotAllProvidedHeadersInDifferentOrderAndParams()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("Basic"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyAllProvidedHeadersWithWrongDataAndParams()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .WithAuthenticationHeaderChallenges(
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
                    new AuthenticationHeaderValue("Scheme"));
        }
    }
}
