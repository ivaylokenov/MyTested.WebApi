// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyWebApi.Tests.BuildersTests.ServersTests
{
    using System;
    using Common.Servers;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void StartsAndStopsShouldWorkCorrectlyForHttpServers()
        {
            MyWebApi.Server().Starts(TestObjectFactory.GetHttpConfigurationWithRoutes());

            Assert.IsNotNull(HttpTestServer.GlobalServer);
            Assert.IsNotNull(HttpTestServer.GlobalClient);
            Assert.IsTrue(HttpTestServer.GlobalIsStarted);

            MyWebApi.Server().Stops();

            Assert.IsNull(HttpTestServer.GlobalServer);
            Assert.IsNull(HttpTestServer.GlobalClient);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }

        [Test]
        public void SecondStartsShouldStopTheFirstHttpServer()
        {
            MyWebApi.Server().Starts();

            var server = HttpTestServer.GlobalServer;

            MyWebApi.Server().Starts();

            var secondServer = HttpTestServer.GlobalServer;

            Assert.AreNotSame(server, secondServer);
        }

        [Test]
        public void StartsShouldWorkCorrectlyWithGlobalConfiguration()
        {
            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());

            MyWebApi.Server().Starts();

            Assert.IsNotNull(HttpTestServer.GlobalServer);
            Assert.IsNotNull(HttpTestServer.GlobalClient);
            Assert.IsTrue(HttpTestServer.GlobalIsStarted);

            MyWebApi.Server().Stops();

            Assert.IsNull(HttpTestServer.GlobalServer);
            Assert.IsNull(HttpTestServer.GlobalClient);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }

        [Test]
        [ExpectedException(
            typeof(NullReferenceException),
            ExpectedMessage = "'IsUsing' method should be called before testing server pipeline or HttpConfiguration should be provided. MyWebApi must be configured and HttpConfiguration cannot be null.")]
        public void StartsShouldThrowExceptionWithoutAnyConfiguration()
        {
            MyWebApi.IsUsing(null);

            MyWebApi.Server().Starts();
        }

        [Test]
        public void StartsAndStopsShouldWorkCorrectlyForOwinServers()
        {
            MyWebApi.Server().Starts<CustomStartup>();

            Assert.IsNotNull(OwinTestServer.GlobalServer);
            Assert.IsNotNull(OwinTestServer.GlobalClient);
            Assert.IsTrue(OwinTestServer.GlobalIsStarted);

            Assert.AreEqual("http://localhost:1234", OwinTestServer.GlobalClient.BaseAddress.OriginalString);

            MyWebApi.Server().Stops();

            Assert.IsNull(OwinTestServer.GlobalServer);
            Assert.IsNull(OwinTestServer.GlobalClient);
            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
        }

        [Test]
        public void SecondStartsShouldStopTheFirstOwinServer()
        {
            MyWebApi.Server().Starts<CustomStartup>();

            var server = OwinTestServer.GlobalServer;

            MyWebApi.Server().Starts<CustomStartup>();

            var secondServer = OwinTestServer.GlobalServer;

            Assert.AreNotSame(server, secondServer);
        }

        [Test]
        public void StartWithCustomPortShouldWorkCorrectly()
        {
            MyWebApi.Server().Starts<CustomStartup>(port: 9876);

            Assert.AreEqual("http://localhost:9876", OwinTestServer.GlobalClient.BaseAddress.OriginalString);

            MyWebApi.Server().Stops();
        }

        [Test]
        public void StartWithCustomHostShouldWorkCorrectly()
        {
            MyWebApi.Server().Starts<CustomStartup>(host: "https://localhost");

            Assert.AreEqual("https://localhost:1234", OwinTestServer.GlobalClient.BaseAddress.OriginalString);

            MyWebApi.Server().Stops();
        }

        [Test]
        public void HttpAndOwinServersShouldBeStartedAtTheSameTimeAndStopsShouldStopBothOfThem()
        {
            MyWebApi.Server().Starts<CustomStartup>();
            MyWebApi.Server().Starts();

            Assert.IsNotNull(HttpTestServer.GlobalServer);
            Assert.IsNotNull(HttpTestServer.GlobalClient);
            Assert.IsTrue(HttpTestServer.GlobalIsStarted);
            Assert.IsNotNull(OwinTestServer.GlobalServer);
            Assert.IsNotNull(OwinTestServer.GlobalClient);
            Assert.IsTrue(OwinTestServer.GlobalIsStarted);

            MyWebApi.Server().Stops();

            Assert.IsNull(HttpTestServer.GlobalServer);
            Assert.IsNull(HttpTestServer.GlobalClient);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
            Assert.IsNull(OwinTestServer.GlobalServer);
            Assert.IsNull(OwinTestServer.GlobalClient);
            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
        }

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "There are no running test servers to stop. Calling MyWebApi.Server().Stops() should be done only after MyWebApi.Server.Starts() is invoked.")]
        public void StopsShouldThrowExceptionIfNoServersAreRunning()
        {
            MyWebApi.Server().Stops();
        }

        [TestFixtureTearDown]
        public void RestoreConfiguration()
        {
            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }
    }
}
