namespace MyWebApi.Builders.InternalServerErrors
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.Exceptions;
    using Contracts.InternalServerErrors;
    using Exceptions;

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
        /// 
        /// </summary>
        /// <returns></returns>
        public IExceptionTestBuilder WithException()
        {
            var exceptionResult = this.GetExceptionResult();
            // TODO: return exception test builder
            return null;
        }

        public IBaseTestBuilder WithException(Exception exception)
        {
            var exceptionResult = this.GetExceptionResult();
            // TODO: test excception
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
