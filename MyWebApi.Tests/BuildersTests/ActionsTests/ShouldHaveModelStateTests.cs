namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using Exceptions;

    using NUnit.Framework;

    using Setups;
    using Setups.Models;

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
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have valid model state with no errors, but it had some.")]
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
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected to have invalid model state, but was in fact valid.")]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHaveInvalidModelState();
        }

        [Test]
        public void AndShouldWorkCorrectlyWithValidModelState()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();
            
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHaveValidModelState()
                .AndAlso()
                .ShouldReturnOk();
        }

        [Test]
        public void AndShouldWorkCorrectlyWithInvalidModelState()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveInvalidModelState()
                .AndAlso()
                .ShouldReturnOk();
        }
    }
}
