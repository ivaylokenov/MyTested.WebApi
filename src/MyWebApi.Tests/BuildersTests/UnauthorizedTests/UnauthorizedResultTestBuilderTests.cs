// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.BuildersTests.UnauthorizedTests
{
    using System.Net.Http.Headers;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class UnauthorizedResultTestBuilderTests
    {
        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithScheme()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge("Scheme");
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithSchemeEnum()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(AuthenticationScheme.Anonymous);
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithSchemeAndParamter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge("Scheme", "Parameter");
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeader()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("TestScheme"));
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(new AuthenticationHeaderValue("Scheme", "Parameter"));
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(authHeader => authHeader.WithScheme(AuthenticationScheme.Basic));
        }

        [Test]
        public void ContainingAuthenticationHeaderChallengeShouldNotThrowExceptionWhenResultContainsTheProvidedHeaderWithParameterBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(authHeader =>
                    authHeader
                        .WithScheme("TestScheme")
                        .WithParameter("TestParameter"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(authHeader => authHeader.WithScheme("Scheme"));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and Parameter parameter, but none found.")]
        public void ContainingAuthenticationHeaderChallengeShouldThrowExceptionWhenResultDoesNotContainTheProvidedHeaderWithParameterBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .ContainingAuthenticationHeaderChallenge(authHeader =>
                    authHeader
                        .WithScheme("Scheme")
                        .WithParameter("Parameter"));
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeaders()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
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
                .ShouldReturn()
                .Unauthorized()
                .WithAuthenticationHeaderChallenges(
                    new AuthenticationHeaderValue("TestScheme", "TestParameter"),
                    new AuthenticationHeaderValue("YetAnotherScheme", "YetAnotherParameter"),
                    new AuthenticationHeaderValue("Scheme"));
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeadersAndParamsAndBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .WithAuthenticationHeaderChallenges(
                    authHeaders =>
                        authHeaders
                            .ContainingHeader(header => header.WithScheme("Basic"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("TestScheme").WithParameter("TestParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("YetAnotherScheme").WithParameter("YetAnotherParameter")));
        }

        [Test]
        public void WithAuthenticationHeaderChallengesShouldNotThrowExceptionWhenResultContainsExactlyAllProvidedHeadersInDifferentOrderAndParamsAndBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .WithAuthenticationHeaderChallenges(
                    authHeaders =>
                        authHeaders
                            .ContainingHeader(header => header.WithScheme("TestScheme").WithParameter("TestParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("YetAnotherScheme").WithParameter("YetAnotherParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("Basic")));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have 2 authentication header challenges, but found 3.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyNotAllProvidedHeadersInDifferentOrderAndParamsAndBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .WithAuthenticationHeaderChallenges(
                    authHeaders =>
                        authHeaders
                            .ContainingHeader(header => header.WithScheme("TestScheme").WithParameter("TestParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("Basic")));
        }

        [Test]
        [ExpectedException(
            typeof(UnauthorizedResultAssertionException),
            ExpectedMessage = "When calling UnauthorizedActionWithChallenges action in WebApiController expected to have authentication header challenge with Scheme scheme and no matter what parameter, but none found.")]
        public void WithAuthenticationHeaderChallengesShouldThrowExceptionWhenResultDoesNotContainExactlyAllProvidedHeadersWithWrongDataAndBuilder()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.UnauthorizedActionWithChallenges())
                .ShouldReturn()
                .Unauthorized()
                .WithAuthenticationHeaderChallenges(
                    authHeaders =>
                        authHeaders
                            .ContainingHeader(header => header.WithScheme("TestScheme").WithParameter("TestParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("YetAnotherScheme").WithParameter("YetAnotherParameter"))
                            .AndAlso()
                            .ContainingHeader(header => header.WithScheme("Scheme")));
        }
    }
}
