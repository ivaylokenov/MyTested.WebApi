namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;
    using ControllerSetups;
    using ControllerSetups.Models;

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
