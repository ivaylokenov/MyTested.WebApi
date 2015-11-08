// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
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
