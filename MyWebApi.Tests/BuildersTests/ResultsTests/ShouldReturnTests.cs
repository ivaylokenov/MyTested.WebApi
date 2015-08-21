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
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ICollection<ResponseModel>>();
        }

        [Test]
        [ExpectedException(typeof(IHttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ResponseModel>();
        }

        [Test]
        [ExpectedException(typeof(IHttpActionResultAssertionException))]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn<ICollection<int>>();
        }
    }
}
