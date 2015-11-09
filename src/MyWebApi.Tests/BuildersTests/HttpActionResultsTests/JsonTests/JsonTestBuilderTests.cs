// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpActionResultsTests.JsonTests
{
    using System.Collections.Generic;
    using System.Text;
    using Exceptions;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;

    [TestFixture]
    public class JsonTestBuilderTests
    {
        [Test]
        public void WithResponseModelOfTypeShouldWorkCorrectlyWithJson()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Test]
        public void WithDefaultEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithEncodingAction action in WebApiController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        public void WithDefaultEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }

        [Test]
        public void WithEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithEncoding(Encoding.ASCII);
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithEncodingAction action in WebApiController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        public void WithEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithEncodingAction())
                .ShouldReturn()
                .Json()
                .WithEncoding(Encoding.UTF8);
        }

        [Test]
        public void WithDefaultJsonSettingsShouldNotThrowExeptionWithDefaultJsonSettings()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding()
                .AndAlso()
                .WithDefaulJsonSerializerSettings();
        }

        [Test]
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithSameJsonSettings()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(TestObjectFactory.GetJsonSerializerSettings());
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.")]
        public void WithJsonSerializerSettingsShouldThrowExceptionWithDifferentJsonSettings()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTime;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(jsonSerializerSettings);
        }
    }
}
