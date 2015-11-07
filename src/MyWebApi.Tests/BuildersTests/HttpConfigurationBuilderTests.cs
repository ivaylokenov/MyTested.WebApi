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

namespace MyWebApi.Tests.BuildersTests
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
