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
                .ShouldReturnOk();
        }
    }
}
