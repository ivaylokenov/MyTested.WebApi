namespace MyWebApi.Builders.Json
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Contracts.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Utilities;

    public class JsonSerializerSettingsTestBuilder : IAndJsonSerializerSettingsTestBuilder
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly ICollection<Func<JsonSerializerSettings, JsonSerializerSettings, bool>> validations; 

        public JsonSerializerSettingsTestBuilder()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.validations = new List<Func<JsonSerializerSettings, JsonSerializerSettings, bool>>();
        }

        public IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture)
        {
            this.jsonSerializerSettings.Culture = culture;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.Culture, actual.Culture));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver)
        {
            this.jsonSerializerSettings.ContractResolver = contractResolver;
            validations.Add((expected, actual) => !Reflection.AreDifferentTypes(expected.ContractResolver, actual.ContractResolver));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
            where TContractResolver : IContractResolver
        {
            this.WithContractResolver(Activator.CreateInstance<TContractResolver>());
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling)
        {
            this.jsonSerializerSettings.ConstructorHandling = constructorHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.ConstructorHandling, actual.ConstructorHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling)
        {
            this.jsonSerializerSettings.DateFormatHandling = dateFormatHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.DateFormatHandling, actual.DateFormatHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling)
        {
            this.jsonSerializerSettings.DateParseHandling = dateParseHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.DateParseHandling, actual.DateParseHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling)
        {
            this.jsonSerializerSettings.DateTimeZoneHandling = dateTimeZoneHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.DateTimeZoneHandling, actual.DateTimeZoneHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling)
        {
            this.jsonSerializerSettings.DefaultValueHandling = defaultValueHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.DefaultValueHandling, actual.DefaultValueHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting)
        {
            this.jsonSerializerSettings.Formatting = formatting;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.Formatting, actual.Formatting));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth)
        {
            this.jsonSerializerSettings.MaxDepth = maxDepth;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.MaxDepth, actual.MaxDepth));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            this.jsonSerializerSettings.MissingMemberHandling = missingMemberHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.MissingMemberHandling, actual.MissingMemberHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling)
        {
            this.jsonSerializerSettings.NullValueHandling = nullValueHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.NullValueHandling, actual.NullValueHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling)
        {
            this.jsonSerializerSettings.ObjectCreationHandling = objectCreationHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.ObjectCreationHandling, actual.ObjectCreationHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(PreserveReferencesHandling preserveReferencesHandling)
        {
            this.jsonSerializerSettings.PreserveReferencesHandling = preserveReferencesHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.PreserveReferencesHandling, actual.PreserveReferencesHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling)
        {
            this.jsonSerializerSettings.ReferenceLoopHandling = referenceLoopHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.ReferenceLoopHandling, actual.ReferenceLoopHandling));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat)
        {
            this.jsonSerializerSettings.TypeNameAssemblyFormat = typeNameAssemblyFormat;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.TypeNameAssemblyFormat, actual.TypeNameAssemblyFormat));
            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling)
        {
            this.jsonSerializerSettings.TypeNameHandling = typeNameHandling;
            validations.Add((expected, actual) => Validator.CheckEquality(expected.TypeNameHandling, actual.TypeNameHandling));
            return this;
        }

        public IJsonSerializerSettingsTestBuilder AndAlso()
        {
            return this;
        }

        internal JsonSerializerSettings GetJsonSerializerSettings()
        {
            return this.jsonSerializerSettings;
        }

        internal ICollection<Func<JsonSerializerSettings, JsonSerializerSettings, bool>> GetJsonSerializerSettingsValidations()
        {
            return this.validations;
        }
    }
}
