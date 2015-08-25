namespace MyWebApi
{
    using System;
    using System.Web.Http;

    using Builders;
    using Builders.Contracts;
    using Utilities;

    /// <summary>
    /// Starting point of the testing framework, which provides a way to specify the ASP.NET Web API controller to be tested.
    /// </summary>
    public static class MyWebApi
    {
        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controller = Reflection.TryCreateInstance<TController>();
            return Controller(() => controller);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function.
        /// </summary>
        /// <typeparam name="TController">Class inheriting ASP.NET Web API controller.</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller.</param>
        /// <returns>Controller builder used to build the test case.</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {
            var controllerInstance = construction();
            return new ControllerBuilder<TController>(controllerInstance);
        }
    }
}
