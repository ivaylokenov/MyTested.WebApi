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

namespace MyWebApi.Tests.BuildersTests.HttpActionResultsTests.UnauthorizedTests
{
    using Exceptions;
    using Setups.Controllers;
    using NUnit.Framework;

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
