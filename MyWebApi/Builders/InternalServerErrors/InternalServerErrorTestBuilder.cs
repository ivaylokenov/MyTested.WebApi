namespace MyWebApi.Builders.InternalServerErrors
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ExceptionErrors;
    using Contracts.InternalServerErrors;
    using ExceptionErrors;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing internal server error results.
    /// </summary>
    /// <typeparam name="TInternalServerErrorResult">Type of internal server error result - InternalServerErrorResult or ExceptionResult.</typeparam>
    public class InternalServerErrorTestBuilder<TInternalServerErrorResult>
        : BaseTestBuilderWithActionResult<TInternalServerErrorResult>, IInternalServerErrorTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalServerErrorTestBuilder{TInternalServerErrorResult}" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public InternalServerErrorTestBuilder(
            ApiController controller,
            string actionName,
            TInternalServerErrorResult actionResult)
            : base(controller, actionName, actionResult)
        {
        }

        /// <summary>
        /// Tests internal server error whether it contains exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        public IExceptionTestBuilder WithException()
        {
            var exceptionResult = this.GetExceptionResult();
            return new ExceptionTestBuilder(this.Controller, this.ActionName, exceptionResult.Exception);
        }

        /// <summary>
        /// Tests internal server error whether it contains exception with the same type and having the same message as the provided exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        public IBaseTestBuilder WithException(Exception exception)
        {
            var exceptionResult = this.GetExceptionResult();
            var actualException = exceptionResult.Exception;

            if (Reflection.AreDifferentTypes(actualException, exception))
            {
                throw new InternalServerErrorResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected internal server error result to contain {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    exception.GetName(),
                    actualException.GetName()));
            }

            var expectedExceptionMessage = exception.Message;
            var actualExceptionMessage = actualException.Message;
            if (expectedExceptionMessage != actualExceptionMessage)
            {
                throw new InternalServerErrorResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected internal server error result to contain exception with message '{2}', but instead received '{3}'.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedExceptionMessage,
                    actualExceptionMessage));
            }

            return this.NewAndProvideTestBuilder();
        }

        private ExceptionResult GetExceptionResult()
        {
            var actualInternalServerErrorResult = this.ActionResult as ExceptionResult;
            if (actualInternalServerErrorResult == null)
            {
                throw new InternalServerErrorResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected internal server error result to contain exception, but it could not be found.",
                    this.ActionName,
                    this.Controller.GetName()));
            }

            return actualInternalServerErrorResult;
        }
    }
}
