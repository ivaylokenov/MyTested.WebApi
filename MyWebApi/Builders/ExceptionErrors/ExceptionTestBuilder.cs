namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Utilities;

    public class ExceptionTestBuilder : BaseTestBuilder, IExceptionTestBuilder
    {
        private readonly Exception actualException;

        public ExceptionTestBuilder(ApiController controller, string actionName, Exception exception)
            : base(controller, actionName)
        {
            this.actualException = exception;
        }

        public IExceptionTestBuilder OfType<TException>()
        {
            var expectedExceptionType = typeof (TException);
            var actualExceptionType = this.actualException.GetType();
            if (Reflection.AreDifferentTypes(expectedExceptionType, actualExceptionType))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedExceptionType.GetName(),
                    actualException.GetName()));
            }

            return this;
        }

        public IExceptionMessageTestBuilder WithMessage()
        {
            return new ExceptionMessageTestBuilder(this.Controller, this.ActionName, this, this.actualException.Message);
        }

        public IExceptionTestBuilder WithMessage(string message)
        {
            var actualExceptionMessage = this.actualException.Message;
            if (actualExceptionMessage != message)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected exception with message '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Controller.GetName(),
                    message,
                    actualExceptionMessage));
            }

            return this;
        }

        public IExceptionTestBuilder AndAlso()
        {
            return this;
        }
    }
}
