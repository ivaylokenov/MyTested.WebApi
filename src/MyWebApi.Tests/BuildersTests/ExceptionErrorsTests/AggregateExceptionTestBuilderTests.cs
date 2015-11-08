// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.ExceptionErrorsTests
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class AggregateExceptionTestBuilderTests
    {
        [Test]
        public void ContainingInnerExceptionOfTypeShouldNotThrowIfInnerExceptionIsCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidExceptionAssertionException),
            ExpectedMessage = "When calling ActionWithAggregateException action in WebApiController expected AggregateException to contain ArgumentException, but none was found.")]
        public void ContainingInnerExceptionOfTypeShouldThrowIfInnerExceptionIsNotCorrect()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<ArgumentException>();
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithAggregateException())
                .ShouldThrow()
                .AggregateException()
                .ContainingInnerExceptionOfType<NullReferenceException>()
                .AndAlso()
                .ContainingInnerExceptionOfType<InvalidOperationException>();
        }
    }
}
