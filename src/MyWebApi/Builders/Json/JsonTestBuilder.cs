namespace MyWebApi.Builders.Json
{
    using System;
    using System.Web.Http;
    using Contracts.Json;
    using Models;

    public class JsonTestBuilder<TActionResult> : BaseResponseModelTestBuilder<TActionResult>, IJsonTestBuilder
    {
        public JsonTestBuilder(
            ApiController controller,
            string actionName, 
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
    }
}
