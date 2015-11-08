// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
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
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to not have any attributes, but it had some.")]
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
            ExpectedMessage = "When calling EmptyActionWithAttributes action in WebApiController expected action to not have any attributes, but it had some.")]
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
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected action to have at least 1 attribute, but in fact none was found.")]
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
            ExpectedMessage = "When calling EmptyAction action in WebApiController expected action to have at least 1 attribute, but in fact none was found.")]
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
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to have 10 attributes, but in fact found 3.")]
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
            ExpectedMessage = "When calling NormalActionWithAttributes action in WebApiController expected action to have 1 attribute, but in fact found 3.")]
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
