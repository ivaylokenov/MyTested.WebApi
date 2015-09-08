namespace MyWebApi.Builders.Created
{
    using System;
    using System.Web.Http;
    using Contracts.Created;
    using Models;

    public class CreatedTestBuilder<TCreatedResult>
        : BaseResponseModelTestBuilder<TCreatedResult>, ICreatedTestBuilder
    {
        public CreatedTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TCreatedResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
    }
}
