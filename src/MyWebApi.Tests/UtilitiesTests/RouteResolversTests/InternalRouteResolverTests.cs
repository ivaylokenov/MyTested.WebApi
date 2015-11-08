// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace My.WebApi.Tests.UtilitiesTests.RouteResolversTests
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Routing;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Utilities.RouteResolvers;

    [TestFixture]
    public class InternalRouteResolverTests
    {
        [Test]
        public void ResolveShouldResolveCorrectControllerAndAction()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/NoParameterlessConstructor/OkAction");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(NoParameterlessConstructorController), routeInfo.Controller);
            Assert.AreEqual("OkAction", routeInfo.Action);
            Assert.AreEqual(0, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectControllerAndActionWithActionNameAttribute()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/ChangedActionName");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("ChangedActionName", routeInfo.Action);
            Assert.AreEqual(0, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithRoutePrefixAndRouteAttribute()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Routes/Test");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("WithRouteAttribute", routeInfo.Action);
            Assert.AreEqual(0, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithParameter()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/WithParameter/5");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("WithParameter", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.AreEqual(5, routeInfo.ActionArguments["id"].Value);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithParameterOfDifferentType()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/WithParameter/Test");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("WithParameter", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.ActionArguments["id"].Value);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsFalse(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithParameterAndQueryString()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/WithParameterAndQueryString/5?value=test");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("WithParameterAndQueryString", routeInfo.Action);
            Assert.AreEqual(5, routeInfo.ActionArguments["id"].Value);
            Assert.AreEqual("test", routeInfo.ActionArguments["value"].Value);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithSpecificMethod()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Route/GetMethod");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("GetMethod", routeInfo.Action);
            Assert.AreEqual(0, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldNotResolveCorrectlyWithWrongMethod()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/GetMethod");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsTrue(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Method Not Allowed'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithFullQueryString()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/QueryString?first=test&second=5");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("QueryString", routeInfo.Action);
            Assert.AreEqual(2, routeInfo.ActionArguments.Count);
            Assert.AreEqual("test", routeInfo.ActionArguments["first"].Value);
            Assert.AreEqual(5, routeInfo.ActionArguments["second"].Value);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldNotResolveCorrectlyWithPartialQueryString()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/QueryString?second=5");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Not Found'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldNotResolveCorrectlyWithMissingQueryString()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/QueryString");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Not Found'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyJsonContentBody()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithModel")
            {
                Content =
                    new StringContent(
                        "{\"Integer\": 1, \"RequiredString\": \"Test\", \"NonRequiredString\": \"AnotherTest\", \"NotValidateInteger\": 2}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.ApplicationJson);

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithModel", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.IsNotNull(routeInfo.ActionArguments["someModel"]);
            Assert.IsAssignableFrom<RequestModel>(routeInfo.ActionArguments["someModel"].Value);
            Assert.AreEqual(1, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).Integer);
            Assert.AreEqual("Test", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).RequiredString);
            Assert.AreEqual("AnotherTest", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NonRequiredString);
            Assert.AreEqual(2, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NotValidateInteger);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyPartialJsonContentBody()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithModel")
            {
                Content =
                    new StringContent(
                        "{\"NonRequiredString\": \"AnotherTest\", \"NotValidateInteger\": 2}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.ApplicationJson);

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithModel", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.IsNotNull(routeInfo.ActionArguments["someModel"].Value);
            Assert.IsAssignableFrom<RequestModel>(routeInfo.ActionArguments["someModel"].Value);
            Assert.AreEqual(0, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).Integer);
            Assert.IsNullOrEmpty(((RequestModel)routeInfo.ActionArguments["someModel"].Value).RequiredString);
            Assert.AreEqual("AnotherTest", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NonRequiredString);
            Assert.AreEqual(2, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NotValidateInteger);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsFalse(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyWithEmptyJsonContentBody()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithModel");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithModel", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.IsNull(routeInfo.ActionArguments["someModel"].Value);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyJsonContentBodyAndRouteParameter()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithParameterAndModel/5")
            {
                Content =
                    new StringContent(
                        "{\"Integer\": 1, \"RequiredString\": \"Test\", \"NonRequiredString\": \"AnotherTest\", \"NotValidateInteger\": 2}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.ApplicationJson);

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithParameterAndModel", routeInfo.Action);
            Assert.AreEqual(2, routeInfo.ActionArguments.Count);
            Assert.AreEqual(5, routeInfo.ActionArguments["id"].Value);
            Assert.IsNotNull(routeInfo.ActionArguments["someModel"].Value);
            Assert.IsAssignableFrom<RequestModel>(routeInfo.ActionArguments["someModel"].Value);
            Assert.AreEqual(1, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).Integer);
            Assert.AreEqual("Test", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).RequiredString);
            Assert.AreEqual("AnotherTest", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NonRequiredString);
            Assert.AreEqual(2, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NotValidateInteger);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldResolveCorrectlyJsonContentBodyAndQueryString()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithQueryStringAndModel?value=test")
            {
                Content =
                    new StringContent(
                        "{\"Integer\": 1, \"RequiredString\": \"Test\", \"NonRequiredString\": \"AnotherTest\", \"NotValidateInteger\": 2}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.ApplicationJson);

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithQueryStringAndModel", routeInfo.Action);
            Assert.AreEqual(2, routeInfo.ActionArguments.Count);
            Assert.AreEqual("test", routeInfo.ActionArguments["value"].Value);
            Assert.IsNotNull(routeInfo.ActionArguments["someModel"].Value);
            Assert.IsAssignableFrom<RequestModel>(routeInfo.ActionArguments["someModel"].Value);
            Assert.AreEqual(1, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).Integer);
            Assert.AreEqual("Test", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).RequiredString);
            Assert.AreEqual("AnotherTest", ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NonRequiredString);
            Assert.AreEqual(2, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NotValidateInteger);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldWorkWithBindingAttributes()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/PostMethodWithModelAndAttribute")
            {
                Content =
                    new StringContent(
                        "{\"NonRequiredString\": \"AnotherTest\", \"NotValidateInteger\": 2}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.ApplicationJson);

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("PostMethodWithModelAndAttribute", routeInfo.Action);
            Assert.AreEqual(1, routeInfo.ActionArguments.Count);
            Assert.IsNotNull(routeInfo.ActionArguments["someModel"]);
            Assert.IsAssignableFrom<RequestModel>(routeInfo.ActionArguments["someModel"].Value);
            Assert.AreEqual(0, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).Integer);
            Assert.IsNullOrEmpty(((RequestModel)routeInfo.ActionArguments["someModel"].Value).RequiredString);
            Assert.IsNullOrEmpty(((RequestModel)routeInfo.ActionArguments["someModel"].Value).NonRequiredString);
            Assert.AreEqual(0, ((RequestModel)routeInfo.ActionArguments["someModel"].Value).NotValidateInteger);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsFalse(routeInfo.ModelState.IsValid);
        }

        [Test]
        public void ResolveShouldReturnProperErrorWhenControllerIsNotFound()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/InvalidController/Action");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Not Found'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldReturnProperErrorWhenActionIsNotFound()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/InvalidAction");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Not Found'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldReturnProperErrorWhenTwoActionsAreMatched()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Route/SameAction");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsFalse(routeInfo.IsResolved);
            Assert.IsFalse(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.AreEqual("it could not be resolved: 'Multiple actions were found that match the request'", routeInfo.UnresolvedError);
            Assert.IsNull(routeInfo.Controller);
            Assert.IsNull(routeInfo.Action);
            Assert.IsNull(routeInfo.ActionArguments);
            Assert.IsNull(routeInfo.HttpMessageHandler);
            Assert.IsNull(routeInfo.ModelState);
        }

        [Test]
        public void ResolveShouldIgnoreRoutesWithStopRountingHandler()
        {
            var config = TestObjectFactory.GetHttpConfigurationWithRoutes();
            var request = new HttpRequestMessage(HttpMethod.Get, "api/IgnoredRoute");

            var routeInfo = InternalRouteResolver.Resolve(config, request);

            Assert.IsTrue(routeInfo.IsResolved);
            Assert.IsTrue(routeInfo.IsIgnored);
            Assert.IsFalse(routeInfo.MethodIsNotAllowed);
            Assert.IsNullOrEmpty(routeInfo.UnresolvedError);
            Assert.AreEqual(typeof(RouteController), routeInfo.Controller);
            Assert.AreEqual("GetMethod", routeInfo.Action);
            Assert.AreEqual(0, routeInfo.ActionArguments.Count);
            Assert.IsAssignableFrom<StopRoutingHandler>(routeInfo.HttpMessageHandler);
            Assert.IsTrue(routeInfo.ModelState.IsValid);
        }
    }
}
