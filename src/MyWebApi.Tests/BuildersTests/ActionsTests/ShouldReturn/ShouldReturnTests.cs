namespace MyWebApi.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Exceptions;
    using Setups.Controllers;
    using Setups.Models;
    using NUnit.Framework;

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
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<IList<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(IList<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericDefinitionResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(IList<>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassGenericDefinitionTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>));
        }

        [Test]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>))
                .Passing(c => c.Count == 2);
        }

        [Test]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithGenericDefinition()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>()
                .Passing(c => c.Count == 2);
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "ActionResult cannot be null.")]
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
            typeof(NullReferenceException),
            ExpectedMessage = "ActionResult cannot be null.")]
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
            ExpectedMessage = "When calling GenericAction action in WebApiController expected response model ICollection<ResponseModel> to pass the given condition, but it failed.")]
        public void ShouldReturnShouldThrowExceptionWithModelDetailsTestsWithGenericDefinitionAndIncorrectAssertion()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>()
                .Passing(c => c.Count == 1);
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericAction action in WebApiController expected action result to be ResponseModel, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericAction action in WebApiController expected action result to be ResponseModel, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericAction action in WebApiController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ICollection<int>>();
        }

        [Test]
        [ExpectedException(
            typeof(HttpActionResultAssertionException),
            ExpectedMessage = "When calling GenericAction action in WebApiController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.")]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<int>));
        }
    }
}
