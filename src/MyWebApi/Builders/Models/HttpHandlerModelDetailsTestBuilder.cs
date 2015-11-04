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

namespace MyWebApi.Builders.Models
{
    using System;
    using System.Net.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Exceptions;
    using Utilities;

    public class HttpHandlerModelDetailsTestBuilder<TResponseModel>
        : BaseHandlerTestBuilder, IHttpHandlerModelDetailsTestBuilder<TResponseModel>
    {
        private readonly IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder;
        private readonly TResponseModel model;

        public HttpHandlerModelDetailsTestBuilder(
            HttpMessageHandler handler,
            IHttpHandlerResponseMessageTestBuilder httpHandlerResponseMessageTestBuilder,
            TResponseModel model = default(TResponseModel))
            : base(handler)
        {
            this.httpHandlerResponseMessageTestBuilder = httpHandlerResponseMessageTestBuilder;
            this.model = model;
        }

        public IHttpHandlerResponseMessageTestBuilder Passing(Action<TResponseModel> assertions)
        {
            assertions(this.model);
            return this.httpHandlerResponseMessageTestBuilder;
        }

        public IHttpHandlerResponseMessageTestBuilder Passing(Func<TResponseModel, bool> predicate)
        {
            if (!predicate(this.model))
            {
                throw new ResponseModelAssertionException(string.Format(
                            "When testing {0} expected HTTP response message content model {1} to pass the given condition, but it failed.",
                            this.Handler.GetName(),
                            typeof(TResponseModel).ToFriendlyTypeName()));
            }

            return this.httpHandlerResponseMessageTestBuilder;
        }

        public TResponseModel AndProvideTheModel()
        {
            return this.model;
        }
    }
}
