namespace MyWebApi.Tests.BuildersTests.ResultsTests
{
    using System.Collections.Generic;

    using ControllerSetups;
    using ControllerSetups.Models;
    using Exceptions;

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
                .ShouldReturn<bool>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithStructTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn(typeof(bool));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<IList<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(IList<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericDefinitionResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(IList<>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ICollection<ResponseModel>>();
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(ICollection<ResponseModel>));
        }

        [Test]
        public void ShouldReturnShouldNotThrowExceptionWithClassGenericDefinitionTypesAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(ICollection<>));
        }

        [Test]
        [ExpectedException(typeof(HttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ResponseModel>();
        }

        [Test]
        [ExpectedException(typeof(HttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(ResponseModel));
        }

        [Test]
        [ExpectedException(typeof(HttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ICollection<int>>();
        }

        [Test]
        [ExpectedException(typeof(HttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResultAndTypeOf()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn(typeof(ICollection<int>));
        }
    }
}
