namespace MyWebApi.Tests.BuildersTests
{
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using global::MyWebApi.Builders.Contracts;

    using ControllerSetups;

    using NUnit.Framework;

    [TestFixture]
    public class ControllerBuilderTests
    {
        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            var actionName = actionResultTestBuilder.ActionName;
            var actionResult = actionResultTestBuilder.ActionResult;

            var testedActionResult = actionResult;
            if (actionResult is Task<TActionResult>)
            {
                testedActionResult = (actionResult as Task<TActionResult>).Result;
            }

            Assert.IsNotNullOrEmpty(actionName);
            Assert.IsNotNull(testedActionResult);

            Assert.AreEqual(expectedActionName, actionName);
            Assert.IsAssignableFrom<OkResult>(testedActionResult);
        }
    }
}
