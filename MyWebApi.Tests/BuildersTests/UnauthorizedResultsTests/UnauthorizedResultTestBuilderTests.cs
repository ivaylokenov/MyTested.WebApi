namespace MyWebApi.Tests.BuildersTests.UnauthorizedResultsTests
{
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
    }
}
