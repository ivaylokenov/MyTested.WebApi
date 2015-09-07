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

    /// <summary>
    /// Used for testing JSON serializer settings in a JSON result.
    /// </summary>
    public class JsonSerializerSettingsTestBuilder : IAndJsonSerializerSettingsTestBuilder
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly ICollection<Func<JsonSerializerSettings, JsonSerializerSettings, bool>> validations; 

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerSettingsTestBuilder" /> class.
        /// </summary>
        public JsonSerializerSettingsTestBuilder()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.validations = new List<Func<JsonSerializerSettings, JsonSerializerSettings, bool>>();
        }

        /// <summary>
        /// Tests the Culture property in a JSON serializer settings object.
        /// </summary>
        /// <param name="culture">Expected Culture.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture)
        {
            this.jsonSerializerSettings.Culture = culture;
            this.validations.Add((expected, actual) => expected.Culture.DisplayName == actual.Culture.DisplayName);
            return this;
        }

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object.
        /// </summary>
        /// <param name="contractResolver">Expected ContractResolver.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver)
        {
            this.jsonSerializerSettings.ContractResolver = contractResolver;
            this.validations.Add((expected, actual) => Reflection.AreSameTypes(expected.ContractResolver, actual.ContractResolver));
            return this;
        }

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object by using generic type.
        /// </summary>
        /// <typeparam name="TContractResolver">Expected ContractResolver type.</typeparam>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
            where TContractResolver : IContractResolver
        {
            this.WithContractResolver(Activator.CreateInstance<TContractResolver>());
            return this;
        }

        /// <summary>
        /// Tests the ConstructorHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="constructorHandling">Expected ConstructorHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling)
        {
            this.jsonSerializerSettings.ConstructorHandling = constructorHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.ConstructorHandling, actual.ConstructorHandling));
            return this;
        }

        /// <summary>
        /// Tests the DateFormatHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateFormatHandling">Expected DateFormatHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling)
        {
            this.jsonSerializerSettings.DateFormatHandling = dateFormatHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.DateFormatHandling, actual.DateFormatHandling));
            return this;
        }

        /// <summary>
        /// Tests the DateParseHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateParseHandling">Expected DateParseHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling)
        {
            this.jsonSerializerSettings.DateParseHandling = dateParseHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.DateParseHandling, actual.DateParseHandling));
            return this;
        }

        /// <summary>
        /// Tests the WithDateTimeZoneHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateTimeZoneHandling">Expected WithDateTimeZoneHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling)
        {
            this.jsonSerializerSettings.DateTimeZoneHandling = dateTimeZoneHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.DateTimeZoneHandling, actual.DateTimeZoneHandling));
            return this;
        }

        /// <summary>
        /// Tests the DefaultValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="defaultValueHandling">Expected DefaultValueHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling)
        {
            this.jsonSerializerSettings.DefaultValueHandling = defaultValueHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.DefaultValueHandling, actual.DefaultValueHandling));
            return this;
        }

        /// <summary>
        /// Tests the Formatting property in a JSON serializer settings object.
        /// </summary>
        /// <param name="formatting">Expected Formatting.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting)
        {
            this.jsonSerializerSettings.Formatting = formatting;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.Formatting, actual.Formatting));
            return this;
        }

        /// <summary>
        /// Tests the MaxDepth property in a JSON serializer settings object.
        /// </summary>
        /// <param name="maxDepth">Expected max depth.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth)
        {
            this.jsonSerializerSettings.MaxDepth = maxDepth;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.MaxDepth, actual.MaxDepth));
            return this;
        }

        /// <summary>
        /// Tests the MissingMemberHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="missingMemberHandling">Expected MissingMemberHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            this.jsonSerializerSettings.MissingMemberHandling = missingMemberHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.MissingMemberHandling, actual.MissingMemberHandling));
            return this;
        }

        /// <summary>
        /// Tests the NullValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="nullValueHandling">Expected NullValueHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling)
        {
            this.jsonSerializerSettings.NullValueHandling = nullValueHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.NullValueHandling, actual.NullValueHandling));
            return this;
        }

        /// <summary>
        /// Tests the ObjectCreationHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="objectCreationHandling">Expected ObjectCreationHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling)
        {
            this.jsonSerializerSettings.ObjectCreationHandling = objectCreationHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.ObjectCreationHandling, actual.ObjectCreationHandling));
            return this;
        }

        /// <summary>
        /// Tests the PreserveReferencesHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="preserveReferencesHandling">Expected PreserveReferencesHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(PreserveReferencesHandling preserveReferencesHandling)
        {
            this.jsonSerializerSettings.PreserveReferencesHandling = preserveReferencesHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.PreserveReferencesHandling, actual.PreserveReferencesHandling));
            return this;
        }

        /// <summary>
        /// Tests the ReferenceLoopHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="referenceLoopHandling">Expected ReferenceLoopHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling)
        {
            this.jsonSerializerSettings.ReferenceLoopHandling = referenceLoopHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.ReferenceLoopHandling, actual.ReferenceLoopHandling));
            return this;
        }

        /// <summary>
        /// Tests the FormatterAssemblyStyle property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameAssemblyFormat">Expected FormatterAssemblyStyle.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat)
        {
            this.jsonSerializerSettings.TypeNameAssemblyFormat = typeNameAssemblyFormat;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.TypeNameAssemblyFormat, actual.TypeNameAssemblyFormat));
            return this;
        }

        /// <summary>
        /// Tests the TypeNameHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameHandling">Expected TypeNameHandling.</param>
        /// <returns>And JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling)
        {
            this.jsonSerializerSettings.TypeNameHandling = typeNameHandling;
            this.validations.Add((expected, actual) => Validator.CheckEquality(expected.TypeNameHandling, actual.TypeNameHandling));
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining JSON serializer settings test builder.
        /// </summary>
        /// <returns>JSON serializer settings test builder.</returns>
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
