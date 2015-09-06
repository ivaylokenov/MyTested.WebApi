namespace MyWebApi.Builders.Contracts.Json
{
    using System;
    using System.Text;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public interface IJsonTestBuilder : IBaseResponseModelTestBuilder
    {
        IAndJsonTestBuilder WithDefaultEncoding();

        IAndJsonTestBuilder WithEncoding(Encoding encoding);

        IAndJsonTestBuilder WithDefaulJsonSerializerSettings();

        IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings);

        IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder);
    }
}
