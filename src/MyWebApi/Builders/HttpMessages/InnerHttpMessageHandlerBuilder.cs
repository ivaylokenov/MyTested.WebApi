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

namespace MyWebApi.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using Base;
    using Contracts.Handlers;

    public class InnerHttpMessageHandlerBuilder : BaseHandlerTestBuilder,
        IInnerHttpMessageHandlerBuilder
    {
        public InnerHttpMessageHandlerBuilder(HttpMessageHandler handler)
            : base(handler)
        {
        }

        public void WithInnerHandler<TInnerHandler>()
            where TInnerHandler : HttpMessageHandler, new()
        {
            this.WithInnerHandler(new TInnerHandler());
        }

        public void WithInnerHandler<TInnerHandler>(TInnerHandler innerHandler)
            where TInnerHandler : HttpMessageHandler
        {
            this.SetInnerHandler(innerHandler);
        }

        public void WithInnerHandler<TInnerHandler>(Func<TInnerHandler> construction)
            where TInnerHandler : HttpMessageHandler
        {
            var innerHandlerInstance = construction();
            this.WithInnerHandler(innerHandlerInstance);
        }

        public void WithInnerHandler<TInnerHandler>(Action<IInnerHttpMessageHandlerBuilder> httpMessageHandlerBuilder)
            where TInnerHandler : HttpMessageHandler, new()
        {
            var newHttpMessageHandlerBuilder = new InnerHttpMessageHandlerBuilder(new TInnerHandler());
            httpMessageHandlerBuilder(newHttpMessageHandlerBuilder);
            this.WithInnerHandler(newHttpMessageHandlerBuilder.Handler);
        }
    }
}
