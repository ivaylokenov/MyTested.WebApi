// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.BuildersTests.HttpActionResultsTests.UnauthorizedTests
{
    using Exceptions;
    using NUnit.Framework;
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge("TestScheme", "TestParameter")
                .AndAlso()
                .ContainingAuthenticationHeaderChallenge("Scheme", "Parameter");
        }
    }
}
