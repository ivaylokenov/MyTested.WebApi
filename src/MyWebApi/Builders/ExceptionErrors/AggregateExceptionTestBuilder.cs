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

namespace MyWebApi.Builders.ExceptionErrors
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.ExceptionErrors;
    using Exceptions;
    using Utilities;

    public class AggregateExceptionTestBuilder : ExceptionTestBuilder, IAndAggregateExceptionTestBuilder
    {
        private readonly AggregateException aggregateException;

        public AggregateExceptionTestBuilder(
            ApiController controller,
            string actionName,
            AggregateException caughtException)
            : base(controller, actionName, caughtException)
        {
            this.aggregateException = caughtException;
        }

        public IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
        {
            var expectedInnerExceptionType = typeof(TInnerException);
            var innerExceptionFound = this.aggregateException.InnerExceptions.Any(e => e.GetType() == expectedInnerExceptionType);
            if (!innerExceptionFound)
            {
                throw new InvalidExceptionAssertionException(string.Format(
                    "When calling {0} action in {1} expected AggregateException to contain {2}, but none was found.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedInnerExceptionType.ToFriendlyTypeName()));
            }

            return this;
        }

        public new IAggregateExceptionTestBuilder AndAlso()
        {
            return this;
        }
    }
}
