namespace MyWebApi.Builders.Json
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Json;

    public class JsonTestBuilder : BaseTestBuilder, IJsonTestBuilder
    {
        public JsonTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }
    }
}
