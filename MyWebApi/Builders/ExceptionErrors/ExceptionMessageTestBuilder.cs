namespace MyWebApi.Builders.ExceptionErrors
{
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.ExceptionErrors;
    using Exceptions;

    public class ExceptionMessageTestBuilder
        : BaseTestBuilder, IExceptionMessageTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;
        private readonly string actualMessage;

        public ExceptionMessageTestBuilder(
            ApiController controller,
            string actionName,
            IExceptionTestBuilder exceptionTestBuilder,
            string actualMessage)
            : base(controller, actionName)
        {
            this.exceptionTestBuilder = exceptionTestBuilder;
            this.actualMessage = actualMessage;
        }

        public IExceptionTestBuilder ThatEquals(string errorMessage)
        {
            if (this.actualMessage != errorMessage)
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "When calling {0} action in {1} expected exception message to be '{2}', but instead found '{3}'.",
                    errorMessage);
            }

            return this.exceptionTestBuilder;
        }

        public IExceptionTestBuilder BeginningWith(string beginMessage)
        {
            if (!this.actualMessage.StartsWith(beginMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "When calling {0} action in {1} expected exception message to begin with '{2}', but instead found '{3}'.",
                    beginMessage);
            }

            return this.exceptionTestBuilder;
        }

        public IExceptionTestBuilder EndingWith(string endMessage)
        {
            if (!this.actualMessage.EndsWith(endMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "When calling {0} action in {1} expected exception message to end with '{2}', but instead found '{3}'.",
                    endMessage);
            }

            return this.exceptionTestBuilder;
        }

        public IExceptionTestBuilder Containing(string containsMessage)
        {
            if (!this.actualMessage.Contains(containsMessage))
            {
                this.ThrowNewInvalidExceptionAssertionException(
                    "When calling {0} action in {1} expected exception message to contain '{2}', but instead found '{3}'.",
                    containsMessage);
            }

            return this.exceptionTestBuilder;
        }

        public IExceptionTestBuilder AndAlso()
        {
            return this.exceptionTestBuilder;
        }

        private void ThrowNewInvalidExceptionAssertionException(string messageFormat, string operation)
        {
            throw new InvalidExceptionAssertionException(string.Format(
                messageFormat,
                this.ActionName,
                this.Controller.GetName(),
                operation,
                this.actualMessage));
        }
    }
}
