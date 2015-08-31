namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Exceptions;

    public class ExceptionTestBuilder : BaseTestBuilder, IExceptionTestBuilder
    {
        private Exception exception;

        public ExceptionTestBuilder(ApiController controller, string actionName, Exception exception)
            : base(controller, actionName)
        {
            this.exception = exception;
        }
    }
}
