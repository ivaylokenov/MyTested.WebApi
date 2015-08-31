﻿namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Utilities;

    /// <summary>
    /// Used for testing expected exceptions.
    /// </summary>
    public class ExceptionTestBuilder : BaseTestBuilder, IExceptionTestBuilder
    {
        private readonly Exception actualException;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="exception">Actual received exception.</param>
        public ExceptionTestBuilder(ApiController controller, string actionName, Exception exception)
            : base(controller, actionName)
        {
            this.actualException = exception;
        }

        /// <summary>
        /// Tests whether certain type of exception is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TException">Type of the expected exception.</typeparam>
        /// <returns>The same exception test builder.</returns>
        public IExceptionTestBuilder OfType<TException>()
        {
            var expectedExceptionType = typeof(TException);
            var actualExceptionType = this.actualException.GetType();
            if (Reflection.AreDifferentTypes(expectedExceptionType, actualExceptionType))
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2}, but instead received {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedExceptionType.GetName(),
                    this.actualException.GetName()));
            }

            return this;
        }

        /// <summary>
        /// Tests exception message using test builder.
        /// </summary>
        /// <returns>Exception message test builder.</returns>
        public IExceptionMessageTestBuilder WithMessage()
        {
            return new ExceptionMessageTestBuilder(this.Controller, this.ActionName, this, this.actualException.Message);
        }

        /// <summary>
        /// Tests exception message whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected exception message as string.</param>
        /// <returns>The same exception test builder.</returns>
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

        /// <summary>
        /// AndAlso method for better readability when chaining expected exception tests.
        /// </summary>
        /// <returns>The same exception test builder.</returns>
        public IExceptionTestBuilder AndAlso()
        {
            return this;
        }
    }
}