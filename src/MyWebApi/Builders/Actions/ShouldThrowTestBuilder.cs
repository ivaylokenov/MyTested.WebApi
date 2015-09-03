namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Contracts.Actions;
    using Contracts.ExceptionErrors;
    using ExceptionErrors;

    /// <summary>
    /// Used for testing whether action throws exception.
    /// </summary>
    public class ShouldThrowTestBuilder : BaseTestBuilder, IShouldThrowTestBuilder
    {
        private readonly IExceptionTestBuilder exceptionTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShouldThrowTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        public ShouldThrowTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException)
            : base(controller, actionName, caughtException)
        {
            this.exceptionTestBuilder = new ExceptionTestBuilder(this.Controller, this.ActionName, this.CaughtException);
        }

        /// <summary>
        /// Tests whether action throws any exception.
        /// </summary>
        /// <returns>Exception test builder.</returns>
        public IExceptionTestBuilder Exception()
        {
            return this.exceptionTestBuilder;
        }

        /// <summary>
        /// Tests whether action throws any HttpResponseException.
        /// </summary>
        /// <returns>HttpResponseException test builder.</returns>
        public IHttpResponseExceptionTestBuilder HttpResponseException()
        {
            this.exceptionTestBuilder.OfType<HttpResponseException>();
            return new HttpResponseExceptionTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException as HttpResponseException);
        }
    }
}
