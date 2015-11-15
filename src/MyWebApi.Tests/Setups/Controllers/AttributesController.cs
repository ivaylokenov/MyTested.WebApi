// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System.Web.Http;

    [AllowAnonymous]
    [Route("api/test", Name = "TestRouteAttributes", Order = 1)]
    public class AttributesController : ApiController
    {
        [AllowAnonymous]
        public IHttpActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok(id);
        }
    }
}
