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

namespace MyWebApi.Tests.Setups.Controllers
{
    using System.Web.Http;
    using Models;

    [RoutePrefix("api/routes")]
    public class RouteController : ApiController
    {
        public void VoidAction()
        {
        }

        public IHttpActionResult WithParameter(int id)
        {
            return this.Ok();
        }

        public IHttpActionResult WithParameterAndQueryString(int id, string value)
        {
            return this.Ok();
        }

        [HttpGet]
        public IHttpActionResult GetMethod()
        {
            return this.Ok();            
        }

        public IHttpActionResult QueryString(string first, int second)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithModel(RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithParameterAndModel(int id, RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithQueryStringAndModel(string value, RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithModelAndAttribute([FromUri]RequestModel someModel)
        {
            return this.Ok();
        }

        [Route("test")]
        public IHttpActionResult WithRouteAttribute()
        {
            return this.Ok();
        }

        [ActionName("ChangedActionName")]
        public IHttpActionResult WithActionNameAttribute()
        {
            return this.Ok();
        }

        [HttpGet]
        public IHttpActionResult HeaderRoute()
        {
            return this.Ok();
        }

        public IHttpActionResult SameAction(RequestModel model)
        {
            return this.Ok();
        }

        public IHttpActionResult SameAction(ResponseModel model)
        {
            return this.Ok();
        }
    }
}
