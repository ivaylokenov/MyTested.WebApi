// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ResponseModelTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithIncorrectInheritedTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IList<ResponseModel>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectImplementatorTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultAction action in WebApiController expected response model of type ResponseModel, but instead received null.")]
        public void WithResponseModelShouldThrowExceptionWithNoResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithInterfaceResponse action in WebApiController expected response model to be a ResponseModel, but instead received a ICollection<ResponseModel>.")]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithInterfaceResponse action in WebApiController expected response model to be a ICollection<Int32>, but instead received a ICollection<ResponseModel>.")]
        public void WithResponseModelShouldThrowExceptionWithIncorrectGenericTypeArgument()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<int>>();
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller(() => controller)
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        public void WithResponceModelShouldNotThrowExceptionWithDeeplyEqualPassedExpectedObject()
        {
            var controller = new WebApiController();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(controller.ResponseModel);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected response model List<ResponseModel> to be the given model, but in fact it was a different model.")]
        public void WithResponceModelShouldThrowExceptionWithDifferentPassedExpectedObject()
        {
            var controller = new WebApiController();

            var another = controller.ResponseModel.ToList();
            another.Add(new ResponseModel());

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(another);
        }

        [Test]
        public void WithNoResponseModelShouldNotThrowExceptionWhenNoResponseModel()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .WithNoResponseModel();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected to not have response model but in fact response model was found.")]
        public void WithNoResponseModelShouldThrowExceptionWhenResponseModelExists()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithNoResponseModel();
        }
    }
}
