namespace MyWebApi.Tests.BuildersTests.BadRequestsTests
{
    using System.Web.Http.ModelBinding;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class BadRequestTestBuilderTests
    {
        [Test]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage();
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestAction action in WebApiController expected bad request result to contain error message, but it could not be found.")]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage();
        }

        [Test]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasCorrentErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Bad request");
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithErrorAction action in WebApiController expected bad request with message 'Good request', but instead received 'Bad request'.")]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveCorrentErrorMessage()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Good request");
        }

        [Test]
        public void WithModelStateShouldNotThrowExceptionWhenModelStateHasSameErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithModelState action in WebApiController expected bad request model state dictionary to contain String key, but none found.")]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasDifferentKeys()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("String", "The RequiredString field is required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithModelState action in WebApiController expected bad request model state dictionary to contain 1 keys, but found 2.")]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasLessNumberOfKeys()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithModelState action in WebApiController expected bad request model state dictionary to contain 3 keys, but found 2.")]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreNumberOfKeys()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");
            modelState.AddModelError("NonRequiredString", "The NonRequiredString field is required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithModelState action in WebApiController expected bad request with message 'The RequiredString field is not required.', but instead received 'The RequiredString field is required.'.")]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasWrongError()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is not required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        [ExpectedException(
            typeof(BadRequestResultAssertionException),
            ExpectedMessage = "When calling BadRequestWithModelState action in WebApiController expected bad request model state dictionary to contain 2 errors for RequiredString key, but found 1.")]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is not required.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelState(modelState);
        }

        [Test]
        public void WithModelStateForShouldNotThrowExceptionWhenModelStateHasSameErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<RequestModel>()
                .ContainingModelStateErrorFor(m => m.Integer).ThatEquals("The field Integer must be between 1 and 2147483647.")
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("field")
                .AndAlso()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString);
        }
    }
}
