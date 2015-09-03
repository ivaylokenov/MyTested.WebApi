namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using Contracts.ExceptionErrors;
    using ExceptionErrors;

    public class ShouldThrowTestBuilder : BaseTestBuilder, IShouldThrowTestBuilder
    {
        public ShouldThrowTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException)
            : base(controller, actionName, caughtException)
        {
        }

        public IExceptionTestBuilder Exception()
        {
            return new ExceptionTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }
    }
}
