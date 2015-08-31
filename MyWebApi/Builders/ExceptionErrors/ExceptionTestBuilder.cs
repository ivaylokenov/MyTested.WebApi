namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.ExceptionErrors;

    public class ExceptionTestBuilder : BaseTestBuilder, IExceptionTestBuilder
    {
        private Exception exception;

        public ExceptionTestBuilder(ApiController controller, string actionName, Exception exception)
            : base(controller, actionName)
        {
            this.exception = exception;
        }

        public IExceptionTestBuilder OfType<TException>()
        {
            throw new NotImplementedException();
        }

        public IExceptionMessageTestBuilder WithMessage()
        {
            throw new NotImplementedException();
        }

        public IExceptionTestBuilder WithMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
