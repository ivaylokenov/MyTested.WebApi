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
            var controller = MyWebApi.Controller<NormalController>().Controller;

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<NormalController>(controller);
        }

        [Test]
        public void ControllerWithConstructorFunctionShouldPopulateCorrectNewInstanceOfControllerType()
        {
            var controller = MyWebApi.Controller(() => new NormalController(new InjectedService())).Controller;

            Assert.IsNotNull(controller);
            Assert.IsAssignableFrom<NormalController>(controller);
            
            Assert.IsNotNull(controller.InjectedService);
            Assert.IsAssignableFrom<InjectedService>(controller.InjectedService);
        }
    }
}
