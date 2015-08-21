namespace MyWebApi
{
    using System;
    using System.Web.Http;
    using Builders;
    using Builders.Contracts;

    public static class MyWebApi
    {
        public static IControllerBuilder<TController> Controller<TController>()
            where TController : ApiController
        {
            var controllerInstance = Activator.CreateInstance<TController>();
            return new ControllerBuilder<TController>(controllerInstance);
        }

        public static IControllerBuilder<TController> Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {
            return new ControllerBuilder<TController>(construction());
        }
    }
}
