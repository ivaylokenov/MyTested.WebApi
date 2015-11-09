// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System.Web.Http;

    public class NoAttributesController : ApiController
    {
        public IHttpActionResult WithParameter(int id)
        {
            return this.Ok(id);
        }

        public void VoidAction()
        {
        }
    }
}
