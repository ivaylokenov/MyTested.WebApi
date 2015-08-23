namespace MyWebApi.Tests.BuildersTests.ResponseModelsTests
{
    using ControllerSetups;
    using ControllerSetups.Models;
    using Exceptions;
    using NUnit.Framework;

    [TestFixture]
    public class ResponseModelErrorDetailsTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).ThatEquals("The RequiredString field is required.")
                .ContainingModelStateErrorFor(m => m.Integer).ThatEquals(string.Format("The field Integer must be between {0} and {1}.", 1, int.MaxValue));
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void ThatEqualsShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).ThatEquals("RequiredString field is required.")
                .ContainingModelStateErrorFor(m => m.Integer).ThatEquals(string.Format("Integer must be between {0} and {1}.", 1, int.MaxValue));
        }

        [Test]
        public void BeginningWithShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).BeginningWith("The field Integer");
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void BeginningWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).BeginningWith("Integer");
        }

        [Test]
        public void EngingWithShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
                .ContainingModelStateErrorFor(m => m.Integer).EndingWith(string.Format("{0} and {1}.", 1, int.MaxValue));
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void EngingWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required!")
                .ContainingModelStateErrorFor(m => m.Integer).EndingWith(string.Format("{0} and {1}!", 1, int.MaxValue));
        }

        [Test]
        public void ContainingShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("required")
                .ContainingModelStateErrorFor(m => m.Integer).Containing("between");
        }

        [Test]
        [ExpectedException(typeof(ResponseModelErrorAssertionException))]
        public void ContainingShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHaveModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("invalid")
                .ContainingModelStateErrorFor(m => m.Integer).Containing("invalid");
        }
    }
}
