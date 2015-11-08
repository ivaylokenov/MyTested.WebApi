// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.ModelsTests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    
    [TestFixture]
    public class ModelErrorDetailsTestBuilderTests
    {
        [Test]
        public void ThatEqualsShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).ThatEquals("The RequiredString field is required.")
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.RequiredString)
                .AndAlso()
                .ContainingNoModelStateErrorFor(m => m.NotValidateInteger)
                .AndAlso()
                .ContainingModelStateError("RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).ThatEquals(string.Format("The field Integer must be between {0} and {1}.", 1, int.MaxValue))
                .ContainingModelStateError("RequiredString")
                .ContainingModelStateError("Integer")
                .ContainingNoModelStateErrorFor(m => m.NotValidateInteger);
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected error message for key RequiredString to be 'RequiredString field is required.', but instead found 'The RequiredString field is required.'.")]
        public void ThatEqualsShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .AndAlso()
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
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).BeginningWith("The field Integer");
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected error message for key 'RequiredString' to begin with 'RequiredString', but instead found 'The RequiredString field is required.'.")]
        public void BeginningWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
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
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
                .ContainingModelStateErrorFor(m => m.Integer).EndingWith(string.Format("{0} and {1}.", 1, int.MaxValue));
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected error message for key 'RequiredString' to end with 'required!', but instead found 'The RequiredString field is required.'.")]
        public void EngingWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
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
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("required")
                .ContainingModelStateErrorFor(m => m.Integer).Containing("between");
        }

        [Test]
        [ExpectedException(
            typeof(ModelErrorAssertionException),
            ExpectedMessage = "When calling ModelStateCheck action in WebApiController expected error message for key 'RequiredString' to contain 'invalid', but instead found 'The RequiredString field is required.'.")]
        public void ContainingShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("invalid")
                .ContainingModelStateErrorFor(m => m.Integer).Containing("invalid");
        }
    }
}
