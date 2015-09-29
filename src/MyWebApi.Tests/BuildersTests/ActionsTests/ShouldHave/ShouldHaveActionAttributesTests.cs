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
    public class ShouldHaveActionAttributesTests
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

        [Test]
        public void ActionAttributesShouldNotThrowEceptionWithActionContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action to have at least 1 action attribute, but in fact none was found.")]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNoAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldHave()
                .ActionAttributes();
        }

        [Test]
        public void ActionAttributesShouldNotThrowEceptionWithVoidActionContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithAttributes())
                .ShouldHave()
                .ActionAttributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling EmptyAction action in WebApiController expected action to have at least 1 action attribute, but in fact none was found.")]
        public void ActionAttributesShouldThrowEceptionWithVoidActionContainingNoAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction())
                .ShouldHave()
                .ActionAttributes();
        }

        [Test]
        public void ActionAttributesShouldNotThrowEceptionWithActionContainingNumberOfAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(withTotalNumberOf: 3);
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to have 10 action attributes, but in fact found 3.")]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNumberOfAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(withTotalNumberOf: 10);
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to have 1 action attribute, but in fact found 3.")]
        public void ActionAttributesShouldThrowEceptionWithActionContainingNumberOfAttributesTestingWithOne()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldHave()
                .ActionAttributes(withTotalNumberOf: 1);
        }
    }
}
