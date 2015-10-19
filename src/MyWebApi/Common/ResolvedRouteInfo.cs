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

namespace MyWebApi.Common
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Routing;

    public class ResolvedRouteInfo
    {
        public ResolvedRouteInfo(
            string controller,
            string action,
            IDictionary<string, object> actionArguments,
            HttpMessageHandler httpMessageHandler,
            ModelStateDictionary modelState)
        {
            this.IsResolved = true;
            this.Controller = controller;
            this.Action = action;
            this.ActionArguments = actionArguments;
            this.HttpMessageHandler = httpMessageHandler;
            this.ModelState = modelState;
        }

        public ResolvedRouteInfo(string unresolvedError)
        {
            this.IsResolved = false;
            this.UnresolvedError = unresolvedError;
        }

        public bool IsResolved { get; private set; }

        public string UnresolvedError { get; private set; }

        public string Controller { get; private set; }

        public string Action { get; private set; }

        public IDictionary<string, object> ActionArguments { get; private set; }

        public HttpMessageHandler HttpMessageHandler { get; set; }

        public ModelStateDictionary ModelState { get; set; }

        public bool IsIgnored
        {
            get { return this.HttpMessageHandler is StopRoutingHandler; }
        }
    }
}
