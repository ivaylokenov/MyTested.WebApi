// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpActionResultsTests.JsonTests
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Exceptions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NUnit.Framework;
    using Setups;
    using Setups.Controllers;

    [TestFixture]
    public class JsonSerializerSettingsTestBuilderTests
    {
        [Test]
        public void WithCultureShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s => 
                    s.WithCulture(CultureInfo.InvariantCulture));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have 'English (United States)' culture, but in fact found 'Invariant Language (Invariant Country)'.")]
        public void WithCultureShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithCulture(CultureInfo.CurrentCulture));
        }

        [Test]
        public void WithCultureShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.Culture = CultureInfo.InvariantCulture;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.Default;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithCulture(CultureInfo.InvariantCulture));
        }

        [Test]
        public void WithContractResolverOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>());
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have DefaultContractResolver, but in fact found CamelCasePropertyNamesContractResolver.")]
        public void WithContractResolverOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolverOfType<DefaultContractResolver>());
        }

        [Test]
        public void WithContractResolverOfTypeShouldValidateTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>());
        }

        [Test]
        public void WithContractResolverShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolver(new CamelCasePropertyNamesContractResolver()));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have DefaultContractResolver, but in fact found CamelCasePropertyNamesContractResolver.")]
        public void WithContractResolverShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolver(new DefaultContractResolver()));
        }

        [Test]
        public void WithContractResolverShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolver(new CamelCasePropertyNamesContractResolver()));
        }

        [Test]
        public void WithConstructorHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.")]
        public void WithConstructorHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithConstructorHandling(ConstructorHandling.Default));
        }

        [Test]
        public void WithConstructorHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Test]
        public void WithDateFormatHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have IsoDateFormat date format handling, but in fact found MicrosoftDateFormat.")]
        public void WithDateFormatHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateFormatHandling(DateFormatHandling.IsoDateFormat));
        }

        [Test]
        public void WithDateFormatHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat));
        }

        [Test]
        public void WithDateParseHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateParseHandling(DateParseHandling.DateTimeOffset));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.")]
        public void WithDateParseHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateParseHandling(DateParseHandling.DateTime));
        }

        [Test]
        public void WithDateParseHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateParseHandling(DateParseHandling.DateTimeOffset));
        }

        [Test]
        public void WithDateTimeZoneHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateTimeZoneHandling(DateTimeZoneHandling.Utc));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Local date time zone handling, but in fact found Utc.")]
        public void WithDateTimeZoneHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local));
        }

        [Test]
        public void WithDateTimeZoneHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local));
        }

        [Test]
        public void WithDefaultValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDefaultValueHandling(DefaultValueHandling.Ignore));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Include default value handling, but in fact found Ignore.")]
        public void WithDefaultValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDefaultValueHandling(DefaultValueHandling.Include));
        }

        [Test]
        public void WithDefaultValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDefaultValueHandling(DefaultValueHandling.Ignore));
        }

        [Test]
        public void WithFormattingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithFormatting(Formatting.Indented));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have None formatting, but in fact found Indented.")]
        public void WithFormattingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithFormatting(Formatting.None));
        }

        [Test]
        public void WithFormattingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.Indented;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithFormatting(Formatting.Indented));
        }

        [Test]
        public void WithMaxDepthShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMaxDepth(2));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have 2147483647 max depth, but in fact found 2.")]
        public void WithMaxDepthShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMaxDepth(int.MaxValue));
        }

        [Test]
        public void WithMaxDepthShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.None;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMaxDepth(int.MaxValue));
        }

        [Test]
        public void WithMissingMemberHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMissingMemberHandling(MissingMemberHandling.Ignore));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Error missing member handling, but in fact found Ignore.")]
        public void WithMissingMemberHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMissingMemberHandling(MissingMemberHandling.Error));
        }

        [Test]
        public void WithMissingMemberHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMissingMemberHandling(MissingMemberHandling.Error));
        }

        [Test]
        public void WithNullValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithNullValueHandling(NullValueHandling.Ignore));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Include null value handling, but in fact found Ignore.")]
        public void WithNullValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithNullValueHandling(NullValueHandling.Include));
        }

        [Test]
        public void WithNullValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Include;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithNullValueHandling(NullValueHandling.Include));
        }

        [Test]
        public void WithObjectCreationHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithObjectCreationHandling(ObjectCreationHandling.Replace));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Auto object creation handling, but in fact found Replace.")]
        public void WithObjectCreationHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithObjectCreationHandling(ObjectCreationHandling.Auto));
        }

        [Test]
        public void WithObjectCreationHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithObjectCreationHandling(ObjectCreationHandling.Replace));
        }

        [Test]
        public void WithPreserveReferencesHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Objects preserve references handling, but in fact found Arrays.")]
        public void WithPreserveReferencesHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithPreserveReferencesHandling(PreserveReferencesHandling.Objects));
        }

        [Test]
        public void WithPreserveReferencesHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Arrays;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays));
        }

        [Test]
        public void WithReferenceLoopHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithReferenceLoopHandling(ReferenceLoopHandling.Serialize));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Ignore reference loop handling, but in fact found Serialize.")]
        public void WithReferenceLoopHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore));
        }

        [Test]
        public void WithReferenceLoopHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore));
        }

        [Test]
        public void WithTypeNameAssemblyFormatShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Simple));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Full type name assembly format, but in fact found Simple.")]
        public void WithTypeNameAssemblyFormatShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Full));
        }

        [Test]
        public void WithTypeNameAssemblyFormatShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Simple));
        }

        [Test]
        public void WithTypeNameHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameHandling(TypeNameHandling.None));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Auto type name handling, but in fact found None.")]
        public void WithTypeNameHandlingShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameHandling(TypeNameHandling.Auto));
        }

        [Test]
        public void WithTypeNameHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Arrays;

            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameHandling(TypeNameHandling.Arrays));
        }

        [Test]
        public void AndAlsoShouldNotThrowExceptionWithCorrectValues()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s => s
                    .WithFormatting(Formatting.Indented)
                    .AndAlso()
                    .WithMaxDepth(2)
                    .AndAlso()
                    .WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Test]
        [ExpectedException(
            typeof(JsonResultAssertionException),
            ExpectedMessage = "When calling JsonWithSettingsAction action in WebApiController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.")]
        public void AndAlsoShouldThrowExceptionWithIncorrectValue()
        {
            MyWebApi
                .Controller<WebApiController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s => s
                    .WithFormatting(Formatting.Indented)
                    .AndAlso()
                    .WithMaxDepth(2)
                    .AndAlso()
                    .WithConstructorHandling(ConstructorHandling.Default));
        }
    }
}
