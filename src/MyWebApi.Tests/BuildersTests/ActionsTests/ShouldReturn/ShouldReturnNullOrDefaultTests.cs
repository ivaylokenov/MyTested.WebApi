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

namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldReturnNullOrDefaultTests
    {
        [Test]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueIsNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .Null();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "Boolean cannot be null.")]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNullable()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .Null();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action result to be null, but instead received IHttpActionResult.")]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Null();
        }

        [Test]
        public void ShouldReturnNotNullShouldNotThrowExceptionWhenReturnValueIsNotNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .NotNull();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "Boolean cannot be null.")]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueIsNotNullable()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .NotNull();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling NullAction action in WebApiController expected action result to be not null, but it was IHttpActionResult object.")]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueIsNotNull()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .NotNull();
        }

        [Test]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIDefaultForClass()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Test]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIsNotDefaultForStructs()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.DefaultStructAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action result to be the default value of IHttpActionResult, but in fact it was not.")]
        public void ShouldReturnDefaultShouldThrowExceptionWhenReturnValueIsNotDefault()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .DefaultValue();
        }
    }
}
