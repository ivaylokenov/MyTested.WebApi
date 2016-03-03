// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests
{
    using System.Web.Http;
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

        [Test]
        public void DefaultErrorDetailPolicyShouldBeAlways()
        {
            MyWebApi.IsUsingDefaultHttpConfiguration();

            Assert.AreEqual(IncludeErrorDetailPolicy.Always, MyWebApi.Configuration.IncludeErrorDetailPolicy);

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }

        [Test]
        public void WithBaseAddressShouldChangedDefaultAddress()
        {
            Assert.IsFalse(RemoteServer.GlobalIsConfigured);
            Assert.AreEqual(MyWebApi.DefaultHost, MyWebApi.BaseAddress.OriginalString);

            string address = "http://mytestedasp.net";

            MyWebApi
                .IsUsingDefaultHttpConfiguration()
                .WithBaseAddress(address);

            Assert.AreEqual(address, MyWebApi.BaseAddress.OriginalString);
            Assert.IsTrue(RemoteServer.GlobalIsConfigured);

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());

            Assert.AreEqual(MyWebApi.DefaultHost, MyWebApi.BaseAddress.OriginalString);

            RemoteServer.DisposeGlobal();
        }

        [Test]
        public void WithErrorDetailPolicyShouldSetCorrectErrorDetailPolicy()
        {
            MyWebApi.IsUsingDefaultHttpConfiguration().WithErrorDetailPolicy(IncludeErrorDetailPolicy.LocalOnly);

            Assert.AreEqual(IncludeErrorDetailPolicy.LocalOnly, MyWebApi.Configuration.IncludeErrorDetailPolicy);

            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }
    }
}
