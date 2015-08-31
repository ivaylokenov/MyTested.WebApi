namespace MyWebApi.Tests.BuildersTests.ActionsTests
{
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class ShouldReturnBadRequestTests
    {
        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequest()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturnBadRequest();
        }

        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsInvalidModelStateResult()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturnBadRequest();
        }

        [Test]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequestErrorMessageResult()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturnBadRequest();
        }
    }
}
