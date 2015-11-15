// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).

namespace MyTested.WebApi.Tests
{
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class WithTests
    {
        [Test]
        public void WithAnyShouldWorkCorrectlyWhereTheValueOfActionParametersAreNotImportant()
        {
            MyWebApi
                .Controller<AttributesController>()
                .Calling(c => c.WithAttributesAndParameters(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs.AllowingAnonymousRequests());
        }
    }
}
