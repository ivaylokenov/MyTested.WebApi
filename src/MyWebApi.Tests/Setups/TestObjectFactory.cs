namespace MyWebApi.Tests.Setups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Runtime.Serialization.Formatters;
    using System.Web.Http.ModelBinding;
    using Common;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class TestObjectFactory
    {
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

        public static MediaTypeFormatter GetCustomMediaTypeFormatter()
        {
            return new CustomMediaTypeFormatter();
        }

        public static Uri GetUri()
        {
            return new Uri("http://somehost.com/someuri/1?query=Test");
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
