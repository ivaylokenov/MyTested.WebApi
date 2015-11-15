// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.AttributesTests
{
    using System.Web.Http;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class ControllerAttributesTestBuilderTests
    {
        [Test]
        public void ContainingAttributeOfTypeShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<AuthorizeAttribute>());
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have AllowAnonymousAttribute, but in fact such was not found.")]
        public void ContainingAttributeOfTypeShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ContainingAttributeOfType<AllowAnonymousAttribute>());
        }

        [Test]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttribute()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
        }

        [Test]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCaseDifference()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/Test"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing AttributesController was expected to have RouteAttribute with 'api/another' template, but in fact found 'api/test'.")]
        public void ChangingRouteToShouldThrowExceptionWithControllerWithTheAttributeAndWrongTemplate()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/another"));
        }

        [Test]
        public void ChangingRouteToShouldNotThrowExceptionWithControllerWithTheAttributeAndCorrectName()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "TestRouteAttributes"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing AttributesController was expected to have RouteAttribute with 'AnotherRoute' name, but in fact found 'TestRouteAttributes'.")]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongName()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withName: "AnotherRoute"));
        }

        [Test]
        public void ChangingRouteToShouldNotThrowExceptionWithActionWithTheAttributeAndCorrectOrder()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 1));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing AttributesController was expected to have RouteAttribute with order of 2, but in fact found 1.")]
        public void ChangingRouteToShouldThrowExceptionWithActionWithTheAttributeAndWrongOrder()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test", withOrder: 2));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have RouteAttribute, but in fact such was not found.")]
        public void ChangingRouteToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRouteTo("api/test"));
        }

        [Test]
        public void ChangingRoutePrefixToShouldNotThrowExceptionWithCorrectTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/test"));
        }

        [Test]
        public void ChangingRoutePrefixToShouldNotThrowExceptionWithCorrectTheAttributeAndCaseDifference()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/Test"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have RoutePrefixAttribute with '/api/another' prefix, but in fact found '/api/test'.")]
        public void ChangingRoutePrefixToShouldThrowExceptionWithControllerWithTheAttributeAndWrongPrefix()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/another"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing AttributesController was expected to have RoutePrefixAttribute, but in fact such was not found.")]
        public void ChangingActionNameToShouldThrowExceptionWithActionWithoutTheAttribute()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.ChangingRoutePrefixTo("/api/test"));
        }

        [Test]
        public void AllowingAnonymousRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have AllowAnonymousAttribute, but in fact such was not found.")]
        public void AllowingAnonymousRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.AllowingAnonymousRequests());
        }

        [Test]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttribute()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing AttributesController was expected to have AuthorizeAttribute, but in fact such was not found.")]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttribute()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests());
        }

        [Test]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectRoles()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin,Moderator"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have AuthorizeAttribute with allowed 'Admin' roles, but in fact found 'Admin,Moderator'.")]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectRoles()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedRoles: "Admin"));
        }

        [Test]
        public void RestrictingForAuthorizedRequestsShouldNotThrowExceptionWithTheAttributeWithCorrectUsers()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedUsers: "John,George"));
        }

        [Test]
        [ExpectedException(
            typeof(AttributeAssertionException),
            ExpectedMessage = "When testing WebApiController was expected to have AuthorizeAttribute with allowed 'John' users, but in fact found 'John,George'.")]
        public void RestrictingForAuthorizedRequestsShouldThrowExceptionWithControllerWithoutTheAttributeWithIncorrectUsers()
        {
            MyWebApi
                .Controller<WebApiController>()
                .ShouldHave()
                .Attributes(attributes => attributes.RestrictingForAuthorizedRequests(withAllowedUsers: "John"));
        }

        [Test]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyWebApi
                .Controller<AttributesController>()
                .ShouldHave()
                .Attributes(attributes
                    => attributes
                        .AllowingAnonymousRequests()
                        .AndAlso()
                        .ChangingRouteTo("api/test"));
        }
    }
}
