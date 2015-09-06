namespace MyWebApi.Builders.Json
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Contracts.Json;
    using Newtonsoft.Json;

    public class JsonSerializerSettingsBuilder : IAndJsonSerializerSettingsBuilder
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public JsonSerializerSettingsBuilder()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
        }

        public IAndJsonSerializerSettingsBuilder WithCulture(CultureInfo culture)
        {
            this.jsonSerializerSettings.Culture = culture;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithConstructorHandling(ConstructorHandling constructorHandling)
        {
            this.jsonSerializerSettings.ConstructorHandling = constructorHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling)
        {
            this.jsonSerializerSettings.DateFormatHandling = dateFormatHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithDateParseHandling(DateParseHandling dateParseHandling)
        {
            this.jsonSerializerSettings.DateParseHandling = dateParseHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling)
        {
            this.jsonSerializerSettings.DateTimeZoneHandling = dateTimeZoneHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling)
        {
            this.jsonSerializerSettings.DefaultValueHandling = defaultValueHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithFormatting(Formatting formatting)
        {
            this.jsonSerializerSettings.Formatting = formatting;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithMaxDepth(int? maxDepth)
        {
            this.jsonSerializerSettings.MaxDepth = maxDepth;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            this.jsonSerializerSettings.MissingMemberHandling = missingMemberHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithNullValueHandling(NullValueHandling nullValueHandling)
        {
            this.jsonSerializerSettings.NullValueHandling = nullValueHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling)
        {
            this.jsonSerializerSettings.ObjectCreationHandling = objectCreationHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithPreserveReferencesHandling(PreserveReferencesHandling preserveReferencesHandling)
        {
            this.jsonSerializerSettings.PreserveReferencesHandling = preserveReferencesHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling)
        {
            this.jsonSerializerSettings.ReferenceLoopHandling = referenceLoopHandling;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat)
        {
            this.jsonSerializerSettings.TypeNameAssemblyFormat = typeNameAssemblyFormat;
            return this;
        }

        public IAndJsonSerializerSettingsBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling)
        {
            this.jsonSerializerSettings.TypeNameHandling = typeNameHandling;
            return this;
        }

        public IJsonSerializerSettingsBuilder AndAlso()
        {
            return this;
        }
    }
}
