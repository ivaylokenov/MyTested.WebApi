namespace MyWebApi.Builders.ExceptionErrors
{
    using System.Web.Http;
    using Base;
    using Contracts.ExceptionErrors;

    public class ExceptionMessageTestBuilder
        : BaseTestBuilder, IExceptionMessageTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;
        private string actualMessage;

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
            throw new System.NotImplementedException();
        }

        public IExceptionTestBuilder BeginningWith(string beginMessage)
        {
            throw new System.NotImplementedException();
        }

        public IExceptionTestBuilder EndingWith(string endMessage)
        {
            throw new System.NotImplementedException();
        }

        public IExceptionTestBuilder Containing(string containsMessage)
        {
            throw new System.NotImplementedException();
        }

        public IExceptionTestBuilder AndAlso()
        {
            return this.exceptionTestBuilder;
        }
    }
}
