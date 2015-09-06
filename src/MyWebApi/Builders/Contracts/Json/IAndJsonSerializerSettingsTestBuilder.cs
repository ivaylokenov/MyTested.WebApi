namespace MyWebApi.Builders.Contracts.Json
{
    public interface IAndJsonSerializerSettingsTestBuilder : IJsonSerializerSettingsTestBuilder
    {
        IJsonSerializerSettingsTestBuilder AndAlso();
    }
}
