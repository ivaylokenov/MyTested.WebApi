namespace MyWebApi.Tests.BuildersTests.ResultsTests
{
    using System.Collections.Generic;

    using ControllerSetups;

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
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<IList<ResponseModel>>();
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
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ICollection<int>>();
        }
    }
}
