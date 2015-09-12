namespace MyWebApi.Builders.Content
{
    using System;
    using System.Web.Http;
    using Contracts.Content;
    using Models;

    public class ContentTestBuilder<TContentResult>
        : BaseResponseModelTestBuilder<TContentResult>, IAndContentTestBuilder
    {
        public ContentTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TContentResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

        public IContentTestBuilder AndAlso()
        {
            return this;
        }
    }
}
