// MyTested.WebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
namespace MyTested.WebApi.Tests.BuildersTests.HttpMessagesTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Exceptions;
    using NUnit.Framework;
    using Setups.Controllers;

    [TestFixture]
    public class HttpRequestMessageBuilderTests
    {
        private const string StringContent = "TestContent";
        private const string TestHeader = "TestHeader";
        private const string TestHeaderValue = "TestHeaderValue";
        private const string AnotherTestHeaderValue = "AnotherTestHeaderValue";
        private const string RequestUri = "http://sometest.com/sometest?test=1";
        private const string Version = "1.1";

        private readonly byte[] buffer = { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        [Test]
        public void WithContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithContent(new StringContent(StringContent)))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void WithStreamContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStreamContent(new MemoryStream(this.buffer)))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StreamContent>(httpRequestMessage.Content);
            Assert.AreEqual(this.buffer.Length, httpRequestMessage.Content.ReadAsByteArrayAsync().Result.Length);
        }

        [Test]
        public void WithStreamContentAndBufferSizeShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStreamContent(new MemoryStream(this.buffer), 8))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StreamContent>(httpRequestMessage.Content);
            Assert.AreEqual(this.buffer.Length, httpRequestMessage.Content.ReadAsByteArrayAsync().Result.Length);
        }

        [Test]
        public void WithByteArrayContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithByteArrayContent(this.buffer))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<ByteArrayContent>(httpRequestMessage.Content);
            Assert.AreEqual(this.buffer.Length, httpRequestMessage.Content.ReadAsByteArrayAsync().Result.Length);
        }

        [Test]
        public void WithByteArrayContentAndBufferSizeShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithByteArrayContent(this.buffer, 2, 4))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<ByteArrayContent>(httpRequestMessage.Content);
            Assert.AreEqual(4, httpRequestMessage.Content.ReadAsByteArrayAsync().Result.Length);
        }

        [Test]
        public void WithFormUrlEncodedContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("First", "FirstValue"), new KeyValuePair<string, string>("Second", "SecondValue")
                }))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<FormUrlEncodedContent>(httpRequestMessage.Content);
            Assert.AreEqual("First=FirstValue&Second=SecondValue", httpRequestMessage.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(MediaType.FormUrlEncoded, httpRequestMessage.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void WithFormUrlEncodedContentShouldPopulateCorrectContentWithDirectString()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithFormUrlEncodedContent("First=FirstValue&Second=SecondValue"))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual("First=FirstValue&Second=SecondValue", httpRequestMessage.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(MediaType.FormUrlEncoded, httpRequestMessage.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void WithJsonContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithJsonContent(@"{""Age"":5}"))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(@"{""Age"":5}", httpRequestMessage.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(MediaType.ApplicationJson, httpRequestMessage.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void WithStringContentShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStringContent(StringContent))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void WithStringContentAndMediaTypeShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStringContent(StringContent, MediaType.ApplicationXml))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(MediaType.ApplicationXml, httpRequestMessage.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void WithStringContentAndEncodingShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStringContent(StringContent, Encoding.UTF8))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void WithStringContentEncodingAndMediaTypeShouldPopulateCorrectContent()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithStringContent(StringContent, Encoding.UTF8, MediaType.ApplicationJson))
                .AndProvideTheHttpRequestMessage();

            Assert.IsInstanceOf<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void WithHeaderShouldPopulateCorrectHeader()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithHeader(TestHeader, TestHeaderValue))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
        }

        [Test]
        public void WithHeaderAndMultipleValuesShouldPopulateCorrectHeader()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithHeader(TestHeader, new[] { TestHeaderValue, AnotherTestHeaderValue }))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
            Assert.IsTrue(httpRequestMessage.Headers.First(h => h.Key == TestHeader).Value.Contains(AnotherTestHeaderValue));
        }

        [Test]
        public void WithHeadersDictionaryShouldPopulateCorrectHeaders()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>
                    {
                        { TestHeader, new[] { TestHeaderValue, AnotherTestHeaderValue } },
                    }))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
            Assert.IsTrue(httpRequestMessage.Headers.First(h => h.Key == TestHeader).Value.Contains(AnotherTestHeaderValue));
        }

        [Test]
        public void WithContentHeaderShouldPopulateCorrectHeader()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request
                    .WithStringContent(StringContent)
                    .WithContentHeader(TestHeader, TestHeaderValue))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Content.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Content.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
        }

        [Test]
        public void WithContentHeaderAndMultipleValuesShouldPopulateCorrectHeader()
        {
            var headers = new[]
            {
                TestHeaderValue, AnotherTestHeaderValue
            };

            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request
                    .WithStringContent(StringContent)
                    .WithContentHeader(TestHeader, headers))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Content.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Content.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
            Assert.IsTrue(httpRequestMessage.Content.Headers.First(h => h.Key == TestHeader).Value.Contains(AnotherTestHeaderValue));
        }

        [Test]
        public void WithContentHeadersDictionaryShouldPopulateCorrectHeaders()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request
                    .WithStringContent(StringContent)
                    .WithContentHeaders(new Dictionary<string, IEnumerable<string>>
                    {
                        { TestHeader, new[] { TestHeaderValue, AnotherTestHeaderValue } },
                    }))
                .AndProvideTheHttpRequestMessage();

            Assert.IsTrue(httpRequestMessage.Content.Headers.Contains(TestHeader));
            Assert.IsTrue(httpRequestMessage.Content.Headers.First(h => h.Key == TestHeader).Value.Contains(TestHeaderValue));
            Assert.IsTrue(httpRequestMessage.Content.Headers.First(h => h.Key == TestHeader).Value.Contains(AnotherTestHeaderValue));
        }

        [Test]
        [ExpectedException(
            typeof(InvalidHttpRequestMessageException),
            ExpectedMessage = "When building HttpRequestMessage expected content to be initialized and set in order to add content headers.")]
        public void WithContentHeadersShouldThrowExpcetionIfNoContentIsPresent()
        {
            MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request
                    .WithContentHeaders(new Dictionary<string, IEnumerable<string>>
                    {
                        { TestHeader, new[] { TestHeaderValue, AnotherTestHeaderValue } },
                    }));
        }

        [Test]
        public void WithMethodStringShouldPopulateCorrectMethod()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithMethod("GET"))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(HttpMethod.Get, httpRequestMessage.Method);
        }

        [Test]
        public void WithMethodShouldPopulateCorrectMethod()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithMethod(HttpMethod.Get))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(HttpMethod.Get, httpRequestMessage.Method);
        }

        [Test]
        public void WithRequestUriStringShouldPopulateCorrectUri()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithRequestUri(RequestUri))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(new Uri(RequestUri), httpRequestMessage.RequestUri);
        }

        [Test]
        public void WithRequestUriShouldPopulateCorrectUri()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithRequestUri(new Uri(RequestUri)))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(new Uri(RequestUri), httpRequestMessage.RequestUri);
        }

        [Test]
        public void WithRequestUriBuilderShouldPopulateCorrectUri()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request
                    => request
                        .WithRequestUri(uri
                            => uri
                                .WithScheme("http")
                                .WithHost("sometest.com")))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(new Uri(RequestUri).Host, httpRequestMessage.RequestUri.Host);
        }

        [Test]
        public void WithVersionStringShouldPopulateCorrectVersion()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithVersion(Version))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(Version, httpRequestMessage.Version.ToString());
        }

        [Test]
        public void WithVersionIntegersShouldPopulateCorrectVersion()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithVersion(1, 1))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(Version, httpRequestMessage.Version.ToString());
        }

        [Test]
        public void WithVersionShouldPopulateCorrectVersion()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithVersion(new Version(1, 1)))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(Version, httpRequestMessage.Version.ToString());
        }

        [Test]
        [ExpectedException(
            typeof(InvalidHttpRequestMessageException),
            ExpectedMessage = "When building HttpRequestMessage expected version to be valid version string, but instead received invalid one.")]
        public void WithInvalidVersionShouldThrowInvalidHttpRequestMessageException()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request => request.WithVersion("Invalid"))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(Version, httpRequestMessage.Version.ToString());
        }

        [Test]
        public void AndAlsoShouldBuildCorrectHttpRequestMessage()
        {
            var httpRequestMessage = MyWebApi
                .Controller<WebApiController>()
                .WithHttpRequestMessage(request
                    => request
                        .WithMethod("GET")
                        .AndAlso()
                        .WithRequestUri(RequestUri)
                        .AndAlso()
                        .WithVersion("1.1")
                        .AndAlso()
                        .WithStringContent(StringContent))
                .AndProvideTheHttpRequestMessage();

            Assert.AreEqual(HttpMethod.Get, httpRequestMessage.Method);
            Assert.AreEqual(new Uri(RequestUri), httpRequestMessage.RequestUri);
            Assert.AreEqual(new Version(1, 1), httpRequestMessage.Version);
            Assert.IsAssignableFrom<StringContent>(httpRequestMessage.Content);
            Assert.AreEqual(StringContent, httpRequestMessage.Content.ReadAsStringAsync().Result);
        }
    }
}
