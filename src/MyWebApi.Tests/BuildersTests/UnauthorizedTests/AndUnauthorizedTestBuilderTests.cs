namespace MyWebApi.Tests.BuildersTests.UnauthorizedTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;

    [TestFixture]
    public class AndUnauthorizedTestBuilderTests
    {
        [Test]
        public void AndShouldReturnCorrectResultsWhenHeadersAreCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("TestScheme", "TestParameter")
                .AndAlso()
                .ContainingAuthenticationHeaderChallenge("Basic");
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and Parameter parameter, but none found.")]
        public void AndShouldThrowExceptionWhenHeadersAreInCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturnUnauthorized()
                .ContainingAuthenticationHeaderChallenge("TestScheme", "TestParameter")
                .AndAlso()
                .ContainingAuthenticationHeaderChallenge("Scheme", "Parameter");
        }
    }
}
