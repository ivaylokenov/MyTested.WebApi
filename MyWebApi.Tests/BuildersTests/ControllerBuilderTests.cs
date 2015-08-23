namespace MyWebApi.Tests.BuildersTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Builders.Contracts;
    using ControllerSetups;
    using ControllerSetups.Models;

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

            CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Test]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .CallingAsync(c => c.AsyncOkResultAction());

            CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        [Test]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrors()
        {
            var requestBody = TestObjectFactory.GetRequestModelWithErrors();

            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody));

            var modelState = actionResultTestBuilder.Controller.ModelState;

            Assert.IsFalse(modelState.IsValid);
            Assert.AreEqual(2, modelState.Values.Count);
            Assert.AreEqual("Integer", modelState.Keys.First());
            Assert.AreEqual("RequiredString", modelState.Keys.Last());
        }

        [Test]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            var actionResultTestBuilder = MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody));

            var modelState = actionResultTestBuilder.Controller.ModelState;

            Assert.IsTrue(modelState.IsValid);
            Assert.AreEqual(0, modelState.Values.Count);
            Assert.AreEqual(0, modelState.Keys.Count);
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            var actionName = actionResultTestBuilder.ActionName;
            var actionResult = actionResultTestBuilder.ActionResult;
            
            Assert.IsNotNullOrEmpty(actionName);
            Assert.IsNotNull(actionResult);

            Assert.AreEqual(expectedActionName, actionName);
            Assert.IsAssignableFrom<OkResult>(actionResult);
        }
    }
}
