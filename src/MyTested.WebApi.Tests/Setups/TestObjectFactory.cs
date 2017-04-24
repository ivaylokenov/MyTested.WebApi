// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.Setups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Runtime.Serialization.Formatters;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Routing;
    using Common;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Routes;

    public static class TestObjectFactory
    {
        public const string MediaType = "application/json";

        public static HttpConfiguration GetHttpConfigurationWithRoutes()
        {
            var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };

            var constraintResolver = GetCustomInlineConstraintResolver();
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "HeaderRoute",
                routeTemplate: "api/HeaderRoute",
                defaults: new { controller = "Route", action = "HeaderRoute" },
                constraints: new { controller = new HeaderRouteConstraint("CustomHeader", "CustomHeaderValue") });

            config.Routes.MapHttpRoute(
                name: "TestRoute",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Ignored",
                routeTemplate: "api/IgnoredRoute",
                defaults: new { controller = "Route", action = "GetMethod" },
                constraints: null,
                handler: new StopRoutingHandler());

            config.Routes.MapHttpRoute(
                name: "Redirect",
                routeTemplate: "api/Redirect/{action}",
                defaults: new { controller = "NoAttributes" });

            return config;
        }

        public static IPrincipal GetClaimsPrincipal()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "CustomUser") }));
        }

        public static IEnumerable<MediaTypeFormatter> GetFormatters()
        {
            return new List<MediaTypeFormatter>
            {
                new BsonMediaTypeFormatter(),
                new FormUrlEncodedMediaTypeFormatter(),
                new JQueryMvcFormUrlEncodedFormatter(),
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            };
        }

        public static HttpRequestMessage GetCustomHttpRequestMessage()
        {
            return new HttpRequestMessage();
        }

        public static IContentNegotiator GetCustomContentNegotiator()
        {
            return new CustomContentNegotiator();
        }

        public static IInlineConstraintResolver GetCustomInlineConstraintResolver()
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("custom", typeof(CustomConstraint));
            return constraintResolver;
        }

        public static MediaTypeFormatter GetCustomMediaTypeFormatter()
        {
            return new CustomMediaTypeFormatter();
        }

        public static Uri GetUri()
        {
            return new Uri("http://somehost.com/someuri/1?query=Test");
        }

        public static Action<string, string> GetFailingValidationActionWithTwoParameteres()
        {
            return (x, y) => { throw new NullReferenceException(string.Format("{0} {1}", x, y)); };
        }

        public static Action<string, string, string> GetFailingValidationAction()
        {
            return (x, y, z) => { throw new NullReferenceException(string.Format("{0} {1} {2}", x, y, z)); };
        }

        public static RequestModel GetNullRequestModel()
        {
            return null;
        }

        public static RequestModel GetValidRequestModel()
        {
            return new RequestModel
            {
                Integer = 2,
                RequiredString = "Test"
            };
        }

        public static RequestModel GetRequestModelWithErrors()
        {
            return new RequestModel();
        }

        public static List<ResponseModel> GetListOfResponseModels()
        {
            return new List<ResponseModel>
            {
                new ResponseModel { IntegerValue = 1, StringValue = "Test" },
                new ResponseModel { IntegerValue = 2, StringValue = "Another Test" }
            };
        }

        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Culture = CultureInfo.InvariantCulture,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
                MaxDepth = 2,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = TypeNameHandling.None
            };
        }
    }
}
