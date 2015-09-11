// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Tests.BuildersTests.ModelsTests
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
