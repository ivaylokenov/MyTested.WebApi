namespace MyWebApi
{
    using System;
    using System.Web.Http;

    public static class MyWebApi
    {
        public static void Controller<TController>()
            where TController : ApiController
        {

        }

        public static void Controller<TController>(Func<TController> construction)
            where TController : ApiController
        {

        }
    }
}
