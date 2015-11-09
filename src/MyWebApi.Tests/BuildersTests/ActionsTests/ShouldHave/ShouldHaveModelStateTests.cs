// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ActionsTests.ShouldHave
{
    using System;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ShouldHaveModelStateTests
    {
        [Test]
        public void ShouldHaveModelStateForShouldChainCorrectly()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(r => r.NonRequiredString)
                .ContainingModelStateErrorFor(r => r.Integer)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void ShouldHaveValidModelStateShouldBeValidWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .ValidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have valid model state with no errors, but it had some.")]
        public void ShouldHaveValidModelStateShouldThrowExceptionWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ValidModelState();
        }

        [Test]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState();
        }

        [Test]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModelAndCorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState(2);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have invalid model state with 5 errors, but in fact contained 2.")]
        public void ShouldHaveInvalidModelStateShouldBeInvalidWithInvalidRequestModelAndIncorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState(5);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have invalid model state, but was in fact valid.")]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .InvalidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have invalid model state with 5 errors, but in fact contained 0.")]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModelAndProvidedNumberOfErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .InvalidModelState(withNumberOfErrors: 5);
        }

        [Test]
        public void AndShouldWorkCorrectlyWithValidModelState()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();
            
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Test]
        public void AndShouldWorkCorrectlyWithInvalidModelState()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "AndProvideTheModel can be used when there is response model from the action.")]
        public void AndProvideModelShouldThrowExceptionWhenIsCalledOnTheRequest()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(r => r.NonRequiredString)
                .ContainingModelStateErrorFor(r => r.Integer)
                .ContainingModelStateErrorFor(r => r.RequiredString)
                .AndProvideTheModel();
        }
    }
}
