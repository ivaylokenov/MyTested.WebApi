namespace MyWebApi.Builders.Contracts.Json
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public interface IJsonSerializerSettingsTestBuilder
    {
        IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture);

        IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver);

        IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
             where TContractResolver : IContractResolver;

        IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling);

        IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling);

        IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling);

        IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling);

        IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling);

        IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting);

        IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth);

        IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling);

        IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling);

        IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling);

        IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(
            PreserveReferencesHandling preserveReferencesHandling);

        IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling);

        IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat);

        IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling);
    }
}
