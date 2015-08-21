namespace MyWebApi
{
    using System;
    using System.Web.Http;

    using Builders;
    using Builders.Contracts;

    public static class MyWebApi
    {
        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated with default constructor
        /// </summary>
        /// <typeparam name="TController">Controller of type ApiController</typeparam>
        /// <returns>Controller builder used to build the test case</returns>
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controllerInstance = Activator.CreateInstance<TController>();
            return new ControllerBuilder<TController>(controllerInstance);
        }

        /// <summary>
        /// Selects controller on which the test will be executed. Controller is instantiated using construction function
        /// </summary>
        /// <typeparam name="TController">Controller of type ApiController</typeparam>
        /// <param name="construction">Construction function returning the instantiated controller</param>
        /// <returns>Controller builder used to build the test case</returns>
        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {
            return new ControllerBuilder<TController>(construction());
        }
    }
}
