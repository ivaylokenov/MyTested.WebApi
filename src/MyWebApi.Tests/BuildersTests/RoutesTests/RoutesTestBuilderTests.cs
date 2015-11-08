// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.BuildersTests.RoutesTests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Routing;
    using Exceptions;
    using NUnit.Framework;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class RoutesTestBuilderTests
    {
        private const string CustomHeader = "CustomHeader";
        private const string CustomHeaderValue = "CustomHeaderValue";

        [Test]
        public void WithCustomConfigurationShouldWorkCorrectly()
        {
            MyWebApi
                .Routes(TestObjectFactory.GetHttpConfigurationWithRoutes())
                .ShouldMap("api/NoParameterlessConstructor/OkAction")
                .WithHttpMethod(HttpMethod.Post)
                .To<NoParameterlessConstructorController>(c => c.OkAction());
        }

        [Test]
        public void WithHttpRequestMessageShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap(new HttpRequestMessage(HttpMethod.Post, "api/NoParameterlessConstructor/OkAction"))
                .WithHttpMethod(HttpMethod.Post)
                .To<NoParameterlessConstructorController>(c => c.OkAction());
        }

        [Test]
        public void WithHttpRequestMessageBuilderShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap(request => request
                    .WithRequestUri("api/NoParameterlessConstructor/OkAction")
                    .WithMethod(HttpMethod.Post))
                .To<NoParameterlessConstructorController>(c => c.OkAction());
        }

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

        public void ToShouldResolveCorrectControllerAndActionWithUriLocation()
        {
            MyWebApi
                .Routes()
                .ShouldMap(new Uri("api/NoParameterlessConstructor/OkAction", UriKind.RelativeOrAbsolute))
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
                .To<RouteController>(c => c.PostMethodWithParameterAndModel(
                    5, 
                    new RequestModel
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
                .To<RouteController>(c => c.PostMethodWithQueryStringAndModel(
                    "test", 
                    new RequestModel
                    {
                        Integer = 1,
                        RequiredString = "Test",
                        NonRequiredString = "AnotherTest",
                        NotValidateInteger = 2
                    }));
        }

        [Test]
        public void ToShouldResolveCorrectyWithFormUrlEncodedContentAndQueryString()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithFormUrlEncodedContent("Integer=1&RequiredString=Test&NonRequiredString=AnotherTest&NotValidateInteger=2")
                .To<RouteController>(c => c.PostMethodWithQueryStringAndModel(
                    "test", 
                    new RequestModel
                    {
                        Integer = 1,
                        RequiredString = "Test",
                        NonRequiredString = "AnotherTest",
                        NotValidateInteger = 2
                    }));
        }

        [Test]
        public void ToShouldResolveCorrectlyWithVoidAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/VoidAction")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to match GetMethod action in RouteController but it could not be resolved: 'Method Not Allowed'.")]
        public void ToShouldThrowExceptionWithNotResolvedRoute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .WithHttpMethod(HttpMethod.Post)
                .To<RouteController>(c => c.GetMethod());
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to match GetMethod action in RouteController but it was ignored with StopRoutingHandler.")]
        public void ToShouldThrowExceptionWithIgnoredRoute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .To<RouteController>(c => c.GetMethod());
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to match OkResultAction action in WebApiController but instead matched RouteController.")]
        public void ToShouldThrowExceptionWithWrongController()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .To<WebApiController>(c => c.OkResultAction());
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to match VoidAction action in RouteController but instead matched GetMethod action.")]
        public void ToShouldThrowExceptionWithWrongAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .To<RouteController>(c => c.VoidAction());
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel?value=test' to match PostMethodWithQueryStringAndModel action in RouteController but the 'someModel' parameter was different.")]
        public void ToShouldThrowExceptionWithWrongActionArguments()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .And()
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .To<RouteController>(c => c.PostMethodWithQueryStringAndModel(
                    "test",
                    new RequestModel
                    {
                        Integer = 1,
                        RequiredString = "Test2",
                        NonRequiredString = "AnotherTest",
                        NotValidateInteger = 2
                    }));
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithModel' to match PostMethodWithModel action in RouteController but it could not be resolved: 'Unsupported Media Type'.")]
        public void ToShouldThrowExceptionWithUnsupportedMediaType()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithModel")
                .WithHttpMethod(HttpMethod.Post)
                .WithContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}", MediaType.TextPlain)
                .To<RouteController>(c => c.PostMethodWithModel(new RequestModel
                {
                    Integer = 1,
                    RequiredString = "Test",
                    NonRequiredString = "AnotherTest",
                    NotValidateInteger = 2
                }));
        }

        [Test]
        public void ToNotAllowedMethodShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .WithHttpMethod("POST")
                .ToNotAllowedMethod();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to not allow method 'GET' but in fact it was allowed.")]
        public void ToNotAllowedMethodShouldThrowExceptionWithWrongMethod()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .WithHttpMethod("GET")
                .ToNotAllowedMethod();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/SameAction' to not allow method 'POST' but it could not be resolved: 'Multiple actions were found that match the request'.")]
        public void ToNotAllowedMethodShouldThrowExceptionWithUnresolvedRoute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/SameAction")
                .WithHttpMethod("POST")
                .ToNotAllowedMethod();
        }

        [Test]
        public void ToNonExistingRouteShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("nonexisting")
                .ToNonExistingRoute();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to be non-existing but in fact it was resolved successfully.")]
        public void ToNonExistingRouteShouldThrowExceptionWhenRouteIsResolved()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .ToNonExistingRoute();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to be non-existing but in fact it was ignored with StopRoutingHandler.")]
        public void ToNonExistingRouteShouldThrowExceptionWhenRouteIsIgnored()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToNonExistingRoute();
        }

        [Test]
        public void ToIgnoredRouteShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToIgnoredRoute();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to be ignored with StopRoutingHandler but in fact it was resolved successfully.")]
        public void ToIgnoredRouteShouldThrowExceptionWhenRouteIsResolved()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .ToIgnoredRoute();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/SameAction' to be ignored with StopRoutingHandler but in fact it could not be resolved: 'Method Not Allowed'.")]
        public void ToIgnoredRouteShouldThrowExceptionWhenRouteIsNotResolved()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/SameAction")
                .ToIgnoredRoute();
        }

        [Test]
        public void ToHandlerOfTypeShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToHandlerOfType<StopRoutingHandler>();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/GetMethod' to be handled by StopRoutingHandler but in fact found no handler at all.")]
        public void ToHandlerOfTypeShouldThrowExceptionIfHandlerIsNull()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .ToHandlerOfType<StopRoutingHandler>();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to be handled by CustomHttpMessageHandler but in fact found StopRoutingHandler.")]
        public void ToHandlerOfTypeShouldThrowExceptionIfHandlerIsAnotherType()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToHandlerOfType<CustomHttpMessageHandler>();
        }

        [Test]
        public void ToNoHandlerOfTypeShouldWorkCorrectlyWithOtherHandler()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToNoHandlerOfType<CustomHttpMessageHandler>();
        }

        [Test]
        public void ToNoHandlerOfTypeShouldWorkCorrectlyIfHandlerIsNull()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .ToNoHandlerOfType<StopRoutingHandler>();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to not be handled by StopRoutingHandler but in fact found the same type of handler.")]
        public void ToNoHandlerOfTypeShouldThrowExceptionIfHandlerIsAnotherType()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToNoHandlerOfType<StopRoutingHandler>();
        }

        [Test]
        public void ToNoHandlerShouldWorkCorrectlyIfHandlerIsNull()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .ToNoHandler();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to have no handler of any type but in fact found StopRoutingHandler.")]
        public void ToNoHandlerShouldThrowExceptionIfHandlerIsAnotherType()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .ToNoHandler();
        }

        [Test]
        public void ToValidModelStateShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToValidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel' to have valid model state with no errors but it could not be resolved: 'Not Found'.")]
        public void ToValidModelStateShouldThrowExceptionWhenRouteIsNotResolved()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToValidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to have valid model state with no errors but it was ignored with StopRoutingHandler.")]
        public void ToValidModelStateShouldThrowExceptionWhenRouteIsIgnored()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToValidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel?value=test' to have valid model state with no errors but it had some.")]
        public void ToValidModelStateShouldThrowExceptionWhenModelStateIsNotValid()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToValidModelState();
        }

        [Test]
        public void ToInvalidModelStateShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState();
        }

        [Test]
        public void ToInvalidModelStateShouldWorkCorrectlyWithNumberOfErrors()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState(withNumberOfErrors: 1);
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel?value=test' to have invalid model state with 2 errors but in fact contained 1.")]
        public void ToInvalidModelStateShouldShouldThrowExceptionWithIncorrectNumberOfErrors()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState(withNumberOfErrors: 2);
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel' to have invalid model state but it could not be resolved: 'Not Found'.")]
        public void ToInvalidModelStateShouldThrowExceptionWhenRouteIsNotResolved()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/IgnoredRoute' to have invalid model state but it was ignored with StopRoutingHandler.")]
        public void ToInvalidModelStateShouldThrowExceptionWhenRouteIsIgnored()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/IgnoredRoute")
                .WithJsonContent(
                    @"{""Integer"": 1, ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState();
        }

        [Test]
        [ExpectedException(
            typeof(RouteAssertionException),
            ExpectedMessage = "Expected route 'api/Route/PostMethodWithQueryStringAndModel?value=test' to have invalid model state but was in fact valid.")]
        public void ToInvalidModelStateShouldThrowExceptionWhenModelStateIsValidValid()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/PostMethodWithQueryStringAndModel?value=test")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(
                    @"{""Integer"": 1, ""RequiredString"": ""Test"", ""NonRequiredString"": ""AnotherTest"", ""NotValidateInteger"": 2}")
                .ToInvalidModelState();
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/Route/GetMethod")
                .To<RouteController>(c => c.GetMethod())
                .AndAlso()
                .ToNoHandler();
        }

        [Test]
        public void WithRequestHeaderShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithRequestHeader(CustomHeader, CustomHeaderValue)
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void WithRequestHeaderMultipleValuesShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithRequestHeader(CustomHeader, new[] { CustomHeaderValue })
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void WithRequestHeadersDictionaryShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithRequestHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { CustomHeader, new List<string> { CustomHeaderValue } }
                })
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void WithRequestContentHeaderShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithContent(string.Empty, new MediaTypeHeaderValue(MediaType.TextPlain))
                .WithContentHeader(CustomHeader, CustomHeaderValue)
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void WithRequestContentHeaderMultipleValuesShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithContent(string.Empty, MediaType.TextPlain)
                .WithContentHeader(CustomHeader, new[] { CustomHeaderValue })
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void WithRequestContentHeadersDictionaryShouldWorkCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .WithContent(string.Empty, MediaType.TextPlain)
                .WithContentHeaders(new Dictionary<string, IEnumerable<string>>
                {
                    { CustomHeader, new List<string> { CustomHeaderValue } }
                })
                .To<RouteController>(c => c.HeaderRoute());
        }

        [Test]
        public void RouteShouldNotMatchWithoutHeaders()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/HeaderRoute")
                .ToNonExistingRoute();
        }
    }
}
