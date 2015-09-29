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

namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldHave
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ShouldHaveActionAttributeTests
    {
        [Test]
        public void NoActionAttributesShouldNotThrowExceptionWithActionContainingNoAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Test]
        public void NoActionAttributesShouldNotThrowExceptionWithVoidActionContainingNoAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to not have any action attributes, but in had some.")]
        public void NoActionAttributesShouldThrowExceptionWithActionContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .NoActionAttributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling EmptyActionWithAttributes action in WebApiController expected action to not have any action attributes, but in had some.")]
        public void NoActionAttributesShouldThrowExceptionWithVoidActionContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithAttributes())
                .ShouldHave()
                .NoActionAttributes();
        }
    }
}
