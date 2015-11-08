// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ModelErrorTestBuilderTests
    {
        [Test]
        public void ContainingNoErrorsShouldNotThrowExceptionWhenThereAreNoModelStateErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling OkResultActionWithRequestBody action in WebApiController expected to have valid model state with no errors, but it had some.")]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingNoModelStateErrors();
        }

        [Test]
        public void AndModelStateErrorShouldNotThrowExceptionWhenTheProvidedModelStateErrorExists()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingModelStateError("RequiredString");
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have a model error against key Name, but none found.")]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExist()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingModelStateError("Name");
        }

        [Test]
        public void AndModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have a model error against key RequiredString, but none found.")]
        public void AndModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have no model errors against key RequiredString, but found some.")]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenChainedWithValidModel()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.Integer)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have no model errors against key Integer, but found some.")]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenChainedWithInvalidModel()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingNoModelStateErrorFor(r => r.Integer)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Test]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModel()
        {
            var responseModel = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .AndProvideTheModel();

            Assert.IsNotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.AreEqual(2, responseModel.Count);
        }

        [Test]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithPassing()
        {
            var responseModel = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .Passing(m => m.Count == 2)
                .AndProvideTheModel();

            Assert.IsNotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.AreEqual(2, responseModel.Count);
        }

        [Test]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateCheck()
        {
            var responseModel = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .ContainingNoModelStateErrorFor(m => m.Count)
                .AndProvideTheModel();

            Assert.IsNotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.AreEqual(2, responseModel.Count);
        }

        [Test]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateError()
        {
            var responseModel = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingModelStateError("Test")
                .AndProvideTheModel();

            Assert.IsNotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.AreEqual(2, responseModel.Count);
        }

        [Test]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndErrorCheck()
        {
            var responseModel = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingModelStateError("Test").ThatEquals("Test error")
                .AndProvideTheModel();

            Assert.IsNotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.AreEqual(2, responseModel.Count);
        }
    }
}
