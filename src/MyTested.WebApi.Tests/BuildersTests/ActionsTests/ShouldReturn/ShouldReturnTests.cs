// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class ShouldReturnTests
    {
        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithStructTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .ResultOfType<bool>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithStructTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .ResultOfType(typeof(bool));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<IResponseModel>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(IResponseModel));
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndInterfaceReturn()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndTypeOfAndInterfaceReturn()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericInterfaceAction action in WebApiController expected action result to be ICollection<T>, but instead received ResponseModel.")]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndTypeOfAndInterfaceReturnWithInterface()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndInterfaceReturn()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType<IResponseModel>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndTypeOfAndInterfaceReturn()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType(typeof(IResponseModel));
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<IList<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithNotInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType<IList<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithListCollection action in WebApiController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldNotExceptionWithOtherGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType<HashSet<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotExceptionWithConcreteGenericResultWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(List<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithNotInheritedGenericResultWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResultWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<ResponseModel>));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithListCollection action in WebApiController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithOtherGenericResultWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(HashSet<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericDefinitionAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(List<>));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be HashSet<T>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentWrongGenericDefinitionAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(HashSet<>));
        }

        [Test]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericDefinitionResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithCollectionOfClassTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithCollectionOfClassTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<ResponseModel>));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithCollectionOfClassTypesWithInterface()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<IResponseModel>>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithCollectionOfClassTypesAndTypeOfWithInterface()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<IResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassGenericDefinitionTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>));
        }

        [Test]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>))
                .Passing(c => c.Count == 2);
        }

        [Test]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithGenericDefinition()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>()
                .Passing(c => c.Count == 2);
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.")]
        public void ShouldReturnShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ActionWithException())
                .ShouldReturn()
                .ResultOfType<IHttpActionResult>();
        }

        [Test]
        [ExpectedException(
            typeof(InvalidCallAssertionException),
            ExpectedMessage = "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.")]
        public void ShouldReturnWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.ActionWithExceptionAsync())
                .ShouldReturn()
                .ResultOfType<IHttpActionResult>();
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected response model ICollection<ResponseModel> to pass the given condition, but it failed.")]
        public void ShouldReturnShouldThrowExceptionWithModelDetailsTestsWithGenericDefinitionAndIncorrectAssertion()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>()
                .Passing(c => c.Count == 1);
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ResponseModel, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ResponseModel, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<int>>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericActionWithCollection action in WebApiController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<int>));
        }

        [Test]
        public void DynamicResultShouldBeProperlyRecognised()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.DynamicResult())
                .ShouldReturn()
                .ResultOfType<List<ResponseModel>>();
        }
    }
}
