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

namespace MyWebApi.Tests.BuildersTests.HttpActionResultsTests.RedirectTests
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

        [Test]
        public void ToShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.WithParameter(1));
        }

        [Test]
        public void ToShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectToRouteVoidAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectToRouteAction action in WebApiController expected redirect result to redirect to '/api/Redirect/WithParameter?id=2', but in fact redirected to '/api/Redirect/WithParameter?id=1'.")]
        public void ToShouldThrowExceptionWithIncorrectActionParameter()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect()
                .To<NoAttributesController>(c => c.WithParameter(2));
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectToRouteAction action in WebApiController expected redirect result to redirect to a specific URI, but such URI could not be resolved from the 'Redirect' route template.")]
        public void ToShouldThrowExceptionWithIncorrectActionCall()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect()
                .To<RouteController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(RedirectResultAssertionException),
            ExpectedMessage = "When calling RedirectAction action in WebApiController expected redirect result to contain route name, but it could not be found.")]
        public void ToShouldThrowExceptionWithIncorrectActionResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.RedirectAction())
                .ShouldReturn()
                .Redirect()
                .To<RouteController>(c => c.VoidAction());
        }
    }
}
