namespace MyWebApi.Tests.Builders
{
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using ControllerSetups;

    using global::MyWebApi.Builders.Contracts;

    using NUnit.Framework;

    [TestFixture]
    public class ControllerBuilderTests
    {
        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.EmptyAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "EmptyAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.AsyncEmptyAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncEmptyAction");
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
