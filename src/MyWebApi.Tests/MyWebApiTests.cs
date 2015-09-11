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

namespace MyWebApi.Tests
{
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Services;

    [TestFixture]
    public class MyWebApiTests
    {
        [Test]
        public void ControllerWithoutConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyWebApi.Controller<WebApiController>().Controller;

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
        }

        [Test]
        public void ControllerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyWebApi.Controller(() => new WebApiController(new InjectedService())).Controller;

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
            
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsAssignableFrom<InjectedService>(controller.InjectedService);
        }

        [Test]
        public void ControllerWithProvidedInstanceShouldPopulateCorrectInstanceOfControllerType()
        {
            var instance = new WebApiController();
            var controller = MyWebApi.Controller(instance).Controller;

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<WebApiController>(controller);
        }

        [Test]
        [ExpectedException(
            typeof(UnresolvedDependenciesException),
            ExpectedMessage = "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking no parameters.")]
        public void ControllerWithNoParameterlessConstructorShouldThrowProperException()
        {
            MyWebApi
                .Controller<NoParameterlessConstructorController>()
                .Calling(c => c.OkAction())
                .ShouldReturn()
                .Ok();
        }
    }
}
