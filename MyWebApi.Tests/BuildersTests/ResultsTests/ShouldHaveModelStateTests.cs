namespace MyWebApi.Tests.BuildersTests.ResultsTests
{
    using ControllerSetups;
    using ControllerSetups.Models;
    using Exceptions;
    using NUnit.Framework;

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
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(r => r.Id)
                .ContainingModelStateErrorFor(r => r.Name);
        }

        [Test]
        public void ShouldHaveValidModelStateShouldBeValidWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHaveValidModelState();
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void ShouldHaveValidModelStateShouldThrowExceptionWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveValidModelState();
        }

        [Test]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveInvalidModelState();
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHaveInvalidModelState();
        }
    }
}
