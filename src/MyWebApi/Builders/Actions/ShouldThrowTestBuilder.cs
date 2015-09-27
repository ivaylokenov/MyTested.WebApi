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

namespace MyWebApi.Builders.Actions
{
    using System;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.Actions;
    using Contracts.ExceptionErrors;
    using ExceptionErrors;
    using Exceptions;

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
        /// Tests whether action throws any AggregateException.
        /// </summary>
        /// <param name="withNumberOfInnerExceptions">Optional expected number of total inner exceptions.</param>
        /// <returns>AggregateException test builder.</returns>
        public IAggregateExceptionTestBuilder AggregateException(int? withNumberOfInnerExceptions = null)
        {
            this.exceptionTestBuilder.OfType<AggregateException>();
            var aggregateException = this.CaughtException as AggregateException;
            var innerExceptionsCount = aggregateException.InnerExceptions.Count;
            if (withNumberOfInnerExceptions.HasValue &&
                withNumberOfInnerExceptions != innerExceptionsCount)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected AggregateException to contain {2} inner exceptions, but in fact contained {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    withNumberOfInnerExceptions,
                    innerExceptionsCount));
            }

            return new AggregateExceptionTestBuilder(
                this.Controller,
                this.ActionName,
                aggregateException);
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
