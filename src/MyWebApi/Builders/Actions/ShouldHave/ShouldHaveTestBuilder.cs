namespace MyWebApi.Builders.Actions.ShouldHave
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;

    public partial class ShouldHaveTestBuilder<TActionResult>
        : BaseTestBuilderWithActionResult<TActionResult>, IShouldHaveTestBuilder<TActionResult>
    {
        public ShouldHaveTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TActionResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }
    }
}
