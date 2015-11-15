// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ControllersTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ControllerTestBuilderTests
    {
        [Test]
        public void NoAttributesShouldNotThrowExceptionWithControllerContainingNoAttributes()
        {
            MyWebApi
                .Controller<NoAttributesController>()
                .ShouldHave()
                .NoAttributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to not have any attributes, but it had some.")]
        public void NoAttributesShouldThrowExceptionWithControllerContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .NoAttributes();
        }

        [Test]
        public void AttributesShouldNotThrowEceptionWithControllerContainingAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes();
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing NoAttributesController was expected to have at least 1 attribute, but in fact none was found.")]
        public void AttributesShouldThrowEceptionWithControllerContainingNoAttributes()
        {
            MyWebApi
                .Controller<NoAttributesController>()
                .ShouldHave()
                .Attributes();
        }

        [Test]
        public void AttributesShouldNotThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 2);
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have 10 attributes, but in fact found 2.")]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 10);
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have 1 attribute, but in fact found 2.")]
        public void AttributesShouldThrowEceptionWithControllerContainingNumberOfAttributesTestingWithOne()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(withTotalNumberOf: 1);
        }
    }
}
