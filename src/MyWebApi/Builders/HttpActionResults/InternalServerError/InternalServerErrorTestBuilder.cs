// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Builders.HttpActionResults.InternalServerError
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Base;
    using Common.Extensions;
    using Contracts.Base;
    using Contracts.ExceptionErrors;
    using Contracts.HttpActionResults.InternalServerError;
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
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">Result from the tested action.</param>
        public InternalServerErrorTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            TInternalServerErrorResult actionResult)
            : base(controller, actionName, caughtException, actionResult)
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
        /// <param name="exception">Expected exception.</param>
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
