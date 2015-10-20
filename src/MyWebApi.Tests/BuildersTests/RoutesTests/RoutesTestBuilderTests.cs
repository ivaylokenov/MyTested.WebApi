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

namespace MyWebApi.Tests.BuildersTests.RoutesTests
{
    using System.Net.Http;
    using NUnit.Framework;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class RoutesTestBuilderTests
    {
        [Test]
        public void ToShouldResolveCorrectControllerAndAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/NoParameterlessConstructor/OkAction")
                .WithHttpMethod(HttpMethod.Post)
                .To<NoParameterlessConstructorController>(c => c.OkAction());
        }

        [Test]
        public void ToShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/ChangedActionName")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.WithActionNameAttribute());
        }

        [Test]
        public void ToShouldResolveCorrectlyWithRoutePrefixAndRouteAttribute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Routes/Test")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.WithRouteAttribute());
        }

        [Test]
        public void ToShouldResolveCorrectlyWithParameter()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/WithParameter/5")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.WithParameter(5));
        }

        [Test]
        public void ToShouldResolveCorrectlyWithParameterAndQueryString()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/WithParameterAndQueryString/5?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.WithParameterAndQueryString(5, "test"));
        }

        [Test]
        public void ToShouldResolveCorrectlyWithDefaultGetMethod()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .To<RouteController>(c => c.GetMethod());
        }

        [Test]
        public void ToShouldResolveCorrectlyWithFullQueryString()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/QueryString?first=test&second=5")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.QueryString("test", 5));
        }

        [Test]
        public void ToShouldResolveCorrectyWithJsonContent()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithModel")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .To<RouteController>(c => c.PostMethodWithModel(new RequestModel
                {
                    Integer = 1,
                    RequiredString = "Test",
                    NonRequiredString = "AnotherTest",
                    NotValidateInteger = 2
                }));
        }

        [Test]
        public void ToShouldResolveCorrectyWithJsonContentAndParameter()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithParameterAndModel/5")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .To<RouteController>(c => c.PostMethodWithParameterAndModel(5, new RequestModel
                {
                    Integer = 1,
                    RequiredString = "Test",
                    NonRequiredString = "AnotherTest",
                    NotValidateInteger = 2
                }));
        }

        [Test]
        public void ToShouldResolveCorrectyWithJsonContentAndQueryString()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .To<RouteController>(c => c.PostMethodWithQueryStringAndModel("test", new RequestModel
                {
                    Integer = 1,
                    RequiredString = "Test",
                    NonRequiredString = "AnotherTest",
                    NotValidateInteger = 2
                }));
        }
    }
}
