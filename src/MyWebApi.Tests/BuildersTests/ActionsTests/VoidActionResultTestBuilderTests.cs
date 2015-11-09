// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ActionsTests
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class VoidActionResultTestBuilderTests
    {
        [Test]
        public void ShouldReturnEmptyShouldNotThrowExceptionWithNormalVoidAction()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction())
                .ShouldReturnEmpty();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.")]
        public void ShouldReturnEmptyShouldThrowExceptionIfActionThrowsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldReturnEmpty();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.")]
        public void ShouldReturnEmptyWithAsyncShouldThrowExceptionIfActionThrowsException()
        {
            MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.EmptyActionWithExceptionAsync())
                .ShouldReturnEmpty();
        }

        [Test]
        public void ShouldThrowExceptionShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyActionWithException())
                .ShouldThrow()
                .Exception()
                .OfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling EmptyAction action in WebApiController expected to have invalid model state, but was in fact valid.")]
        public void ShouldHaveModelStateShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction())
                .ShouldHave()
                .InvalidModelState();
        }
    }
}
