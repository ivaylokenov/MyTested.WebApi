// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups.Controllers
{
    using System.Web.Http;
    using Services;

    public class NoParameterlessConstructorController : ApiController
    {
        public NoParameterlessConstructorController(IInjectedService service)
        {
            this.Service = service;
        }

        public IInjectedService Service { get; private set; }

        public IHttpActionResult OkAction()
        {
            return this.Ok();
        }
    }
}
