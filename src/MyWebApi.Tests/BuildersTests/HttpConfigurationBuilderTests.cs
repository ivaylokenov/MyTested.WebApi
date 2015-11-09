// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests
{
    using Common.Servers;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class HttpConfigurationBuilderTests
    {
        [Test]
        public void AndStartsServerShouldStartServerCorrectly()
        {
            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes()).AndStartsServer();

            Assert.IsNotNull(HttpTestServer.GlobalServer);
            Assert.IsNotNull(HttpTestServer.GlobalClient);
            Assert.IsTrue(HttpTestServer.GlobalIsStarted);

            MyWebApi.Server().Stops();

            Assert.IsNull(HttpTestServer.GlobalServer);
            Assert.IsNull(HttpTestServer.GlobalClient);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }
    }
}
