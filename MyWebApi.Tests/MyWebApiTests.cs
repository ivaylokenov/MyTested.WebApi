namespace MyWebApi.Tests
{
    using ControllerSetups;

    using NUnit.Framework;

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
    }
}
