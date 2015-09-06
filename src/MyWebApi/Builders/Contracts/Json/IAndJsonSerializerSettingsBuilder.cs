namespace MyWebApi.Builders.Contracts.Json
{
    public interface IAndJsonSerializerSettingsBuilder : IJsonSerializerSettingsBuilder
    {
        IJsonSerializerSettingsBuilder AndAlso();
    }
}
