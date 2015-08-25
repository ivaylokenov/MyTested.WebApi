namespace MyWebApi.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Exceptions;
    using Setups;
    using Setups.Models;

    using NUnit.Framework;

    [TestFixture]
    public class ResponseModelDetailsTestBuilderTests
    {
        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(2, m.Count);
                    Assert.AreEqual(1, m.First().IntegerValue);
                });
        }

        [Test]
        [ExpectedException(
            typeof(AssertionException))]
        public void WithResponseModelShouldThrowExceptionWithIncorrectAssertions()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m =>
                {
                    Assert.AreEqual(1, m.First().IntegerValue);
                    Assert.AreEqual(3, m.Count);
                });
        }

        [Test]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPredicate()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 1);
        }

        [Test]
        [ExpectedException(
            typeof(ResponseModelAssertionException),
            ExpectedMessage = "When calling OkResultWithResponse action in WebApiController expected response model IList<ResponseModel> to pass the given condition, but it failed.")]
        public void WithResponseModelShouldThrowExceptionWithWrongPredicate()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturnOk()
                .WithResponseModelOfType<IList<ResponseModel>>()
                .Passing(m => m.First().IntegerValue == 2);
        }
    }
}
