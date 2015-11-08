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

namespace MyWebApi.Tests.BuildersTests.ServersTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Common.Servers;
    using NUnit.Framework;
    using Setups;

    [TestFixture]
    public class ServerTestBuilderTests
    {
        [Test]
        public void OwinTestsShouldWorkCorrectlyWithGlobalTestServer()
        {
            MyWebApi.Server().Starts<CustomStartup>();

            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Found);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req.WithHeader("CustomHeader", "CustomValue"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(new HttpRequestMessage())
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);

            MyWebApi.Server().Stops();
        }

        [Test]
        public void HttpTestsShouldWorkCorrectlyWithGlobalTestServer()
        {
            MyWebApi.Server().Starts();

            var request = new HttpRequestMessage(HttpMethod.Post, "/test");

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(request)
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req.WithMethod(HttpMethod.Post).WithRequestUri("api/NoAttributes/WithParameter/5"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithResponseModelOfType<int>()
                .Passing(m => m == 5);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(new HttpRequestMessage(HttpMethod.Post,  "/Invalid"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);

            MyWebApi.Server().Stops();
        }

        [Test]
        public void HttpServerTestShouldWorkCorrectlyWhenNoGlobalServerIsRunning()
        {
            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(
                    req => req.WithMethod(HttpMethod.Post).WithRequestUri("api/NoAttributes/WithParameter/5"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithResponseModelOfType<int>();

            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }

        [Test]
        public void WithCustomConfigurationShouldStartHttpServerCorrectly()
        {
            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);

            MyWebApi
                .Server()
                .Working(new HttpConfiguration())
                .WithHttpRequestMessage(
                    req => req.WithMethod(HttpMethod.Post).WithRequestUri("api/NoAttributes/WithParameter/5"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);

            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }

        [Test]
        public void WithIndividualOwinHostShouldWorkCorrectly()
        {
            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);

            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(req => req.WithHeader("CustomHeader", "CustomValue"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);

            // Running second time to make sure previous server is disposed
            MyWebApi
                .Server()
                .Working<CustomStartup>()
                .WithHttpRequestMessage(req => req.WithHeader("CustomHeader", "CustomValue"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);

            Assert.IsFalse(OwinTestServer.GlobalIsStarted);
            Assert.IsFalse(HttpTestServer.GlobalIsStarted);
        }

        [Test]
        [ExpectedException(
            typeof(InvalidOperationException),
            ExpectedMessage = "No test servers are started or could be started for this particular test case. Either call MyWebApi.Server.Starts() to start a new test server or provide global or test specific HttpConfiguration.")]
        public void WithoutAnyConfigurationServersShouldThrowException()
        {
            MyWebApi.IsUsing(null);

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req.WithMethod(HttpMethod.Post).WithRequestUri("api/NoAttributes/WithParameter/5"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithResponseModelOfType<int>();
        }

        [TestFixtureTearDown]
        public void RestoreConfiguration()
        {
            MyWebApi.IsUsing(TestObjectFactory.GetHttpConfigurationWithRoutes());
        }
    }
}
